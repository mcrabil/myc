@echo off
echo --------------
FOR %%i IN (..\stage_1\valid\*) DO (
	ECHO %%i
	..\Debug\myc.exe %%i
)
echo Valid Stage 1 End
echo --------------
FOR %%i IN (..\stage_1\invalid\*) DO (
	ECHO %%i
	..\Debug\myc.exe %%i
)
echo Invalid Stage 1 End
echo --------------

REM ..\Debug\myc.exe ..\stage_1\valid\return_0.c ..\test\return_0.s
REM i686-w64-mingw32-gcc -m32 ..\test\return_0.s -o ..\test\return_0.exe
REM ..\test\return_0.exe
REM echo %ERRORLEVEL%

REM ..\Debug\myc.exe ..\stage_1\valid\return_2.c ..\test\return_2.s
REM i686-w64-mingw32-gcc -m32 ..\test\return_2.s -o ..\test\return_2.exe
REM ..\test\return_2.exe
REM echo %ERRORLEVEL%