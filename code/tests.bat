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