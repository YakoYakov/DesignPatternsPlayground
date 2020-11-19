using System;

namespace ClassicVisitorPattern
{
    class StartUp
    {
        static void Main(string[] args)
        {
            AdditionExpression ae = new AdditionExpression(
                new DoubleExpression(1),
                new AdditionExpression(
                    new DoubleExpression(2),
                    new DoubleExpression(3)
                    ));

            ExpressionPrinter ep = new ExpressionPrinter();

            ep.Visit(ae);

            Console.WriteLine(ep);

            ExpressionCalculator ec = new ExpressionCalculator();
            ec.Visit(ae);
            Console.WriteLine($"{ep} = {ec.Result}");
        }
    }
}
