#include "Codegen.h"

void Codegen::addToOutputString(char *str)
{
    strcat((outputStr + outputStrIdx), str);
    outputStrIdx += strlen(str);
}

void Codegen::generate(AST_Node *node)
{
    switch (node->type)
    {
        case AST_PROGRAM:
        {
            generate(node->child);
        } break;
        case AST_FUNCTION:
        {
            //Function name
            addToOutputString((char *)".globl ");
            addToOutputString((char *)"_main\n_main:\n");

            generate(node->child);

        } break;
        case AST_RETURN:
        {
            addToOutputString((char *)"movl $");

            generate(node->child);

            addToOutputString((char *)", %eax\nret\n");
        } break;
        case AST_CONSTANT:
        {
            char buffer[33];
            //CLEANUP: fix in case the length is longer than 33.
            _itoa(node->tokValue.value, buffer, 10);
            addToOutputString(buffer);
        } break;
        case AST_UNOP:
        {
            if (node->tokValue.type == TOK_NEGATION)
            {
                generate(node->child);
                addToOutputString((char *)"neg %eax\n");
            }
            else if (node->tokValue.type == TOK_BITWISE_COMP)
            {
                generate(node->child);
                addToOutputString((char *)"not %eax\n");
            }
            else if (node->tokValue.type == TOK_LOGICAL_NEG)
            {
                generate(node->child);
                addToOutputString((char *)"cmpl $0, %eax\n");
                addToOutputString((char *)"movl $0, %eax\n");
                addToOutputString((char *)"sete %al\n");
            }

        } break;
        default:
        {
            printf("Unsupported AST Type!!!\n");
        } break;

    }
}