using System;

namespace myc
{
    public class Parser
    {
        /*
            <program> ::= <function>
            <function> ::= "int" <id> "(" ")" "{" <statement> "}"
            <statement> ::= "return" <exp> ";"
            <exp> ::= <unary_op> <exp> | <int>
            <unary_op> ::= "!" | "~" | "-"
        */

        public Lexer lexer = null;

        private void CopyToken(ASTNode node)
        {
            node.tokValue.type = lexer.currentToken.type;
            node.tokValue.value = lexer.currentToken.value;
            node.tokValue.strval = lexer.currentToken.strval;
        }

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
            Expect(TokenType.Main);
            CopyToken(node);
            Expect(TokenType.LParen);
            Expect(TokenType.RParen);
            Expect(TokenType.LBrace);

            node.child = Statement();

            Expect(TokenType.RBrace);

            return node;
        }

        private ASTNode Statement()
        {
            Expect(TokenType.Ret);

            ASTNode node = new ASTNode();
            node.type = ASTType.Return;
            CopyToken(node);
            node.child = Exp();

            Expect(TokenType.Semi);

            return node;
        }

        private ASTNode Exp()
        {
            lexer.GetNextToken();
            ASTNode node = new ASTNode();
            if (lexer.currentToken.type == TokenType.IntLiteral)
            {
                node.type = ASTType.Constant;
                CopyToken(node);
                node.child = null;
            }
            else if (lexer.currentToken.type == TokenType.Negation)
            {
                node.type = ASTType.UnOp;
                CopyToken(node);
                node.child = Exp();
            }
            else if (lexer.currentToken.type == TokenType.BitwiseComp)
            {
                node.type = ASTType.UnOp;
                CopyToken(node);
                node.child = Exp();
            }
            else if (lexer.currentToken.type == TokenType.LogicalNeg)
            {
                node.type = ASTType.UnOp;
                CopyToken(node);
                node.child = Exp();
            }
            else
            {
                lexer.Error();
            }

            return node;
        }

        private int Expect(TokenType type)
        {
            lexer.GetNextToken();
            if (lexer.currentToken.type == type)
            {
                return 1;
            }

            lexer.Error();

            return 0;
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
                        if (node.tokValue.type == TokenType.Negation)
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