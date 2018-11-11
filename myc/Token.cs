using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myc
{
    public enum TokenType
    {
        LBrace,
        RBrace,
        LParen,
        RParen,
        Semi,
        IntKeyword,
        Ret,
        Identifier,
        IntLiteral,
        Main,
        Minus,
        BitwiseComp,
        LogicalNeg,
        Addition,
        Multiplication,
        Division,
        LogicalAnd,
        LogicalOr,
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        BitwiseAnd,
        BitwiseOr,
        BitwiseXor,
        BitwiseShiftLeft,
        BitwiseShiftRight,
        Modulo,

        EOTF,
    };

    public class Token
    {
        public TokenType type;

        //possible values
        public int value;
        public string strval;

        public static void CopyToken(Token toToken, Token fromToken)
        {
            toToken.type = fromToken.type;
            toToken.value = fromToken.value;
            toToken.strval = fromToken.strval;
        }
    };
}
