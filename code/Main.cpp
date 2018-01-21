#include <stdio.h>
#include <string.h>
#include <stdlib.h>


#include "Lexer.cpp"
#include "Parser.cpp"
#include "Codegen.cpp"

Lexer lexer;
Parser parser;
Codegen codegen;



/*

int accept(Symbol s) {
if (sym == s) {
nextsym();
return 1;
}
return 0;
}

int expect(Symbol s) {
if (accept(s))
return 1;
error("expect: unexpected symbol");
return 0;
}


*/


// Project to look at : https://norasandler.com/2017/11/29/Write-a-Compiler.html
// Compile using: i686-w64-mingw32-gcc -m32 .\mytest.s -o mytest.exe

int main()
{
    lexer.totalTextLen = strlen(lexer.text);

    parser.pretty_print_ast();

    AST_Node *node = parser.program();
    codegen.outputStr = (char *)malloc(1024 * sizeof(char));
    codegen.outputStr[0] = 0;

    codegen.generate(node);

    int ij = 7;

    FILE * file;
    file = fopen("../code/mytest.s", "w");
    fwrite(codegen.outputStr, sizeof(char), codegen.outputStrIdx, file);
    fclose(file);

    free(codegen.outputStr);

    return 0;
}