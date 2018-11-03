using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myc
{
    class Program
    {
        public Lexer lexer;
        public Parser parser;
        public Codegen codegen;

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
            string inputFile = "../../../stage_2/valid/bitwise.c";
            if (args.Length >= 1) { inputFile = args[0]; }
            Console.WriteLine("Using input file: " + inputFile);

            prog.ReadInputFile(inputFile);

            prog.parser.lexer = prog.lexer;

            ASTNode node = prog.parser.Program();
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
