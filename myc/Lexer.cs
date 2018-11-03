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

        public Token PeekNextToken()
        {
            int currTextPos = textPos;
            Token peek = GetNextToken();
            textPos = currTextPos;
            return peek;
        }

        private Token GetIdentifier()
        {
            Token ident = new Token();
            ident.type = TokenType.Identifier;

            int stringLen = 0;
            while (Char.IsLetterOrDigit(text[textPos + stringLen]))
            {
                stringLen++;
            }

            if (string.Equals(text.Substring(textPos, 3), "int"))
            {
                ident.type = TokenType.IntKeyword;
            }
            else if (string.Equals(text.Substring(textPos, 4), "main"))
            {
                ident.type = TokenType.Main;
            }
            else if (string.Equals(text.Substring(textPos, 6), "return"))
            {
                ident.type = TokenType.Ret;
            }
            ident.strval = text.Substring(textPos, stringLen);
            textPos += stringLen;
            return ident;
        }

        public void Error()
        {
            Console.WriteLine("THERE WAS AN ERROR PARSING" + Environment.NewLine);
            System.Environment.Exit(1);
        }

        public Token GetNextToken()
        {
            Token nextToken = new Token();
            //Eat Whitespace
            while (char.IsWhiteSpace(text[textPos]))
            {
                textPos++;
            }

            if (Char.IsLetter(text[textPos]))
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
            else if (text[textPos] == '!')
            {
                nextToken.type = TokenType.LogicalNeg;
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
            else if (textPos == totalTextLen)
            {
                nextToken.type = TokenType.EOTF;
                nextToken.value = 0;
            }
            else
            {
                Error();
            }
            return nextToken;
        }

        public void PrintAllTokens()
        {
            //Iterate over the tokens
            Token currentToken = GetNextToken();
            PrintToken(currentToken);
            while (currentToken.type != TokenType.EOTF)
            {
                currentToken = GetNextToken();
                PrintToken(currentToken);
            }
        }
    }
}