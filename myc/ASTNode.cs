using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myc
{
    public enum ASTType
    {
        Program,
        Function,
        Return,
        Constant,
        UnOp,
    };

    public class ASTNode
    {
        public ASTType type;

        public ASTNode child;
        public Token tokValue;


        public ASTNode()
        {
            tokValue = new Token();
        }
        /*AST_Node *left;
        AST_Node *right;


        union {
            Token op;
            Token tokValue;
        };*/
    };
}
