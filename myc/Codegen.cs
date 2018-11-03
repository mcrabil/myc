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
                        if (node.tokValue.type == TokenType.Minus)
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
                case ASTType.BinOp:
                    {
                        Generate(node.left);
                        outputStr += "push %eax" + Environment.NewLine;
                        Generate(node.right);
                        if(node.op.type == TokenType.Addition)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "addl %ecx, %eax" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.Minus)
                        {
                            outputStr += "movl %eax, %ecx" + Environment.NewLine;
                            outputStr += "pop %eax" + Environment.NewLine;
                            outputStr += "subl %ecx, %eax" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.Multiplication)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "imul %ecx, %eax" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.Division)
                        {
                            outputStr += "movl %eax, %ecx" + Environment.NewLine;
                            outputStr += "pop %eax" + Environment.NewLine;
                            outputStr += "movl $0, %edx" + Environment.NewLine;
                            outputStr += "idivl %ecx, %eax" + Environment.NewLine;
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