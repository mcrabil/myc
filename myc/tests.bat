@echo off

REM Run from the bin\Debug\ folder

REM echo --------------
REM FOR %%i IN (..\..\..\stage_1\valid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Valid Stage 1 End
REM echo --------------
REM FOR %%i IN (..\..\..\stage_1\invalid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Invalid Stage 1 End
REM echo --------------
REM FOR %%i IN (..\..\..\stage_2\valid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Valid Stage 2 End
REM echo --------------
REM FOR %%i IN (..\..\..\stage_2\invalid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Invalid Stage 2 End
REM echo --------------
REM FOR %%i IN (..\..\..\stage_3\valid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Valid Stage 3 End
REM echo --------------
REM FOR %%i IN (..\..\..\stage_3\invalid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Invalid Stage 3 End

REM ------------------- Stage 1 Test -------------------------

REM myc.exe ..\..\..\stage_1\valid\multi_digit.c ..\..\..\test\multi_digit.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\multi_digit.s -o ..\..\..\test\multi_digit.exe
REM ..\..\..\test\multi_digit.exe
REM echo expected 100
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_1\valid\newlines.c ..\..\..\test\newlines.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\newlines.s -o ..\..\..\test\newlines.exe
REM ..\..\..\test\newlines.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_1\valid\no_newlines.c ..\..\..\test\no_newlines.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\no_newlines.s -o ..\..\..\test\no_newlines.exe
REM ..\..\..\test\no_newlines.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_1\valid\return_0.c ..\..\..\test\return_0.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\return_0.s -o ..\..\..\test\return_0.exe
REM ..\..\..\test\return_0.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_1\valid\return_2.c ..\..\..\test\return_2.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\return_2.s -o ..\..\..\test\return_2.exe
REM ..\..\..\test\return_2.exe
REM echo expected 2
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_1\valid\spaces.c ..\..\..\test\spaces.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\spaces.s -o ..\..\..\test\spaces.exe
REM ..\..\..\test\spaces.exe
REM echo expected 0
REM echo %ERRORLEVEL%

REM ------------------- Stage 2 Test -------------------------

REM myc.exe ..\..\..\stage_2\valid\bitwise.c ..\..\..\test\bitwise.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\bitwise.s -o ..\..\..\test\bitwise.exe
REM ..\..\..\test\bitwise.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_2\valid\bitwise_zero.c ..\..\..\test\bitwise_zero.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\bitwise_zero.s -o ..\..\..\test\bitwise_zero.exe
REM ..\..\..\test\bitwise_zero.exe
REM echo expected -1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_2\valid\neg.c ..\..\..\test\neg.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\neg.s -o ..\..\..\test\neg.exe
REM ..\..\..\test\neg.exe
REM echo expected -5
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_2\valid\nested_ops.c ..\..\..\test\nested_ops.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\nested_ops.s -o ..\..\..\test\nested_ops.exe
REM ..\..\..\test\nested_ops.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_2\valid\nested_ops_2.c ..\..\..\test\nested_ops_2.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\nested_ops_2.s -o ..\..\..\test\nested_ops_2.exe
REM ..\..\..\test\nested_ops_2.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_2\valid\not_five.c ..\..\..\test\not_five.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\not_five.s -o ..\..\..\test\not_five.exe
REM ..\..\..\test\not_five.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_2\valid\not_zero.c ..\..\..\test\not_zero.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\not_zero.s -o ..\..\..\test\not_zero.exe
REM ..\..\..\test\not_zero.exe
REM echo expected 1
REM echo %ERRORLEVEL%

REM ------------------- Stage 3 Test -------------------------

myc.exe ..\..\..\stage_3\valid\add.c ..\..\..\test\add.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\add.s -o ..\..\..\test\add.exe
..\..\..\test\add.exe
echo expected 3
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\associativity.c ..\..\..\test\associativity.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\associativity.s -o ..\..\..\test\associativity.exe
..\..\..\test\associativity.exe
echo expected -4 
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\associativity_2.c ..\..\..\test\associativity_2.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\associativity_2.s -o ..\..\..\test\associativity_2.exe
..\..\..\test\associativity_2.exe
echo expected 1
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\div.c ..\..\..\test\div.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\div.s -o ..\..\..\test\div.exe
..\..\..\test\div.exe
echo expected 2
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\mult.c ..\..\..\test\mult.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\mult.s -o ..\..\..\test\mult.exe
..\..\..\test\mult.exe
echo expected 6
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\parens.c ..\..\..\test\parens.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\parens.s -o ..\..\..\test\parens.exe
..\..\..\test\parens.exe
echo expected 14
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\precedence.c ..\..\..\test\precedence.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\precedence.s -o ..\..\..\test\precedence.exe
..\..\..\test\precedence.exe
echo expected 14
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\sub.c ..\..\..\test\sub.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\sub.s -o ..\..\..\test\sub.exe
..\..\..\test\sub.exe
echo expected -1
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\sub_neg.c ..\..\..\test\sub_neg.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\sub_neg.s -o ..\..\..\test\sub_neg.exe
..\..\..\test\sub_neg.exe
echo expected 3
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\unop_add.c ..\..\..\test\unop_add.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\unop_add.s -o ..\..\..\test\unop_add.exe
..\..\..\test\unop_add.exe
echo expected 0
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_3\valid\unop_parens.c ..\..\..\test\unop_parens.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\unop_parens.s -o ..\..\..\test\unop_parens.exe
..\..\..\test\unop_parens.exe
echo expected -3
echo %ERRORLEVEL%
