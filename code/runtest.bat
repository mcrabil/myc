@echo off
i686-w64-mingw32-gcc -m32 .\mytest.s -o mytest.exe
.\mytest.exe
echo %ERRORLEVEL%