#pragma once

#include "Lexer.h"

/*
<program> ::= <function>
<function> ::= "int" <id> "(" ")" "{" <statement> "}"
<statement> ::= "return" <exp> ";"
<exp> ::= <int>
*/

enum AST_Type
{
    AST_PROGRAM,
    AST_FUNCTION,
    AST_RETURN,
    AST_CONSTANT,
};

struct AST_Node
{
    int type;

    AST_Node *child;
    Token tokValue;
    /*AST_Node *left;
    AST_Node *right;


    union {
        Token op;
        Token tokValue;
    };*/
};

class Parser
{
public:
    Lexer lexer;

    AST_Node *program()
    {
        AST_Node *node = (AST_Node *)malloc(sizeof(AST_Node));
        node->type = AST_PROGRAM;
        node->child = function_declaration();

        return node;
    }

    AST_Node *function_declaration()
    {
        lexer.getNextToken();

        if (lexer.currentToken.type != TOK_INTKEYWORD)
        {
            lexer.error();
        }

        lexer.getNextToken();

        if (lexer.currentToken.type != TOK_MAIN)
        {
            lexer.error();
        }

        lexer.getNextToken();

        if (lexer.currentToken.type != TOK_LPAREN)
        {
            lexer.error();
        }

        lexer.getNextToken();

        if (lexer.currentToken.type != TOK_RPAREN)
        {
            lexer.error();
        }

        lexer.getNextToken();

        if (lexer.currentToken.type != TOK_LBRACE)
        {
            lexer.error();
        }

        AST_Node *node = (AST_Node *)malloc(sizeof(AST_Node));
        node->type = AST_FUNCTION;
        node->tokValue = lexer.currentToken;
        node->child = statement();

        lexer.getNextToken();

        if (lexer.currentToken.type != TOK_RBRACE)
        {
            lexer.error();
        }

        return node;
    }

    AST_Node *statement()
    {
        lexer.getNextToken();

        if (lexer.currentToken.type != TOK_RET)
        {
            lexer.error();
        }

        AST_Node *node = (AST_Node *)malloc(sizeof(AST_Node));
        node->type = AST_RETURN;
        node->tokValue = lexer.currentToken;
        node->child = exp();

        lexer.getNextToken();

        if (lexer.currentToken.type != TOK_SEMI)
        {
            lexer.error();
        }

        return node;
    }

    AST_Node *exp()
    {
        lexer.getNextToken();

        if (lexer.currentToken.type != TOK_INTLITERAL)
        {
            lexer.error();
        }

        AST_Node *node = (AST_Node *)malloc(sizeof(AST_Node));
        node->type = AST_CONSTANT;
        node->tokValue = lexer.currentToken;
        node->child = 0;

        return node;
    }

    void pretty_print_ast()
    {

    }
};