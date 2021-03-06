﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClassicVisitorPattern
{
    public class ExpressionPrinter : IExpressionVisitor
    {
        private StringBuilder sb = new StringBuilder();

        public void Visit(DoubleExpression de)
        {
            sb.Append(de.Value);
        }

        public void Visit(AdditionExpression ae)
        {
            sb.Append("(");
            ae.Left.Accept(this);
            sb.Append("+");
            ae.Right.Accept(this);
            sb.Append(")");
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
