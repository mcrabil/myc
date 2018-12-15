@echo off
setlocal EnableDelayedExpansion

REM Run from the bin\Debug\ folder

REM Run to populate valid tests
FOR %%k IN (valid) DO (
    FOR %%j IN (8) DO (
        echo --------------
        FOR %%i IN (..\..\..\stage_%%j\%%k\*.c) DO (
            echo %%i
            myc.exe ..\..\..\stage_%%j\%%k\%%~ni.c ..\..\..\test\%%~ni.s > nul
            i686-w64-mingw32-gcc -m32 ..\..\..\test\%%~ni.s -o ..\..\..\test\%%~ni.exe
            if not exist ..\..\..\stage_%%j\%%k_out\ mkdir ..\..\..\stage_%%j\%%k_out\ 
            ..\..\..\test\%%~ni.exe>..\..\..\stage_%%j\%%k_out\%%~ni.txt
            ..\..\..\test\%%~ni.exe
        REM    echo !ERRORLEVEL!
            echo !ERRORLEVEL!>>..\..\..\stage_%%j\%%k_out\%%~ni.txt
        )
    )
)


REM Run to populate invalid tests
REM FOR %%k IN (invalid) DO (
REM     FOR %%j IN (8) DO (
REM         echo --------------
REM         FOR %%i IN (..\..\..\stage_%%j\%%k\*.c) DO (
REM             if not exist ..\..\..\stage_%%j\%%k_out\ mkdir ..\..\..\stage_%%j\%%k_out\ 
REM             myc.exe ..\..\..\stage_%%j\%%k\%%~ni.c ..\..\..\test\%%~ni.s>..\..\..\stage_%%j\%%k_out\%%~ni.txt
REM         )
REM     )
REM )


REM Run to validate valid tests
REM FOR %%k IN (valid) DO (
REM     FOR %%j IN (1, 2, 3, 4, 5, 6, 7, 8) DO (
REM         echo --------------
REM         FOR %%i IN (..\..\..\stage_%%j\%%k\*.c) DO (
REM             myc.exe ..\..\..\stage_%%j\%%k\%%~ni.c ..\..\..\test\%%~ni.s > nul
REM             i686-w64-mingw32-gcc -m32 ..\..\..\test\%%~ni.s -o ..\..\..\test\%%~ni.exe
REM             ..\..\..\test\%%~ni.exe>..\..\..\test\%%~ni.txt
REM             ..\..\..\test\%%~ni.exe
REM         REM    echo !ERRORLEVEL!
REM             echo !ERRORLEVEL!>>..\..\..\test\%%~ni.txt
REM 
REM             fc /b ..\..\..\test\%%~ni.txt ..\..\..\stage_%%j\%%k_out\%%~ni.txt > nul
REM             if errorlevel 1 (
REM                 ECHO the files were different!!!!!!!!!!! %%i
REM             )
REM         )
REM         echo Valid Stage %%j End
REM     )
REM )


REM Run to validate invalid tests
REM FOR %%k IN (invalid) DO (
REM     FOR %%j IN (1, 2, 3, 4, 5, 6, 7, 8) DO (
REM         echo --------------
REM         FOR %%i IN (..\..\..\stage_%%j\%%k\*.c) DO (
REM             if not exist ..\..\..\stage_%%j\%%k_out\ mkdir ..\..\..\stage_%%j\%%k_out\ 
REM             myc.exe ..\..\..\stage_%%j\%%k\%%~ni.c ..\..\..\test\%%~ni.s>..\..\..\test\%%~ni.txt
REM 
REM             fc /b ..\..\..\test\%%~ni.txt ..\..\..\stage_%%j\%%k_out\%%~ni.txt > nul
REM             if errorlevel 1 (
REM                 ECHO the files were different!!!!!!!!!!! %%i
REM             )
REM         )
REM         echo Invalid Stage %%j End
REM     )
REM )
