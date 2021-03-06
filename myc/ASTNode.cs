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
        Declare,
        Assign,
        Var,
        ConditionalStatement,
        ConditionalExpression,
        Compound,
        BinOp,
        UnOp,
        NullStatement,
        ForStatement,
        WhileStatement,
        DoStatement,
        Break,
        Continue,
        Constant,
    };

    public class ASTNode
    {
        public ASTType type;

        public ASTNode child;
        public Token tokValue;

        //Function or Compound
        public List<ASTNode> block_items;

        //Binary Ops
        public ASTNode left;
        public Token op;
        public ASTNode right;

        //Conditional statements
        public ASTNode ifStatement;
        public ASTNode elseStatement;

        //Conditional expressions
        public ASTNode ifExpr;
        public ASTNode elseExpr;

        //For loops
        public ASTNode forInitial;
        public ASTNode forCondition;
        public ASTNode forPostExpr;
        public ASTNode forBody;

        //While loops
        public ASTNode whileCondition;
        public ASTNode whileBody;

        //Do loops
        public ASTNode doCondition;
        public ASTNode doBody;

        //Assign
        public Token ident;

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
