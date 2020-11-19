namespace ClassicVisitorPattern
{
    public class AdditionExpression : Expression
    {
        public Expression Left;
        public Expression Right;

        public AdditionExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            // double dispach
            visitor.Visit(this);
        }
    }
}