using System;

namespace myc
{
    public class Lexer
    {

        public string text;
        public int totalTextLen = 0;
        public int textPos = 0;

        public void PrintToken(Token token)
        {
            Console.WriteLine(token.type.ToString());
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

        public Token PeekNextToken(int numTokens = 1)
        {
            int currTextPos = textPos;
            Token peek = new Token();
            for(int i = 0; i < numTokens && (peek.type != TokenType.EOTF); i++)
            {
                 peek = GetNextToken();
            }
            textPos = currTextPos;
            return peek;
        }

        private Token GetIdentifier()
        {
            Token ident = new Token();
            ident.type = TokenType.Identifier;

            int stringLen = 0;
            while ((textPos + stringLen < totalTextLen) && Char.IsLetterOrDigit(text[textPos + stringLen]))
            {
                stringLen++;
            }

            if ((textPos + 3 < totalTextLen) && (stringLen == 3) && string.Equals(text.Substring(textPos, 3), "int"))
            {
                ident.type = TokenType.IntKeyword;
            }
            else if ((textPos + 4 < totalTextLen) && (stringLen == 4) && string.Equals(text.Substring(textPos, 4), "main"))
            {
                ident.type = TokenType.Main;
            }
            else if ((textPos + 2 < totalTextLen) && (stringLen == 2) && string.Equals(text.Substring(textPos, 2), "if"))
            {
                ident.type = TokenType.IfKeyword;
            }
            else if ((textPos + 4 < totalTextLen) && (stringLen == 4) && string.Equals(text.Substring(textPos, 4), "else"))
            {
                ident.type = TokenType.ElseKeyword;
            }
            else if ((textPos + 6 < totalTextLen) && (stringLen == 6) && string.Equals(text.Substring(textPos, 6), "return"))
            {
                ident.type = TokenType.Ret;
            }
            ident.strval = text.Substring(textPos, stringLen);
            textPos += stringLen;
            return ident;
        }

        public Token GetNextToken()
        {
            Token nextToken = new Token();
            //Eat Whitespace
            while ((textPos < totalTextLen) && char.IsWhiteSpace(text[textPos]))
            {
                textPos++;
            }

            if (textPos == totalTextLen)
            {
                nextToken.type = TokenType.EOTF;
                nextToken.value = 0;
            }
            else if ((textPos + 3 < totalTextLen) && string.Equals(text.Substring(textPos, 3), "for"))
            {
                nextToken.type = TokenType.ForKeyword;
                nextToken.value = 0;
                textPos += 3;
            }
            else if ((textPos + 5 < totalTextLen) && string.Equals(text.Substring(textPos, 5), "while"))
            {
                nextToken.type = TokenType.WhileKeyword;
                nextToken.value = 0;
                textPos += 5;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "do"))
            {
                nextToken.type = TokenType.DoKeyword;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 5 < totalTextLen) && string.Equals(text.Substring(textPos, 5), "break"))
            {
                nextToken.type = TokenType.BreakKeyword;
                nextToken.value = 0;
                textPos += 5;
            }
            else if ((textPos + 8 < totalTextLen) && string.Equals(text.Substring(textPos, 8), "continue"))
            {
                nextToken.type = TokenType.ContinueKeyword;
                nextToken.value = 0;
                textPos += 8;
            }
            else if (Char.IsLetter(text[textPos]))
            {
                Token ident = GetIdentifier();
                nextToken.type = ident.type;
                nextToken.value = ident.value;
                nextToken.strval = ident.strval;
            }
            else if (Char.IsDigit(text[textPos]))
            {
                nextToken.type = TokenType.IntLiteral;
                nextToken.value = MyAtoi();
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "+="))
            {
                nextToken.type = TokenType.AdditionAssignment;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "-="))
            {
                nextToken.type = TokenType.MinusAssignment;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "/="))
            {
                nextToken.type = TokenType.DivisionAssignment;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "*="))
            {
                nextToken.type = TokenType.MultiplicationAssignment;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 3 < totalTextLen) && string.Equals(text.Substring(textPos, 3), "<<="))
            {
                nextToken.type = TokenType.BitShiftLeftAssignment;
                nextToken.value = 0;
                textPos += 3;
            }
            else if ((textPos + 3 < totalTextLen) && string.Equals(text.Substring(textPos, 3), ">>="))
            {
                nextToken.type = TokenType.BitShiftRightAssignment;
                nextToken.value = 0;
                textPos += 3;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "&="))
            {
                nextToken.type = TokenType.BitwiseAndAssignment;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "|="))
            {
                nextToken.type = TokenType.BitwiseOrAssignment;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "^="))
            {
                nextToken.type = TokenType.BitwiseXorAssignment;
                nextToken.value = 0;
                textPos += 2;
            }
            else if (text[textPos] == ';')
            {
                nextToken.type = TokenType.Semi;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '(')
            {
                nextToken.type = TokenType.LParen;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == ')')
            {
                nextToken.type = TokenType.RParen;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '{')
            {
                nextToken.type = TokenType.LBrace;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '}')
            {
                nextToken.type = TokenType.RBrace;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '-')
            {
                nextToken.type = TokenType.Minus;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '~')
            {
                nextToken.type = TokenType.BitwiseComp;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '+')
            {
                nextToken.type = TokenType.Addition;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '*')
            {
                nextToken.type = TokenType.Multiplication;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '/')
            {
                nextToken.type = TokenType.Division;
                nextToken.value = 0;
                textPos++;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "&&"))
            {
                nextToken.type = TokenType.LogicalAnd;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "||"))
            {
                nextToken.type = TokenType.LogicalOr;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "=="))
            {
                nextToken.type = TokenType.Equal;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "!="))
            {
                nextToken.type = TokenType.NotEqual;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "<="))
            {
                nextToken.type = TokenType.LessThanOrEqual;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), ">="))
            {
                nextToken.type = TokenType.GreaterThanOrEqual;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), "<<"))
            {
                nextToken.type = TokenType.BitwiseShiftLeft;
                nextToken.value = 0;
                textPos += 2;
            }
            else if ((textPos + 2 < totalTextLen) && string.Equals(text.Substring(textPos, 2), ">>"))
            {
                nextToken.type = TokenType.BitwiseShiftRight;
                nextToken.value = 0;
                textPos += 2;
            }
            else if (text[textPos] == '%')
            {
                nextToken.type = TokenType.Modulo;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '&')
            {
                nextToken.type = TokenType.BitwiseAnd;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '|')
            {
                nextToken.type = TokenType.BitwiseOr;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '^')
            {
                nextToken.type = TokenType.BitwiseXor;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '!')
            {
                nextToken.type = TokenType.LogicalNeg;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '<')
            {
                nextToken.type = TokenType.LessThan;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '>')
            {
                nextToken.type = TokenType.GreaterThan;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '=')
            {
                nextToken.type = TokenType.Assignment;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == ':')
            {
                nextToken.type = TokenType.Colon;
                nextToken.value = 0;
                textPos++;
            }
            else if (text[textPos] == '?')
            {
                nextToken.type = TokenType.QuestionMark;
                nextToken.value = 0;
                textPos++;
            }
            else
            {
                Program.Error();
            }
            return nextToken;
        }

        public void PrintAllTokens()
        {
            textPos = 0;

            //Iterate over the tokens
            Token currentToken = GetNextToken();
            PrintToken(currentToken);
            while (currentToken.type != TokenType.EOTF)
            {
                currentToken = GetNextToken();
                PrintToken(currentToken);
            }
            textPos = 0;
        }
    }
}