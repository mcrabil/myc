#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#include "Lexer.h"
#include "Parser.h"
#include "Codegen.h"

Lexer lexer;
Parser parser;
Codegen codegen;

void read_input_file(char *fileName)
{
    FILE * file;
    file = fopen(fileName, "rb");
    fseek(file, 0, SEEK_END);
    int size = ftell(file);
    fseek(file, 0, SEEK_SET);

    char *buf = (char *)malloc(size * sizeof(char));
    buf[0] = 0;
    fread(buf, sizeof(char), size, file);
    buf[size] = 0;

    fclose(file);

    lexer.text = buf;
    lexer.totalTextLen = size;
}

// Project to look at : https://norasandler.com/2017/11/29/Write-a-Compiler.html
// Compile using: i686-w64-mingw32-gcc -m32 .\mytest.s -o mytest.exe

int main(int argc, char **argv)
{
    char *inputFile = (char *)"../stage_1/valid/return_2.c";
    if (argc >= 2) { inputFile = argv[1]; }

    read_input_file(inputFile);

    parser.lexer = lexer;
    parser.pretty_print_ast();

    AST_Node *node = parser.program();
    codegen.outputStr = (char *)malloc(1024 * sizeof(char));
    codegen.outputStr[0] = 0;

    codegen.generate(node);

    int ij = 7;

    /*
    FILE * file;
    file = fopen("../code/mytest.s", "w");
    fwrite(codegen.outputStr, sizeof(char), codegen.outputStrIdx, file);
    fclose(file);
    */

    free(codegen.outputStr);

    return 0;
}