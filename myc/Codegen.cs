using System;
using System.Collections.Generic;

namespace myc
{
    public class Codegen
    {
        public int branchCounterLabel = 0;
        public string outputStr = "";

        public void ScopeBegin()
        {
            Program.varmap.Add(new Dictionary<string, int>());
            Program.scopeidx++;
        }

        public void ScopeEnd()
        {
            outputStr += "addl $" + (4 * Program.varmap[Program.scopeidx].Count).ToString() + ", %esp" + Environment.NewLine; 
            Program.varmap.RemoveAt(Program.scopeidx);
            Program.scopeidx--;
        }

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

                        ScopeBegin();
                        foreach(var block_item in node.block_items)
                        {
                            Generate(block_item);
                        }

                        ScopeEnd();
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
                        if (Program.varmap[Program.scopeidx].ContainsKey(node.ident.strval))
                        {
                            Program.Error("There was already a variable declared " + node.ident.strval);
                        }
                        Program.varmap[Program.scopeidx].Add(node.ident.strval, Program.NextVarMapIdx());

                        if(node.child != null)
                        {
                            Generate(node.child);
                        }

                        outputStr += "push %eax" + Environment.NewLine;
                        break;
                    }
                case ASTType.Assign:
                    {
                        if (!Program.VarMapContainsVar(node.ident.strval))
                        {
                            Program.Error("Variable has not been declared " + node.ident.strval);
                        }
                        if((node.tokValue.type >= TokenType.AdditionAssignment) && (node.tokValue.type <= TokenType.BitwiseXorAssignment))
                        {
                            outputStr += "movl " + Program.VarMapLookup(node.ident.strval) + "(%ebp), %eax" + Environment.NewLine;
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

                        outputStr += "movl %eax, " + Program.VarMapLookup(node.ident.strval) + "(%ebp)" + Environment.NewLine;
                        break;
                    }
                case ASTType.Var:
                    {
                        if (!Program.VarMapContainsVar(node.tokValue.strval))
                        {
                            Program.Error("Variable has not been declared " + node.tokValue.strval);
                        }
                        outputStr += "movl " + Program.VarMapLookup(node.tokValue.strval) + "(%ebp), %eax" + Environment.NewLine;
                        break;
                    }
                case ASTType.Compound:
                    {
                        ScopeBegin();
                        foreach(var block_item in node.block_items)
                        {
                            Generate(block_item);
                        }
                        ScopeEnd();
                        break;
                    }
                case ASTType.ConditionalStatement:
                    {
                        Generate(node.child);
                        branchCounterLabel++;
                        string label = branchCounterLabel.ToString();
                        outputStr += "cmpl $0, %eax" + Environment.NewLine;
                        outputStr += "je _elsebranch_" + label + Environment.NewLine;
                        Generate(node.ifStatement);
                        outputStr += "jmp _post_conditional_stmt_" + label + Environment.NewLine;
                        outputStr += "_elsebranch_" + label + ":" + Environment.NewLine;
                        if(node.elseStatement != null)
                        {
                            Generate(node.elseStatement);
                        }
                        outputStr += "_post_conditional_stmt_" + label + ":" + Environment.NewLine;
                        break;
                    }
                case ASTType.ConditionalExpression:
                    {
                        Generate(node.child);
                        branchCounterLabel++;
                        string label = branchCounterLabel.ToString();
                        outputStr += "cmpl $0, %eax" + Environment.NewLine;
                        outputStr += "je _else_" + label + Environment.NewLine;
                        Generate(node.ifExpr);
                        outputStr += "jmp _post_conditional_expr_" + label + Environment.NewLine;
                        outputStr += "_else_" + label + ":" + Environment.NewLine;
                        Generate(node.elseExpr);
                        outputStr += "_post_conditional_expr_" + label + ":" + Environment.NewLine;
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
                case ASTType.WhileStatement:
                    {
                        branchCounterLabel++;
                        string label = branchCounterLabel.ToString();
                        outputStr += "_while_start_" + label + ":" + Environment.NewLine;
                        Generate(node.whileCondition);
                        outputStr += "cmpl $0, %eax" + Environment.NewLine;
                        outputStr += "je _while_end" + label + Environment.NewLine;
                        Generate(node.whileBody);
                        outputStr += "jmp _while_start_" + label + Environment.NewLine;
                        outputStr += "_while_end" + label + ":" + Environment.NewLine;
                        break;
                    }
                case ASTType.DoStatement:
                    {
                        branchCounterLabel++;
                        string label = branchCounterLabel.ToString();
                        outputStr += "_do_start_" + label + ":" + Environment.NewLine;
                        Generate(node.doBody);
                        Generate(node.doCondition);
                        outputStr += "cmpl $0, %eax" + Environment.NewLine;
                        outputStr += "jne _do_start" + label + Environment.NewLine;
                        break;
                    }
                case ASTType.ForStatement:
                    {
                        ScopeBegin();

                        branchCounterLabel++;
                        string label = branchCounterLabel.ToString();
                        Generate(node.forInitial);
                        outputStr += "_for_start_" + label + ":" + Environment.NewLine;
                        Generate(node.forCondition);
                        outputStr += "cmpl $0, %eax" + Environment.NewLine;
                        outputStr += "je _for_end" + label + Environment.NewLine;
                        Generate(node.forBody);
                        Generate(node.forPostExpr);
                        outputStr += "jmp _for_start_" + label + Environment.NewLine;
                        outputStr += "_for_end" + label + ":" + Environment.NewLine;

                        ScopeEnd();
                        break;
                    }
                case ASTType.Break:
                    {
                        //TODO: implement!
                        break;
                    }
                case ASTType.Continue:
                    {
                        //TODO: implement!
                        break;
                    }
                case ASTType.NullStatement:
                    {
                        //Don't do anything!
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