#pragma once

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

enum Token_Type
{
    TOK_LBRACE,
    TOK_RBRACE,
    TOK_LPAREN,
    TOK_RPAREN,
    TOK_SEMI,
    TOK_INTKEYWORD,
    TOK_RET,
    TOK_IDENTIFIER,
    TOK_INTLITERAL,
    TOK_MAIN,
    TOK_EOTF,
};

struct Token
{
    int type; //Token_Type

              //possible values
    int value;
    //char *strval;
};

class Lexer
{
public:

    char *text = (char *)"int main ( ) { return 2; }";
    int totalTextLen = 0;
    int textPos = 0;
    Token currentToken = {};

    int isDigit(char c)
    {
        return (c >= '0' && c <= '9');
    }

    int isAlNum(char c)
    {
        return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
    }

    int isAlpha(char c)
    {
        return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
    }

    void print_token()
    {
        switch (currentToken.type)
        {
            case TOK_LBRACE:
            {
                printf("LBRACE\n");
            } break;
            case TOK_RBRACE:
            {
                printf("RBRACE\n");
            } break;
            case TOK_LPAREN:
            {
                printf("LPAREN\n");
            } break;
            case TOK_RPAREN:
            {
                printf("RPAREN\n");
            } break;
            case TOK_SEMI:
            {
                printf("SEMI\n");
            } break;
            case TOK_INTKEYWORD:
            {
                printf("INTKEYWORD\n");
            } break;
            case TOK_RET:
            {
                printf("RET\n");
            } break;
            case TOK_IDENTIFIER:
            {
                printf("IDENTIFIER\n");
            } break;
            case TOK_INTLITERAL:
            {
                printf("INTLITERAL\n");
            } break;
            case TOK_MAIN:
            {
                printf("MAIN\n");
            } break;
            case TOK_EOTF:
            {
                printf("EOTF\n");
            } break;
            default:
            {
                printf("Undefined token!\n");
            } break;
        }
    }

    int myatoi()
    {
        int result = 0;
        int i = 0;
        for (i = 0; (isDigit(text[textPos])); i++, textPos++)
        {
            result = (10 * result) + (text[textPos] - '0');
        }

        return result;
    }

    /*char peek()
    {
        int peekPos = textPos + 1;
        if (peekPos > (totalTextLen - 1))
        {
            return 0;
        }
        return text[peekPos];

    }*/

    void get_identifier()
    {
        currentToken.type = TOK_IDENTIFIER;

        int stringLen = 0;
        while (isAlNum(text[textPos + stringLen]))
        {
            stringLen++;
        }

        if (!strncmp(text + textPos, "int", 3))
        {
            currentToken.type = TOK_INTKEYWORD;
        }
        else if (!strncmp(text + textPos, "main", 4))
        {
            currentToken.type = TOK_MAIN;
        }
        else if (!strncmp(text + textPos, "return", 6))
        {
            currentToken.type = TOK_RET;
        }
        /*else
        {
            currentToken.strval = (char *)malloc((stringLen + 1) * sizeof(char));

            strncpy_s(currentToken.strval, stringLen + 1, text + textPos, stringLen);
            currentToken.strval[stringLen] = 0;
        }*/
        textPos += stringLen;
    }

    void error()
    {
        printf("THERE WAS AN ERROR PARSING\n");
        exit(0);
    }

    void getNextToken()
    {
        //Eat Whitespace
        while ((text[textPos] == ' ') || (text[textPos] == '\t') || (text[textPos] == '\n'))
        {
            textPos++;
        }

        if (isAlpha(text[textPos]))
        {
            get_identifier();
        }
        else if (isDigit(text[textPos]))
        {
            currentToken.type = TOK_INTLITERAL;
            currentToken.value = myatoi();
        }
        else if (text[textPos] == ';')
        {
            currentToken.type = TOK_SEMI;
            currentToken.value = 0;
            textPos++;
        }
        else if (text[textPos] == '(')
        {
            currentToken.type = TOK_LPAREN;
            currentToken.value = 0;
            textPos++;
        }
        else if (text[textPos] == ')')
        {
            currentToken.type = TOK_RPAREN;
            currentToken.value = 0;
            textPos++;
        }
        else if (text[textPos] == '{')
        {
            currentToken.type = TOK_LBRACE;
            currentToken.value = 0;
            textPos++;
        }
        else if (text[textPos] == '}')
        {
            currentToken.type = TOK_RBRACE;
            currentToken.value = 0;
            textPos++;
        }
        else if (textPos == totalTextLen)
        {
            currentToken.type = TOK_EOTF;
            currentToken.value = 0;
        }
        else
        {
            error();
        }

    }

    void print_all_tokens()
    {
        //Iterate over the tokens
        getNextToken();
        print_token();
        while (currentToken.type != TOK_EOTF)
        {
            getNextToken();
            print_token();
        }
    }

    /*void eat(int tokenType)
    {
        if (currentToken.type == tokenType)
        {
            getNextToken();
        }
        else
        {
            error();
        }
    }*/
};