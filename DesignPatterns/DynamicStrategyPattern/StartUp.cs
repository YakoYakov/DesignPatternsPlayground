using static System.Console;

namespace DynamicStrategyPattern
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var tp = new TextProcessor();
            tp.SetFormat(OutputFormat.Markdown);
            tp.AppendListItems(new[] { "Foo", "Bar", "Baz" });
            WriteLine(tp);

            tp.Clear();

            tp.SetFormat(OutputFormat.Html);
            tp.AppendListItems(new[] { "Foo", "Bar", "Baz" });
            WriteLine(tp);

            tp.Clear();
        }
    }
}
