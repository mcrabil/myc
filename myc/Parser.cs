using System;
using System.Collections.Generic;

namespace myc
{
    public class Parser
    {
        /*
            <program> ::= <function>
            <function> ::= "int" <id> "(" ")" "{" { <block-item> } "}"
            <block-item> ::= <statement> | <declaration>
            <statement> ::= "return" <exp> ";"
                          | <exp-option> ";"
                          | "if" "(" <exp> ")" <statement> [ "else" <statement> ]
                          | "{" { <block-item> } "}
                          | "for" "(" <exp-option> ";" <exp-option> ";" <exp-option> ")" <statement>
                          | "for" "(" <declaration> <exp-option> ";" <exp-option> ")" <statement>
                          | "while" "(" <exp> ")" <statement>
                          | "do" <statement> "while" <exp> ";"
                          | "break" ";"
                          | "continue" ";"
            <exp-option> ::= <exp> | ""
            <declaration> ::= "int" <id> [ = <exp> ] ";"
            <exp> ::= <id> "=" <exp> | <conditional-exp>
            <conditional-exp> ::= <logical-or-exp> [ "?" <exp> ":" <conditional-exp> ]
            <logical-or-exp> ::= <logical-and-exp> { "||" <logical-and-exp> } 
            <logical-and-exp> ::= <bitwise-or-exp> { "&&" <bitwise-or-exp> }
            <bitwise-or-exp> ::= <bitwise-xor-exp> { "|" <bitwise-xor-exp> }
            <bitwise-xor-exp> ::= <bitwise-and-exp> { "^" <bitwise-and-exp> }
            <bitwise-and-exp> ::= <equality-exp> { "&" <equality-exp> }
            <equality-exp> ::= <relational-exp> { ("!=" | "==") <relational-exp> }
            <relational-exp> ::= <bitwise-shift-exp> { ("<" | ">" | "<=" | ">=") <bitwise-shift-exp> }
            <bitwise-shift-exp> ::= <additive-exp> { ("<<" | ">>") <additive-exp> }
            <additive-exp> ::= <term> { ("+" | "-") <term> }
            <term> ::= <factor> { ("*" | "/" | "%") <factor> }
            <factor> ::= "(" <exp> ")" | <unary_op> <factor> | <int> | <id>
            <unary_op> ::= "!" | "~" | "-"
        */

        public Lexer lexer = null;


        public ASTNode Prog()
        {
            ASTNode node = new ASTNode();
            node.type = ASTType.Program;
            node.child = FunctionDeclaration();

            Expect(TokenType.EOTF);

            return node;
        }

        private ASTNode FunctionDeclaration()
        {
            Program.varmap.Add(new Dictionary<string, int>());
            Program.scopeidx++;
            bool hasReturn = false;
            ASTNode node = new ASTNode();
            node.type = ASTType.Function;

            Expect(TokenType.IntKeyword);
            Token currentToken = Expect(TokenType.Main);
            Token.CopyToken(node.tokValue, currentToken);
            Expect(TokenType.LParen);
            Expect(TokenType.RParen);
            Expect(TokenType.LBrace);

            node.block_items = new List<ASTNode>();
            Token next = lexer.PeekNextToken();
            while(next.type != TokenType.RBrace)
            {
                ASTNode stmt = BlockItem();
                node.block_items.Add(stmt);
                if(stmt.type == ASTType.Declare)
                {
                    if (Program.varmap[Program.scopeidx].ContainsKey(stmt.ident.strval))
                    {
                        Program.Error("There was already a variable declared " + stmt.ident.strval);
                    }
                    Program.varmap[Program.scopeidx].Add(stmt.ident.strval, Program.NextVarMapIdx());
                }
                else if(stmt.type == ASTType.Assign)
                {
                    if (!Program.VarMapContainsVar(stmt.ident.strval))
                    {
                        Program.Error("Variable has not been declared " + stmt.ident.strval);
                    }

                }
                else if(stmt.type == ASTType.Return)
                {
                    hasReturn = true;
                }
                next = lexer.PeekNextToken();
            }
            //Add in a return for main if one doesn't exist
            if(!hasReturn && node.tokValue.type == TokenType.Main)
            {
                ASTNode retNode = new ASTNode();
                retNode.type = ASTType.Return;
                retNode.tokValue = new Token();
                retNode.tokValue.type = TokenType.Ret;
                retNode.child = new ASTNode();
                retNode.child.type = ASTType.Constant;
                retNode.child.tokValue = new Token();
                retNode.child.tokValue.type = TokenType.IntLiteral;
                retNode.child.tokValue.value = 0;
                node.block_items.Add(retNode);
            }

            Expect(TokenType.RBrace);
            Program.varmap.RemoveAt(Program.scopeidx);
            Program.scopeidx--;

            return node;
        }

