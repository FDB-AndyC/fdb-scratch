using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelService
{
    public interface IFeedbackReceiver
    {
        void Progress(decimal progress);


        void Message(string message);

        void Error(string message);
    }

    public class ConsoleFeedbackReceiver : IFeedbackReceiver
    {
        public void Progress(decimal progress)
        {
            Console.WriteLine($"{progress}% complete");
        }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }
    }
}
