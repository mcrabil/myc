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
    TOK_NEGATION,
    TOK_BITWISE_COMP,
    TOK_LOGICAL_NEG,
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

    char *text = 0;
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

    void print_token();
    int myatoi();
    //char peek();
    void get_identifier();
    void error();
    void getNextToken();
    void print_all_tokens();
    //void eat(int tokenType);
};