        private ASTNode BlockItem()
        {
            Token next = lexer.PeekNextToken();
            ASTNode node = new ASTNode();
            if (next.type == TokenType.IntKeyword)
            {
                node = VarDeclaration();
            }
            else
            {
                node = Statement();
            }

            return node;
        }

        private ASTNode Statement()
        {
            Token next = lexer.PeekNextToken();
            ASTNode node = new ASTNode();
            if (next.type == TokenType.Ret)
            {
                Expect(TokenType.Ret);

                node.type = ASTType.Return;
                Token.CopyToken(node.tokValue, next);
                node.child = Exp();

                Expect(TokenType.Semi);
            }
            else if (next.type == TokenType.IfKeyword)
            {
                Expect(TokenType.IfKeyword);
                Expect(TokenType.LParen);
                node.type = ASTType.ConditionalStatement;
                node.child = Exp();
                Expect(TokenType.RParen);
                node.ifStatement = Statement();
                next = lexer.PeekNextToken();
                if(next.type == TokenType.ElseKeyword)
                {
                    Expect(TokenType.ElseKeyword);
                    node.elseStatement = Statement();
                }
            }
            else if (next.type == TokenType.LBrace)
            {
                Program.varmap.Add(new Dictionary<string, int>());
                Program.scopeidx++;
                Expect(TokenType.LBrace);
                node.type = ASTType.Compound;
                node.block_items = new List<ASTNode>();
                next = lexer.PeekNextToken();
                while(next.type != TokenType.RBrace)
                {
                    ASTNode stmt = BlockItem();
                    node.block_items.Add(stmt);
                    if(stmt.type == ASTType.Declare)
                    {
                        if (Program.varmap[Program.scopeidx].ContainsKey(stmt.ident.strval))
                        {
                            Program.Error("There was already a variable declared " + stmt.ident.strval);
                        }
                        Program.varmap[Program.scopeidx].Add(stmt.ident.strval, Program.NextVarMapIdx());
                    }
                    else if(stmt.type == ASTType.Assign)
                    {
                        if (!Program.VarMapContainsVar(stmt.ident.strval))
                        {
                            Program.Error("Variable has not been declared " + stmt.ident.strval);
                        }

                    }
                    next = lexer.PeekNextToken();
                }
                Expect(TokenType.RBrace);
                Program.varmap.RemoveAt(Program.scopeidx);
                Program.scopeidx--;
            }
            else if (next.type == TokenType.ForKeyword)
            {
                Expect(TokenType.ForKeyword);
                Expect(TokenType.LParen);
                node.type = ASTType.ForStatement;

                //Initial exp/decl
                next = lexer.PeekNextToken();
                if (next.type == TokenType.IntKeyword)
                {
                    node.type = ASTType.ForStatement;
                    node.forInitial = VarDeclaration();
                }
                else if (next.type == TokenType.Semi)
                {
                    ASTNode constNode = new ASTNode();
                    constNode.type = ASTType.Constant;
                    constNode.tokValue = new Token();
                    constNode.tokValue.value = 1;
                    node.forInitial = constNode;
                    Expect(TokenType.Semi);
                }
                else
                {
                    node.forInitial = Exp();
                    Expect(TokenType.Semi);
                }

                //For loop condition
                next = lexer.PeekNextToken();
                if (next.type == TokenType.Semi)
                {
                    ASTNode constNode = new ASTNode();
                    constNode.type = ASTType.Constant;
                    constNode.tokValue = new Token();
                    constNode.tokValue.value = 1;
                    node.forCondition = constNode;
                    Expect(TokenType.Semi);
                }
                else
                {
                    node.forCondition = Exp();
                    Expect(TokenType.Semi);
                }

                //For loop post expr
                next = lexer.PeekNextToken();
                if (next.type == TokenType.RParen)
                {
                    ASTNode constNode = new ASTNode();
                    constNode.type = ASTType.Constant;
                    constNode.tokValue = new Token();
                    constNode.tokValue.value = 1;
                    node.forPostExpr = constNode;
                    Expect(TokenType.RParen);
                }
                else
                {
                    node.forPostExpr = Exp();
                    Expect(TokenType.RParen);
                }

                node.forBody = Statement();
            }
            else if (next.type == TokenType.WhileKeyword)
            {
                Expect(TokenType.WhileKeyword);
                Expect(TokenType.LParen);
                node.type = ASTType.WhileStatement;

                //While loop condition
                node.whileCondition = Exp();
                Expect(TokenType.RParen);

                node.whileBody = Statement();
            }
            else if (next.type == TokenType.DoKeyword)
            {
                Expect(TokenType.DoKeyword);
                node.doBody = Statement();
                node.type = ASTType.DoStatement;
                Expect(TokenType.WhileKeyword);
                node.doCondition = Exp();
                Expect(TokenType.Semi);
            }
            else if (next.type == TokenType.BreakKeyword)
            {
                Expect(TokenType.BreakKeyword);
                Expect(TokenType.Semi);
                node.type = ASTType.Break;
            }
            else if (next.type == TokenType.ContinueKeyword)
            {
                Expect(TokenType.ContinueKeyword);
                Expect(TokenType.Semi);
                node.type = ASTType.Continue;
            }
            else if (next.type == TokenType.Semi)
            {
                node.type = ASTType.NullStatement;
                Expect(TokenType.Semi);
            }
            else
            {
                node = Exp();
                Expect(TokenType.Semi);
            }

            return node;
        }

