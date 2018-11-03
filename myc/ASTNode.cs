﻿using System;
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
        BinOp,
    };

    public class ASTNode
    {
        public ASTType type;

        public ASTNode child;
        public Token tokValue;

        //Binary Ops
        public ASTNode left;
        public Token op;
        public ASTNode right;

        public ASTNode()
        {
            tokValue = new Token();
            op = new Token();
        }

        public ASTNode(Token op, ASTNode left, ASTNode right, ASTType type)
        {
            tokValue = new Token();
            this.op = op;
            this.left = left;
            this.right = right;
            this.type = type;
        }
    };
}
