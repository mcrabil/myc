using System;

namespace myc
{
    public class Parser
    {
        /*
            <program> ::= <function>
            <function> ::= "int" <id> "(" ")" "{" <statement> "}"
            <statement> ::= "return" <exp> ";"
            <exp> ::= <logical-and-exp> { "||" <logical-and-exp> }
            <logical-and-exp> ::= <bitwise-or-exp> { "&&" <bitwise-or-exp> }
            <bitwise-or-exp> ::= <bitwise-xor-exp> { "|" <bitwise-xor-exp> }
            <bitwise-xor-exp> ::= <bitwise-and-exp> { "^" <bitwise-and-exp> }
            <bitwise-and-exp> ::= <equality-exp> { "&" <equality-exp> }
            <equality-exp> ::= <relational-exp> { ("!=" | "==") <relational-exp> }
            <relational-exp> ::= <bitwise-shift-exp> { ("<" | ">" | "<=" | ">=") <bitwise-shift-exp> }
            <bitwise-shift-exp> ::= <additive-exp> { ("<<" | ">>") <additive-exp> }
            <additive-exp> ::= <term> { ("+" | "-") <term> }
            <term> ::= <factor> { ("*" | "/" | "%") <factor> }
            <factor> ::= "(" <exp> ")" | <unary_op> <factor> | <int>
            <unary_op> ::= "!" | "~" | "-"
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