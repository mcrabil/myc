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
        Negation,
        BitwiseComp,
        LogicalNeg,
        EOTF,
    };

    public class Token
    {
        public TokenType type;

        //possible values
        public int value;
        public string strval;
    };
}
