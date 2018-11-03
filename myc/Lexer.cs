using System;

namespace myc
{
    public class Lexer
    {

        public string text;
        public int totalTextLen = 0;
        public int textPos = 0;
        public Token currentToken = new Token();

        public void PrintToken()
        {
            switch (currentToken.type)
            {
                case TokenType.LBrace:
                    {
                        Console.WriteLine("LBRACE" + Environment.NewLine);
                        break;
                    }
                case TokenType.RBrace:
                    {
                        Console.WriteLine("RBRACE" + Environment.NewLine);
                        break;
                    }
                case TokenType.LParen:
                    {
                        Console.WriteLine("LPAREN" + Environment.NewLine);
                        break;
                    }
                case TokenType.RParen:
                    {
                        Console.WriteLine("RPAREN" + Environment.NewLine);
                        break;
                    }
                case TokenType.Semi:
                    {
                        Console.WriteLine("SEMI" + Environment.NewLine);
                        break;
                    }
                case TokenType.IntKeyword:
                    {
                        Console.WriteLine("INTKEYWORD" + Environment.NewLine);
                        break;
                    }
                case TokenType.Ret:
                    {
                        Console.WriteLine("RET" + Environment.NewLine);
                        break;
                    }
                case TokenType.Identifier:
                    {
                        Console.WriteLine("IDENTIFIER" + Environment.NewLine);
                        break;
                    }
                case TokenType.IntLiteral:
                    {
                        Console.WriteLine("INTLITERAL" + Environment.NewLine);
                        break;
                    }
                case TokenType.Main:
                    {
                        Console.WriteLine("MAIN" + Environment.NewLine);
                        break;
                    }
                case TokenType.Negation:
                    {
                        Console.WriteLine("NEGATION" + Environment.NewLine);
                        break;
                    }
                case TokenType.BitwiseComp:
                    {
                        Console.WriteLine("BITWISE_COMP" + Environment.NewLine);
                        break;
                    }
                case TokenType.LogicalNeg:
                    {
                        Console.WriteLine("LOGICAL_NEG" + Environment.NewLine);
                        break;
                    }
                case TokenType.EOTF:
                    {
                        Console.WriteLine("EOTF" + Environment.NewLine);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Undefined token!" + Environment.NewLine);
                        break;
                    }
            }
        }

        private int MyAtoi()
        {
            int result = 0;
            for (; Char.IsDigit(text[textPos]); textPos++)
            {
                result = (10 * result) + (text[textPos] - '0');
            }

            return result;
        }

        private char Peek()
        {
            int peekPos = textPos + 1;
            if (peekPos > (totalTextLen - 1))
            {
                return (char)0;
            }
            return text[peekPos];

        }

        private void GetIdentifier()
        {
            currentToken.type = TokenType.Identifier;

            int stringLen = 0;
            while (Char.IsLetterOrDigit(text[textPos + stringLen]))
            {
                stringLen++;
            }

            if (string.Equals(text.Substring(textPos, 3), "int"))
            {
                currentToken.type = TokenType.IntKeyword;
            }
            else if (string.Equals(text.Substring(textPos, 4), "main"))
            {
                currentToken.type = TokenType.Main;
            }
            else if (string.Equals(text.Substring(textPos, 6), "return"))
            {
                currentToken.type = TokenType.Ret;
            }
            currentToken.strval = text.Substring(textPos, stringLen);
            textPos += stringLen;
        }

        public void Error()
        {
            Console.WriteLine("THERE WAS AN ERROR PARSING" + Environment.NewLine);
            System.Environment.Exit(1);
        }

        public void GetNextToken()
        {
            //Eat Whitespace
            while (char.IsWhiteSpace(text[textPos]))
            {
                textPos++;
            }

            if (Char.IsLetter(text[textPos]))
            {
                GetIdentifier();
            }
            else if (Char.IsDigit(text[textPos]))
            {
                currentToken.type = TokenType.IntLiteral;
                currentToken.value = MyAtoi();
            }
            else if (text[textPos] == ';')
            {
                currentToken.type = TokenType.Semi;
                currentToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '(')
            {
                currentToken.type = TokenType.LParen;
                currentToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == ')')
            {
                currentToken.type = TokenType.RParen;
                currentToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '{')
            {
                currentToken.type = TokenType.LBrace;
                currentToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '}')
            {
                currentToken.type = TokenType.RBrace;
                currentToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '-')
            {
                currentToken.type = TokenType.Negation;
                currentToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '~')
            {
                currentToken.type = TokenType.BitwiseComp;
                currentToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '!')
            {
                currentToken.type = TokenType.LogicalNeg;
                currentToken.value = 0;
                textPos++;
            }
            else if (textPos == totalTextLen)
            {
                currentToken.type = TokenType.EOTF;
                currentToken.value = 0;
            }
            else
            {
                Error();
            }

        }

        public void PrintAllTokens()
        {
            //Iterate over the tokens
            GetNextToken();
            PrintToken();
            while (currentToken.type != TokenType.EOTF)
            {
                GetNextToken();
                PrintToken();
            }
        }

        private void Eat(TokenType tokenType)
        {
            if (currentToken.type == tokenType)
            {
                GetNextToken();
            }
            else
            {
                Error();
            }
        }
    }
}