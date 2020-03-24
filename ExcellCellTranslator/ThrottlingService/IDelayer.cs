using System;
using System.Threading;

namespace ThrottlingService
{
    public interface IDelayer
    {
        void Delay(TimeSpan comeBackAfterSpan);
    }

    public class ThreadSleepDelayer : IDelayer
    {
        public void Delay(TimeSpan comeBackAfterSpan)
        {
            Thread.Sleep(comeBackAfterSpan);
        }
    }
}