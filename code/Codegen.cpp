#include "Codegen.h"

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
            char buffer[33];
            //CLEANUP: fix in case the length is longer than 33.
            _itoa(node->tokValue.value, buffer, 10);
            strcat((outputStr + outputStrIdx), buffer);
            outputStrIdx += strlen(buffer);
        } break;
        default:
        {
            printf("Unsupported AST Type!!!\n");
        } break;

    }
}