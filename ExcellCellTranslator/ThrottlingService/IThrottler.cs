using System;
using System.Collections.Generic;

namespace ExcelCellTranslator
{
    public interface IThrottler
    {
        IList<string> Execute(Func<IList<string>, IList<string>> actionFunc, IList<string> arguments);
    }
}