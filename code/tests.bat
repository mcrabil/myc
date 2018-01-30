@echo off
echo --------------
..\Debug\myc.exe ..\stage_1\valid\multi_digit.c
..\Debug\myc.exe ..\stage_1\valid\newlines.c
..\Debug\myc.exe ..\stage_1\valid\no_newlines.c
..\Debug\myc.exe ..\stage_1\valid\return_0.c
..\Debug\myc.exe ..\stage_1\valid\return_2.c
..\Debug\myc.exe ..\stage_1\valid\spaces.c
echo Valid Stage 1 End
echo --------------
..\Debug\myc.exe ..\stage_1\invalid\missing_paren.c
..\Debug\myc.exe ..\stage_1\invalid\missing_retval.c
..\Debug\myc.exe ..\stage_1\invalid\no_brace.c
..\Debug\myc.exe ..\stage_1\invalid\no_semicolon.c
..\Debug\myc.exe ..\stage_1\invalid\no_space.c
..\Debug\myc.exe ..\stage_1\invalid\wrong_case.c
echo Invalid Stage 1 End
echo --------------

..\Debug\myc.exe ..\stage_1\valid\return_0.c ..\test\return_0.s
i686-w64-mingw32-gcc -m32 ..\test\return_0.s -o ..\test\return_0.exe
..\test\return_0.exe
echo %ERRORLEVEL%

..\Debug\myc.exe ..\stage_1\valid\return_2.c ..\test\return_2.s
i686-w64-mingw32-gcc -m32 ..\test\return_2.s -o ..\test\return_2.exe
..\test\return_2.exe
echo %ERRORLEVEL%