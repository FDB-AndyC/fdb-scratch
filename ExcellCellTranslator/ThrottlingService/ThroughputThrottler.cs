using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ThrottlingService;

namespace ExcelCellTranslator
{
    public class ThroughputThrottler : IThrottler
    {
        private const int ThrottleCadenceSeconds = 100;
        private const int ThrottleCharacterLimit = 100000;

        private readonly Queue<CallData> CallLog = new Queue<CallData>();
        private readonly IClock Clock;
        private readonly IDelayer Delayer;

        public ThroughputThrottler(IClock clock, IDelayer delayer)
        {
            this.Clock = clock;
            this.Delayer = delayer;
        }

        public IList<string> Execute(Func<IList<string>, IList<string>> actionFunc, IList<string> arguments)
        {
            LogExecution(arguments);
            DelayIfRequired();

            return actionFunc(arguments);
        }

        private void LogExecution(IList<string> arguments)
        {
            foreach (var argument in arguments)
            {
                this.CallLog.Enqueue(new CallData(argument, this.Clock.GetTime(), this.Clock.GetTime()));
            }
        }

        private void TrimLog()
        {
            var earliestToKeep = GetStartOfWindow();
            while (CallLog.Any() && (CallLog.Peek().End < earliestToKeep))
            {
                CallLog.Dequeue();
            }
        }

        private DateTime GetStartOfWindow()
        {
            return this.Clock.GetTime() - new TimeSpan(0, 0, 0, ThrottleCadenceSeconds);
        }

        private void DelayIfRequired()
        {
            TimeSpan delay;

            do
            {
                delay = TrimAndDelay();
            } while (delay > TimeSpan.Zero);

        }

        private TimeSpan TrimAndDelay()
        {
            TrimLog();
            var delay = CalculateDelay();
            if (delay.CompareTo(TimeSpan.Zero) != 0)
                this.Delayer.Delay(delay);
            
            return CalculateDelay();
        }

        private TimeSpan CalculateDelay()
        {
            var characters = CountQueuedCharacters();
            
            if (characters >= ThrottleCharacterLimit)
            {
                var oldest = this.CallLog.Peek().Start;
                var difference = this.Clock.GetTime() - oldest;

                return difference;
            }

            return TimeSpan.Zero;
        }

        private int CountQueuedCharacters()
        {
            return this.CallLog.Sum(l => l.Text.Length);
        }
    }
}