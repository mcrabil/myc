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

myc.exe ..\..\..\stage_2\valid\bitwise.c ..\..\..\test\bitwise.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\bitwise.s -o ..\..\..\test\bitwise.exe
..\..\..\test\bitwise.exe
echo expected 0
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_2\valid\bitwise_zero.c ..\..\..\test\bitwise_zero.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\bitwise_zero.s -o ..\..\..\test\bitwise_zero.exe
..\..\..\test\bitwise_zero.exe
echo expected -1
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_2\valid\neg.c ..\..\..\test\neg.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\neg.s -o ..\..\..\test\neg.exe
..\..\..\test\neg.exe
echo expected -5
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_2\valid\nested_ops.c ..\..\..\test\nested_ops.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\nested_ops.s -o ..\..\..\test\nested_ops.exe
..\..\..\test\nested_ops.exe
echo expected 0
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_2\valid\nested_ops_2.c ..\..\..\test\nested_ops_2.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\nested_ops_2.s -o ..\..\..\test\nested_ops_2.exe
..\..\..\test\nested_ops_2.exe
echo expected 1
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_2\valid\not_five.c ..\..\..\test\not_five.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\not_five.s -o ..\..\..\test\not_five.exe
..\..\..\test\not_five.exe
echo expected 0
echo %ERRORLEVEL%

myc.exe ..\..\..\stage_2\valid\not_zero.c ..\..\..\test\not_zero.s
i686-w64-mingw32-gcc -m32 ..\..\..\test\not_zero.s -o ..\..\..\test\not_zero.exe
..\..\..\test\not_zero.exe
echo expected 1
echo %ERRORLEVEL%