        private ASTNode VarDeclaration()
        {
            Token next = lexer.PeekNextToken();
            ASTNode node = new ASTNode();
            if (next.type == TokenType.IntKeyword)
            {
                Expect(TokenType.IntKeyword);
                Token ident = Expect(TokenType.Identifier);
                next = lexer.PeekNextToken();
                if((next.type >= TokenType.Assignment) && (next.type <= TokenType.BitwiseXorAssignment))
                {
                    Expect(next.type);
                    node.child = Exp();
                    node.ident = ident;
                    node.tokValue.type = next.type;
                    node.type = ASTType.Declare;
                    Expect(TokenType.Semi);
                }
                else if (next.type == TokenType.Semi)
                {
                    node.ident = ident;
                    node.type = ASTType.Declare;
                    Expect(TokenType.Semi);
                }
                else
                {
                    Program.Error();
                }
            }
            else
            {
                Program.Error();
            }

            return node;
        }

        private ASTNode Exp()
        {
            ASTNode node = new ASTNode();
            Token next = lexer.PeekNextToken();
            Token next2 = lexer.PeekNextToken(2);
            if (next.type == TokenType.Identifier && (next2.type >= TokenType.Assignment) && (next2.type <= TokenType.BitwiseXorAssignment))
            {
                Expect(TokenType.Identifier);
                Expect(next2.type);
                node.child = Exp();
                node.ident = next;
                node.tokValue.type = next2.type;
                node.type = ASTType.Assign;
            }
            else
            {
                node = ConditionalExp();
            }
            return node;
        }

