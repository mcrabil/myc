using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myc
{
    class Program
    {
        //TODO: week 5
        //Compound assignment operators, comma opeerators, increment/decrement operators

        public Lexer lexer;
        public Parser parser;
        public Codegen codegen;

        //Variable map
        public static Dictionary<string, int> varmap = new Dictionary<string, int>();
        public static int varidx = -4;

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
            string inputFile = "../../../stage_5/valid/undefined_missing_return.c";
            if (args.Length >= 1) { inputFile = args[0]; }
            Console.WriteLine("Using input file: " + inputFile);

            prog.ReadInputFile(inputFile);

            prog.parser.lexer = prog.lexer;

            //prog.lexer.PrintAllTokens();

            ASTNode node = prog.parser.Prog();
            prog.codegen.Generate(node);

            //prog.parser.PrettyPrintAST(node);

            if (args.Length >= 2)
            {
                System.IO.File.WriteAllText(args[1], prog.codegen.outputStr);
            }

            Console.WriteLine("Parsed successfully!" + Environment.NewLine);
        }
    }
}
