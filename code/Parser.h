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
    AST_UNOP,
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

    AST_Node *program();
    AST_Node *function_declaration();
    AST_Node *statement();
    AST_Node *exp();
    int expect(int type);
    void pretty_print_ast();
};