#include "Parser.h"

AST_Node *Parser::program()
{
    AST_Node *node = (AST_Node *)malloc(sizeof(AST_Node));
    node->type = AST_PROGRAM;
    node->child = function_declaration();

    return node;
}

AST_Node *Parser::function_declaration()
{
    expect(TOK_INTKEYWORD);
    expect(TOK_MAIN);
    expect(TOK_LPAREN);
    expect(TOK_RPAREN);
    expect(TOK_LBRACE);

    AST_Node *node = (AST_Node *)malloc(sizeof(AST_Node));
    node->type = AST_FUNCTION;
    node->tokValue = lexer.currentToken;
    node->child = statement();

    expect(TOK_RBRACE);

    return node;
}

AST_Node *Parser::statement()
{
    expect(TOK_RET);

    AST_Node *node = (AST_Node *)malloc(sizeof(AST_Node));
    node->type = AST_RETURN;
    node->tokValue = lexer.currentToken;
    node->child = exp();

    expect(TOK_SEMI);

    return node;
}

AST_Node *Parser::exp()
{
    expect(TOK_INTLITERAL);

    AST_Node *node = (AST_Node *)malloc(sizeof(AST_Node));
    node->type = AST_CONSTANT;
    node->tokValue = lexer.currentToken;
    node->child = 0;

    return node;
}

int Parser::expect(int type) {
    lexer.getNextToken();
    if (lexer.currentToken.type == type)
    {
        return 1;
    }

    lexer.error();

    return 0;
}

void Parser::pretty_print_ast()
{
    //TODO: implement pretty print ast
}
