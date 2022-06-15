using System;

namespace ExcelCellTranslator
{
    public interface IClock
    {
        DateTime GetTime();
    }

    public class UtcClock : IClock
    {
        public DateTime GetTime()
        {
            return DateTime.UtcNow;
        }
    }
}