        private ASTNode ConditionalExp()
        {
            ASTNode node = new ASTNode();
            ASTNode logicalOrExp = LogicalOrExp();
            Token next = lexer.PeekNextToken();
            if (next.type == TokenType.QuestionMark)
            {
                Expect(TokenType.QuestionMark);
                ASTNode ifExp = Exp();
                Expect(TokenType.Colon);
                ASTNode elseExp = ConditionalExp();
                node.child = logicalOrExp;
                node.type = ASTType.ConditionalExpression;
                node.ifExpr = ifExp;
                node.elseExpr = elseExp;
            }
            else
            {
                node = logicalOrExp;
            }
            return node;
        }

        private ASTNode LogicalOrExp()
        {
            ASTNode logicalAndExp = LogicalAndExp();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.LogicalOr)
            {
                Token op = lexer.GetNextToken();
                ASTNode nextLogicalAndExp = LogicalAndExp();
                logicalAndExp = new ASTNode(op, logicalAndExp, nextLogicalAndExp, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return logicalAndExp;
        }

        private ASTNode LogicalAndExp()
        {
            ASTNode bitwiseOrExp = BitwiseOrExp();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.LogicalAnd)
            {
                Token op = lexer.GetNextToken();
                ASTNode nextBitwiseOrExp = BitwiseOrExp();
                bitwiseOrExp = new ASTNode(op, bitwiseOrExp, nextBitwiseOrExp, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return bitwiseOrExp;
        }

        private ASTNode BitwiseOrExp()
        {
            ASTNode bitwiseXorExp = BitwiseXorExp();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.BitwiseOr)
            {
                Token op = lexer.GetNextToken();
                ASTNode nextBitwiseOrExp = BitwiseXorExp();
                bitwiseXorExp = new ASTNode(op, bitwiseXorExp, nextBitwiseOrExp, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return bitwiseXorExp;
        }

        private ASTNode BitwiseXorExp()
        {
            ASTNode bitwiseAndExp = BitwiseAndExp();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.BitwiseXor)
            {
                Token op = lexer.GetNextToken();
                ASTNode nextBitwiseAndExp = BitwiseAndExp();
                bitwiseAndExp = new ASTNode(op, bitwiseAndExp, nextBitwiseAndExp, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return bitwiseAndExp;
        }

        private ASTNode BitwiseAndExp()
        {
            ASTNode equalityExp = EqualityExp();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.BitwiseAnd)
            {
                Token op = lexer.GetNextToken();
                ASTNode nextEqualityExp = EqualityExp();
                equalityExp = new ASTNode(op, equalityExp, nextEqualityExp, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return equalityExp;
        }

        private ASTNode EqualityExp()
        {
            ASTNode realationalExp = RelationalExp();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.NotEqual || next.type == TokenType.Equal)
            {
                Token op = lexer.GetNextToken();
                ASTNode nextRelationalExp = RelationalExp();
                realationalExp = new ASTNode(op, realationalExp, nextRelationalExp, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return realationalExp;
        }

        private ASTNode RelationalExp()
        {
            ASTNode bitwiseShiftExp = BitwiseShiftExp();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.LessThan || next.type == TokenType.GreaterThan || next.type == TokenType.LessThanOrEqual || next.type == TokenType.GreaterThanOrEqual)
            {
                Token op = lexer.GetNextToken();
                ASTNode nextBitwiseShiftExp = BitwiseShiftExp();
                bitwiseShiftExp = new ASTNode(op, bitwiseShiftExp, nextBitwiseShiftExp, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return bitwiseShiftExp;
        }

        private ASTNode BitwiseShiftExp()
        {
            ASTNode additiveExp = AdditiveExp();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.BitwiseShiftLeft || next.type == TokenType.BitwiseShiftRight)
            {
                Token op = lexer.GetNextToken();
                ASTNode nextAdditiveExp = AdditiveExp();
                additiveExp = new ASTNode(op, additiveExp, nextAdditiveExp, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return additiveExp;
        }

        private ASTNode AdditiveExp()
        {
            ASTNode term = Term();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.Addition || next.type == TokenType.Minus)
            {
                Token op = lexer.GetNextToken();
                ASTNode nextTerm = Term();
                term = new ASTNode(op, term, nextTerm, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return term;
        }

        private ASTNode Term()
        {
            ASTNode factor = Factor();
            Token next = lexer.PeekNextToken();
            while (next.type == TokenType.Multiplication || next.type == TokenType.Division || next.type == TokenType.Modulo)
            {
                Token op = lexer.GetNextToken();
                Token.CopyToken(op, next);
                ASTNode nextFactor = Factor();
                factor = new ASTNode(op, factor, nextFactor, ASTType.BinOp);
                next = lexer.PeekNextToken();
            }
            return factor;
        }

        private ASTNode Factor()
        {
            Token currentToken = lexer.GetNextToken();
            ASTNode node = new ASTNode();
            if(currentToken.type == TokenType.LParen)
            {
                node = Exp();
                Expect(TokenType.RParen);
            }
            else if (currentToken.type == TokenType.IntLiteral)
            {
                node.type = ASTType.Constant;
                Token.CopyToken(node.tokValue, currentToken);
                node.child = null;
            }
            else if (currentToken.type == TokenType.Identifier)
            {
                node.type = ASTType.Var;
                Token.CopyToken(node.tokValue, currentToken);
                node.child = null;
            }
            else if (currentToken.type == TokenType.Minus)
            {
                node.type = ASTType.UnOp;
                Token.CopyToken(node.tokValue, currentToken);
                node.child = Factor();
            }
            else if (currentToken.type == TokenType.BitwiseComp)
            {
                node.type = ASTType.UnOp;
                Token.CopyToken(node.tokValue, currentToken);
                node.child = Factor();
            }
            else if (currentToken.type == TokenType.LogicalNeg)
            {
                node.type = ASTType.UnOp;
                Token.CopyToken(node.tokValue, currentToken);
                node.child = Factor();
            }
            else
            {
                Program.Error();
            }

            return node;
        }

        private Token Expect(TokenType type)
        {
            Token currentToken = lexer.GetNextToken();
            if (currentToken.type == type)
            {
                return currentToken;
            }

            Program.Error();

            return currentToken;
        }

        public void PrettyPrintAST(ASTNode node)
        {
            switch (node.type)
            {
                case ASTType.Program:
                    {
                        PrettyPrintAST(node.child);
                        break;
                    }
                case ASTType.Function:
                    {
                        Console.WriteLine("FUN INT " + node.tokValue.strval + ":");
                        Console.WriteLine("    " + "params: ()");
                        Console.WriteLine("    " + "body:");
                        PrettyPrintAST(node.child);
                        Console.WriteLine();
                        break;
                    }
                case ASTType.Return:
                    {
                        Console.Write("    " + "    " + "RETURN ");
                        PrettyPrintAST(node.child);
                        break;
                    }
                case ASTType.Constant:
                    {
                        Console.Write("Int<" + node.tokValue.value.ToString() + ">");
                        break;
                    }
                case ASTType.UnOp:
                    {
                        if (node.tokValue.type == TokenType.Minus)
                        {
                            Console.Write("NEG<");
                            PrettyPrintAST(node.child);
                            Console.Write(">");
                        }
                        else if (node.tokValue.type == TokenType.BitwiseComp)
                        {
                            Console.Write("NOT<");
                            PrettyPrintAST(node.child);
                            Console.Write(">");
                        }
                        else if (node.tokValue.type == TokenType.LogicalNeg)
                        {
                            Console.Write("LOG<");
                            PrettyPrintAST(node.child);
                            Console.Write(">");
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("PrettyPrint: Unsupported AST Type!!!" + Environment.NewLine);
                        break;
                    }
            }
        }
    }
}