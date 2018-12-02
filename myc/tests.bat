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
REM echo --------------
REM FOR %%i IN (..\..\..\stage_4\valid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Valid Stage 4 End
REM echo --------------
REM FOR %%i IN (..\..\..\stage_4\invalid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Invalid Stage 4 End
REM echo --------------
REM FOR %%i IN (..\..\..\stage_5\valid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Valid Stage 5 End
REM echo --------------
REM FOR %%i IN (..\..\..\stage_5\invalid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo --------------
REM FOR %%i IN (..\..\..\stage_6\valid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Valid Stage 6 End
REM echo --------------
REM FOR %%i IN (..\..\..\stage_6\invalid\*) DO (
REM     ECHO %%i
REM     myc.exe %%i
REM )
REM echo Invalid Stage 6 End

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

REM myc.exe ..\..\..\stage_3\valid\add.c ..\..\..\test\add.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\add.s -o ..\..\..\test\add.exe
REM ..\..\..\test\add.exe
REM echo expected 3
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\associativity.c ..\..\..\test\associativity.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\associativity.s -o ..\..\..\test\associativity.exe
REM ..\..\..\test\associativity.exe
REM echo expected -4 
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\associativity_2.c ..\..\..\test\associativity_2.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\associativity_2.s -o ..\..\..\test\associativity_2.exe
REM ..\..\..\test\associativity_2.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\div.c ..\..\..\test\div.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\div.s -o ..\..\..\test\div.exe
REM ..\..\..\test\div.exe
REM echo expected 2
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\mult.c ..\..\..\test\mult.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\mult.s -o ..\..\..\test\mult.exe
REM ..\..\..\test\mult.exe
REM echo expected 6
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\parens.c ..\..\..\test\parens.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\parens.s -o ..\..\..\test\parens.exe
REM ..\..\..\test\parens.exe
REM echo expected 14
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\precedence.c ..\..\..\test\precedence.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\precedence.s -o ..\..\..\test\precedence.exe
REM ..\..\..\test\precedence.exe
REM echo expected 14
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\sub.c ..\..\..\test\sub.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\sub.s -o ..\..\..\test\sub.exe
REM ..\..\..\test\sub.exe
REM echo expected -1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\sub_neg.c ..\..\..\test\sub_neg.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\sub_neg.s -o ..\..\..\test\sub_neg.exe
REM ..\..\..\test\sub_neg.exe
REM echo expected 3
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\unop_add.c ..\..\..\test\unop_add.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\unop_add.s -o ..\..\..\test\unop_add.exe
REM ..\..\..\test\unop_add.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_3\valid\unop_parens.c ..\..\..\test\unop_parens.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\unop_parens.s -o ..\..\..\test\unop_parens.exe
REM ..\..\..\test\unop_parens.exe
REM echo expected -3
REM echo %ERRORLEVEL%

REM ------------------- Stage 4 Test -------------------------

REM myc.exe ..\..\..\stage_4\valid\and_false.c ..\..\..\test\and_false.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\and_false.s -o ..\..\..\test\and_false.exe
REM ..\..\..\test\and_false.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\and_true.c ..\..\..\test\and_true.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\and_true.s -o ..\..\..\test\and_true.exe
REM ..\..\..\test\and_true.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\eq_false.c ..\..\..\test\eq_false.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\eq_false.s -o ..\..\..\test\eq_false.exe
REM ..\..\..\test\eq_false.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\eq_true.c ..\..\..\test\eq_true.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\eq_true.s -o ..\..\..\test\eq_true.exe
REM ..\..\..\test\eq_true.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\ge_false.c ..\..\..\test\ge_false.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\ge_false.s -o ..\..\..\test\ge_false.exe
REM ..\..\..\test\ge_false.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\ge_true.c ..\..\..\test\ge_true.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\ge_true.s -o ..\..\..\test\ge_true.exe
REM ..\..\..\test\ge_true.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\le_false.c ..\..\..\test\le_false.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\le_false.s -o ..\..\..\test\le_false.exe
REM ..\..\..\test\le_false.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\le_true.c ..\..\..\test\le_true.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\le_true.s -o ..\..\..\test\le_true.exe
REM ..\..\..\test\le_true.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\ne_false.c ..\..\..\test\ne_false.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\ne_false.s -o ..\..\..\test\ne_false.exe
REM ..\..\..\test\ne_false.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\ne_true.c ..\..\..\test\ne_true.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\ne_true.s -o ..\..\..\test\ne_true.exe
REM ..\..\..\test\ne_true.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\or_false.c ..\..\..\test\or_false.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\or_false.s -o ..\..\..\test\or_false.exe
REM ..\..\..\test\or_false.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\or_true.c ..\..\..\test\or_true.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\or_true.s -o ..\..\..\test\or_true.exe
REM ..\..\..\test\or_true.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\precedence.c ..\..\..\test\precedence.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\precedence.s -o ..\..\..\test\precedence.exe
REM ..\..\..\test\precedence.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\precedence_2.c ..\..\..\test\precedence_2.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\precedence_2.s -o ..\..\..\test\precedence_2.exe
REM ..\..\..\test\precedence_2.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\precedence_3.c ..\..\..\test\precedence_3.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\precedence_3.s -o ..\..\..\test\precedence_3.exe
REM ..\..\..\test\precedence_3.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_4\valid\precedence_4.c ..\..\..\test\precedence_4.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\precedence_4.s -o ..\..\..\test\precedence_4.exe
REM ..\..\..\test\precedence_4.exe
REM echo expected 1
REM echo %ERRORLEVEL%

REM ------------------- Stage 5 Test -------------------------

REM myc.exe ..\..\..\stage_5\valid\assign.c ..\..\..\test\assign.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\assign.s -o ..\..\..\test\assign.exe
REM ..\..\..\test\assign.exe
REM echo expected 2
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_5\valid\assign_val.c ..\..\..\test\assign_val.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\assign_val.s -o ..\..\..\test\assign_val.exe
REM ..\..\..\test\assign_val.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_5\valid\exp_return_val.c ..\..\..\test\exp_return_val.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\exp_return_val.s -o ..\..\..\test\exp_return_val.exe
REM ..\..\..\test\exp_return_val.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_5\valid\initialize.c ..\..\..\test\initialize.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\initialize.s -o ..\..\..\test\initialize.exe
REM ..\..\..\test\initialize.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_5\valid\multiple_vars.c ..\..\..\test\multiple_vars.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\multiple_vars.s -o ..\..\..\test\multiple_vars.exe
REM ..\..\..\test\multiple_vars.exe
REM echo expected 3
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_5\valid\no_initialize.c ..\..\..\test\no_initialize.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\no_initialize.s -o ..\..\..\test\no_initialize.exe
REM ..\..\..\test\no_initialize.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_5\valid\refer.c ..\..\..\test\refer.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\refer.s -o ..\..\..\test\refer.exe
REM ..\..\..\test\refer.exe
REM echo expected 2
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_5\valid\undefined_missing_return.c ..\..\..\test\undefined_missing_return.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\undefined_missing_return.s -o ..\..\..\test\undefined_missing_return.exe
REM ..\..\..\test\undefined_missing_return.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_5\valid\unused_exp.c ..\..\..\test\unused_exp.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\unused_exp.s -o ..\..\..\test\unused_exp.exe
REM ..\..\..\test\unused_exp.exe
REM echo expected 0
REM echo %ERRORLEVEL%

REM ------------------- Stage 5 Test -------------------------

REM myc.exe ..\..\..\stage_6\valid\assign_ternary.c ..\..\..\test\assign_ternary.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\assign_ternary.s -o ..\..\..\test\assign_ternary.exe
REM ..\..\..\test\assign_ternary.exe
REM echo expected 2
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\else.c ..\..\..\test\else.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\else.s -o ..\..\..\test\else.exe
REM ..\..\..\test\else.exe
REM echo expected 2
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\if_nested.c ..\..\..\test\if_nested.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\if_nested.s -o ..\..\..\test\if_nested.exe
REM ..\..\..\test\if_nested.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\if_nested_2.c ..\..\..\test\if_nested_2.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\if_nested_2.s -o ..\..\..\test\if_nested_2.exe
REM ..\..\..\test\if_nested_2.exe
REM echo expected 2
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\if_nested_3.c ..\..\..\test\if_nested_3.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\if_nested_3.s -o ..\..\..\test\if_nested_3.exe
REM ..\..\..\test\if_nested_3.exe
REM echo expected 3
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\if_nested_4.c ..\..\..\test\if_nested_4.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\if_nested_4.s -o ..\..\..\test\if_nested_4.exe
REM ..\..\..\test\if_nested_4.exe
REM echo expected 4
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\if_nested_5.c ..\..\..\test\if_nested_5.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\if_nested_5.s -o ..\..\..\test\if_nested_5.exe
REM ..\..\..\test\if_nested_5.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\if_not_taken.c ..\..\..\test\if_not_taken.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\if_not_taken.s -o ..\..\..\test\if_not_taken.exe
REM ..\..\..\test\if_not_taken.exe
REM echo expected 0
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\if_taken.c ..\..\..\test\if_taken.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\if_taken.s -o ..\..\..\test\if_taken.exe
REM ..\..\..\test\if_taken.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\multiple_if.c ..\..\..\test\multiple_if.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\multiple_if.s -o ..\..\..\test\multiple_if.exe
REM ..\..\..\test\multiple_if.exe
REM echo expected 8
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\multiple_ternary.c ..\..\..\test\multiple_ternary.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\multiple_ternary.s -o ..\..\..\test\multiple_ternary.exe
REM ..\..\..\test\multiple_ternary.exe
REM echo expected 10
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\nested_ternary.c ..\..\..\test\nested_ternary.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\nested_ternary.s -o ..\..\..\test\nested_ternary.exe
REM ..\..\..\test\nested_ternary.exe
REM echo expected 7
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\nested_ternary_2.c ..\..\..\test\nested_ternary_2.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\nested_ternary_2.s -o ..\..\..\test\nested_ternary_2.exe
REM ..\..\..\test\nested_ternary_2.exe
REM echo expected 15
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\rh_assignment.c ..\..\..\test\rh_assignment.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\rh_assignment.s -o ..\..\..\test\rh_assignment.exe
REM ..\..\..\test\rh_assignment.exe
REM echo expected 1
REM echo %ERRORLEVEL%
REM 
REM myc.exe ..\..\..\stage_6\valid\ternary.c ..\..\..\test\ternary.s
REM i686-w64-mingw32-gcc -m32 ..\..\..\test\ternary.s -o ..\..\..\test\ternary.exe
REM ..\..\..\test\ternary.exe
REM echo expected 4
REM echo %ERRORLEVEL%
