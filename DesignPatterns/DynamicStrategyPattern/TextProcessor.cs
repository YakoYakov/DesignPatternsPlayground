using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicStrategyPattern
{
    public class TextProcessor
    {
        private StringBuilder sb = new StringBuilder();
        private IListStrategy strategy;

        private bool IsFormatNotDefined => strategy == null;

        public void SetFormat(OutputFormat format)
        {
            switch (format)
            {
                case OutputFormat.Markdown:
                    this.strategy = new MarkdownListStrategy();
                    break;
                case OutputFormat.Html:
                    this.strategy = new HtmlListStrategy();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }

        public void AppendListItems(IEnumerable<string> items)
        {
            if (IsFormatNotDefined)
                throw new ArgumentNullException(nameof(this.strategy), "No strategy have been defined");

            this.strategy.Start(this.sb);

            foreach (string item in items)
                this.strategy.AddListItem(this.sb, item);

            this.strategy.End(this.sb);
        }

        public StringBuilder Clear()
        {
            return sb.Clear();
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}