using System;

namespace myc
{
    public class Parser
    {
        /*
            <program> ::= <function>
            <function> ::= "int" <id> "(" ")" "{" <statement> "}"
            <statement> ::= "return" <exp> ";"
            <exp> ::= <term> { ("+" | "-") <term> }
            <term> ::= <factor> { ("*" | "/") <factor> }
            <factor> ::= "(" <exp> ")" | <unary_op> <factor> | <int>
        */

        public Lexer lexer = null;


        public ASTNode Program()
        {
            ASTNode node = new ASTNode();
            node.type = ASTType.Program;
            node.child = FunctionDeclaration();

            return node;
        }

        private ASTNode FunctionDeclaration()
        {
            ASTNode node = new ASTNode();
            node.type = ASTType.Function;

            Expect(TokenType.IntKeyword);
            Token currentToken = Expect(TokenType.Main);
            Token.CopyToken(node.tokValue, currentToken);
            Expect(TokenType.LParen);
            Expect(TokenType.RParen);
            Expect(TokenType.LBrace);

            node.child = Statement();

            Expect(TokenType.RBrace);

            return node;
        }

        private ASTNode Statement()
        {
            Token currentToken = Expect(TokenType.Ret);

            ASTNode node = new ASTNode();
            node.type = ASTType.Return;
            Token.CopyToken(node.tokValue, currentToken);
            node.child = Exp();

            Expect(TokenType.Semi);

            return node;
        }

        private ASTNode Exp()
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
            while (next.type == TokenType.Multiplication || next.type == TokenType.Division)
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
                lexer.Error();
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

            lexer.Error();

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
                        Console.WriteLine("Unsupported AST Type!!!" + Environment.NewLine);
                        break;
                    }
            }
        }
    }
}