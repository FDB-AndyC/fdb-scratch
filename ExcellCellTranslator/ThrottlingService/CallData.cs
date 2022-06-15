using System;

namespace ThrottlingService
{
    internal class CallData
    {
        public string Text { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public CallData(string text, DateTime start, DateTime end)
        {
            this.Text = text;
            this.Start = start;
            this.End = end;
        }
    }
}