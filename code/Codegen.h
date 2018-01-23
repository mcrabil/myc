#pragma once

#include "Parser.h"

class Codegen
{
public:

    char *outputStr = 0;
    int outputStrIdx = 0;

    void generate(AST_Node *node);

private:
    void addToOutputString(char *str);
};
