using System;
using System.Collections.Generic;

namespace myc
{
    class Program
    {
        //TODO: week 5
        //Comma operators, increment/decrement operators
        //TODO: week 7
        //Update parsing error name to start with syntax_error. Should pull out the error handling.

        //Make a better testing system.

        //Week 8: work on codegen

        public Lexer lexer;
        public Parser parser;
        public Codegen codegen;

        //Variable map
        public static List<Dictionary<string, int>> varmap = new List<Dictionary<string, int>>();
        public static int scopeidx = -1;
        public static string breakLabel = "";
        public static string continueLabel = "";

        public static int NextVarMapIdx()
        {
            int count = 1;
            foreach (var map in varmap)
            {
                count += map.Count;
            }
            return (-4 * count);
        }

        public static bool VarMapContainsVar(string key)
        {
            foreach(var map in varmap)
            {
                if (map.ContainsKey(key))
                {
                    return true;
                }
            }
            return false;
        }

        public static string VarMapLookup(string key)
        {
            int len = Program.varmap.Count - 1;
            for (int i = len; i >= 0; i--)
            {
                if(Program.varmap[i].ContainsKey(key))
                {
                    return Program.varmap[i][key].ToString();
                }
            }
            return "";
        }

        public static void Error()
        {
            Console.WriteLine("THERE WAS AN ERROR PARSING" + Environment.NewLine);
            System.Environment.Exit(1);
        }

        public static void Error(string error)
        {
            Console.WriteLine(error + Environment.NewLine);
            System.Environment.Exit(1);
        }

        public Program()
        {
            lexer = new Lexer();
            parser = new Parser();
            codegen = new Codegen();
        }

        public void ReadInputFile(string fileName)
        {

            lexer.text = System.IO.File.ReadAllText(fileName);
            lexer.totalTextLen = lexer.text.Length;
        }

        // Project to look at : https://norasandler.com/2017/11/29/Write-a-Compiler.html
        // Compile using: i686-w64-mingw32-gcc -m32 .\mytest.s -o mytest.exe

        static void Main(string[] args)
        {
            Program prog = new Program();
            string inputFile = "../../../stage_8/valid/continue.c";
            if (args.Length >= 1) { inputFile = args[0]; }
            Console.WriteLine("Using input file: " + inputFile);

            prog.ReadInputFile(inputFile);

            prog.parser.lexer = prog.lexer;

            //prog.lexer.PrintAllTokens();

            ASTNode node = prog.parser.Prog();

            if(Program.varmap.Count > 0)
            {
                Error("Internal error: Varmap count was greater than 0");
            }

            prog.codegen.Generate(node);

            //prog.parser.PrettyPrintAST(node);

            if (args.Length >= 2)
            {
                System.IO.File.WriteAllText(args[1], prog.codegen.outputStr);
            }
            else
            {
                //Console.WriteLine(prog.codegen.outputStr);
            }

            Console.WriteLine("Parsed successfully!" + Environment.NewLine);
        }
    }
}
