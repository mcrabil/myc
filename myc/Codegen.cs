using System;

namespace myc
{
    public class Codegen
    {
        public int branchCounterLabel = 0;
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
                        outputStr += "push %ebp" + Environment.NewLine;
                        outputStr += "movl %esp, %ebp" + Environment.NewLine;

                        foreach(var statement in node.statements)
                        {
                            Generate(statement);
                        }
                        break;
                    }
                case ASTType.Return:
                    {
                        Generate(node.child);

                        outputStr += "movl %ebp, %esp" + Environment.NewLine;
                        outputStr += "pop %ebp" + Environment.NewLine;
                        outputStr += "ret" + Environment.NewLine;
                        break;
                    }
                case ASTType.Declare:
                    {
                        if(node.child != null)
                        {
                            Generate(node.child);
                        }

                        outputStr += "push %eax" + Environment.NewLine;
                        break;
                    }
                case ASTType.Assign:
                    {
                        if(!Program.varmap.ContainsKey(node.ident.strval))
                        {
                            Program.Error("Variable undefined");
                        }
                        if((node.tokValue.type >= TokenType.AdditionAssignment) && (node.tokValue.type <= TokenType.BitwiseXorAssignment))
                        {
                            outputStr += "movl " + Program.varmap[node.ident.strval].ToString() + "(%ebp), %eax" + Environment.NewLine;
                            outputStr += "push %eax" + Environment.NewLine;
                        }

                        Generate(node.child);

                        switch (node.tokValue.type)
                        {
                            case TokenType.AdditionAssignment:
                                {
                                    outputStr += "pop %ecx" + Environment.NewLine;
                                    outputStr += "addl %ecx, %eax" + Environment.NewLine;
                                    break;
                                }
                            case TokenType.MinusAssignment:
                                {
                                    outputStr += "movl %eax, %ecx" + Environment.NewLine;
                                    outputStr += "pop %eax" + Environment.NewLine;
                                    outputStr += "subl %ecx, %eax" + Environment.NewLine;
                                    break;
                                }
                            case TokenType.DivisionAssignment:
                                {
                                    outputStr += "movl %eax, %ecx" + Environment.NewLine;
                                    outputStr += "pop %eax" + Environment.NewLine;
                                    outputStr += "movl $0, %edx" + Environment.NewLine;
                                    outputStr += "idivl %ecx, %eax" + Environment.NewLine;
                                    break;
                                }
                            case TokenType.MultiplicationAssignment:
                                {
                                    outputStr += "pop %ecx" + Environment.NewLine;
                                    outputStr += "imul %ecx, %eax" + Environment.NewLine;
                                    break;
                                }
                            case TokenType.ModuloAssignment:
                                {
                                    outputStr += "movl %eax, %ecx" + Environment.NewLine;
                                    outputStr += "pop %eax" + Environment.NewLine;
                                    outputStr += "movl $0, %edx" + Environment.NewLine;
                                    outputStr += "idivl %ecx, %eax" + Environment.NewLine;
                                    outputStr += "movl %edx, %eax" + Environment.NewLine;
                                    break;
                                }
                            case TokenType.BitShiftLeftAssignment:
                                {
                                    outputStr += "movl %eax, %ecx" + Environment.NewLine;
                                    outputStr += "pop %eax" + Environment.NewLine;
                                    outputStr += "shl %cl, %eax" + Environment.NewLine;
                                    break;
                                }
                            case TokenType.BitShiftRightAssignment:
                                {
                                    outputStr += "movl %eax, %ecx" + Environment.NewLine;
                                    outputStr += "pop %eax" + Environment.NewLine;
                                    outputStr += "shr %cl, %eax" + Environment.NewLine;
                                    break;
                                }
                            case TokenType.BitwiseAndAssignment:
                                {
                                    outputStr += "pop %ecx" + Environment.NewLine;
                                    outputStr += "andl %ecx, %eax" + Environment.NewLine;
                                    break;
                                }
                            case TokenType.BitwiseOrAssignment:
                                {
                                    outputStr += "pop %ecx" + Environment.NewLine;
                                    outputStr += "orl %ecx, %eax" + Environment.NewLine;
                                    break;
                                }
                            case TokenType.BitwiseXorAssignment:
                                {
                                    outputStr += "pop %ecx" + Environment.NewLine;
                                    outputStr += "xorl %ecx, %eax" + Environment.NewLine;
                                    break;
                                }
                        }

                        outputStr += "movl %eax, " + Program.varmap[node.ident.strval].ToString() + "(%ebp)" + Environment.NewLine;
                        break;
                    }
                case ASTType.Var:
                    {
                        if(!Program.varmap.ContainsKey(node.tokValue.strval))
                        {
                            Program.Error("Undeclared var");
                        }
                        outputStr += "movl " + Program.varmap[node.tokValue.strval].ToString() + "(%ebp), %eax" + Environment.NewLine;
                        break;
                    }
                case ASTType.ConditionalStatement:
                    {
                        Generate(node.child);
                        branchCounterLabel++;
                        string label = branchCounterLabel.ToString();
                        outputStr += "cmpl $0, %eax" + Environment.NewLine;
                        outputStr += "je _branch_" + label + Environment.NewLine;
                        Generate(node.ifStatement);
                        outputStr += "jmp _post_conditional_" + label + Environment.NewLine;
                        outputStr += "_branch_" + label + ":" + Environment.NewLine;
                        if(node.elseStatement != null)
                        {
                            Generate(node.elseStatement);
                        }
                        outputStr += "_post_conditional_" + label + ":" + Environment.NewLine;
                        break;
                    }
                case ASTType.ConditionalExpression:
                    {
                        Generate(node.child);
                        branchCounterLabel++;
                        string label = branchCounterLabel.ToString();
                        outputStr += "cmpl $0, %eax" + Environment.NewLine;
                        outputStr += "je _e3_" + label + Environment.NewLine;
                        Generate(node.ifExpr);
                        outputStr += "jmp _post_conditional_" + label + Environment.NewLine;
                        outputStr += "_e3_" + label + ":" + Environment.NewLine;
                        Generate(node.elseExpr);
                        outputStr += "_post_conditional_" + label + ":" + Environment.NewLine;
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
                        else if(node.op.type == TokenType.Equal)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "cmpl %eax, %ecx" + Environment.NewLine;
                            outputStr += "movl $0, %eax" + Environment.NewLine;
                            outputStr += "sete %al" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.NotEqual)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "cmpl %eax, %ecx" + Environment.NewLine;
                            outputStr += "movl $0, %eax" + Environment.NewLine;
                            outputStr += "setne %al" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.LessThan)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "cmpl %eax, %ecx" + Environment.NewLine;
                            outputStr += "movl $0, %eax" + Environment.NewLine;
                            outputStr += "setl %al" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.LessThanOrEqual)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "cmpl %eax, %ecx" + Environment.NewLine;
                            outputStr += "movl $0, %eax" + Environment.NewLine;
                            outputStr += "setle %al" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.GreaterThan)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "cmpl %eax, %ecx" + Environment.NewLine;
                            outputStr += "movl $0, %eax" + Environment.NewLine;
                            outputStr += "setg %al" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.GreaterThanOrEqual)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "cmpl %eax, %ecx" + Environment.NewLine;
                            outputStr += "movl $0, %eax" + Environment.NewLine;
                            outputStr += "setge %al" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.LogicalOr)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "orl %ecx, %eax" + Environment.NewLine;
                            outputStr += "movl $0, %eax" + Environment.NewLine;
                            outputStr += "setne %al" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.LogicalAnd)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "cmpl $0, %ecx" + Environment.NewLine;
                            outputStr += "setne %cl" + Environment.NewLine;
                            outputStr += "cmpl $0, %eax" + Environment.NewLine;
                            outputStr += "movl $0, %eax" + Environment.NewLine;
                            outputStr += "setne %al" + Environment.NewLine;
                            outputStr += "andb %cl, %al" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.Modulo)
                        {
                            outputStr += "movl %eax, %ecx" + Environment.NewLine;
                            outputStr += "pop %eax" + Environment.NewLine;
                            outputStr += "movl $0, %edx" + Environment.NewLine;
                            outputStr += "idivl %ecx, %eax" + Environment.NewLine;
                            outputStr += "movl %edx, %eax" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.BitwiseAnd)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "andl %ecx, %eax" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.BitwiseOr)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "orl %ecx, %eax" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.BitwiseXor)
                        {
                            outputStr += "pop %ecx" + Environment.NewLine;
                            outputStr += "xorl %ecx, %eax" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.BitwiseShiftLeft)
                        {
                            outputStr += "movl %eax, %ecx" + Environment.NewLine;
                            outputStr += "pop %eax" + Environment.NewLine;
                            outputStr += "shl %cl, %eax" + Environment.NewLine;
                        }
                        else if(node.op.type == TokenType.BitwiseShiftRight)
                        {
                            outputStr += "movl %eax, %ecx" + Environment.NewLine;
                            outputStr += "pop %eax" + Environment.NewLine;
                            outputStr += "shr %cl, %eax" + Environment.NewLine;
                        }
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
                case ASTType.Constant:
                    {
                        outputStr += "movl $";
                        outputStr += node.tokValue.value.ToString();
                        outputStr += ", %eax" + Environment.NewLine;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("CodeGen: Unsupported AST Type!!!" + Environment.NewLine);
                        break;
                    }
            }
        }
    }
}