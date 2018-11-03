using System;

namespace myc
{
    public class Codegen
    {
        public string outputStr = "";

        public void Generate(ASTNode node)
        {
            switch (node.type)
            {
                case ASTType.Program:
                    {
                        Generate(node.child);
                        break;
                    }
                case ASTType.Function:
                    {
                        //Function name
                        outputStr += ".globl ";
                        outputStr += "_main" + Environment.NewLine + "_main:" + Environment.NewLine;

                        Generate(node.child);
                        break;
                    }
                case ASTType.Return:
                    {
                        Generate(node.child);

                        outputStr += "ret" + Environment.NewLine;
                        break;
                    }
                case ASTType.Constant:
                    {
                        outputStr += "movl $";
                        outputStr += node.tokValue.value.ToString();
                        outputStr += ", %eax" + Environment.NewLine;
                        break;
                    }
                case ASTType.UnOp:
                    {
                        if (node.tokValue.type == TokenType.Negation)
                        {
                            Generate(node.child);
                            outputStr += "neg %eax" + Environment.NewLine;
                        }
                        else if (node.tokValue.type == TokenType.BitwiseComp)
                        {
                            Generate(node.child);
                            outputStr += "not %eax" + Environment.NewLine;
                        }
                        else if (node.tokValue.type == TokenType.LogicalNeg)
                        {
                            Generate(node.child);
                            outputStr += "cmpl $0, %eax" + Environment.NewLine;
                            outputStr += "movl $0, %eax" + Environment.NewLine;
                            outputStr += "sete %al" + Environment.NewLine;
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unsupported AST Type!!!" + Environment.NewLine);
                        break;
                    }
            }
        }
    }
}