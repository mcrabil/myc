#pragma once

#include "Parser.h"

class Codegen
{
public:

    char *outputStr = 0;
    int outputStrIdx = 0;

    void generate(AST_Node *node)
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
                strcat((outputStr + outputStrIdx), ".globl ");
                outputStrIdx += strlen(".globl ");
                strcat((outputStr + outputStrIdx), "_main\n_main:\n");
                outputStrIdx += strlen("_main\n_main:\n");

                generate(node->child);

            } break;
            case AST_RETURN:
            {
                strcat((outputStr + outputStrIdx), "movl $");
                outputStrIdx += strlen("movl $");

                generate(node->child);

                strcat((outputStr + outputStrIdx), ", %eax\nret\n");
                outputStrIdx += strlen(", %eax\nret\n");

            } break;
            case AST_CONSTANT:
            {
                strcat((outputStr + outputStrIdx), "3");
                outputStrIdx += strlen("3");
            } break;
            default:
            {
                printf("Unsupported AST Type!!!\n");
            } break;

        }
    }
};
