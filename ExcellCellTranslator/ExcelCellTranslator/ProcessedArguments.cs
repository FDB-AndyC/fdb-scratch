using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelCellTranslator
{
    public class ProcessedArguments
    {
        public IList<string> Switches { get; }
        public IList<string> Filenames { get; }

        private ProcessedArguments(IList<string> switches, IList<string> filenames)
        {
            this.Switches = switches;
            this.Filenames = filenames;
        }

        public bool ContainsSwitch(string switchName)
        {
            return Switches.Any(sw => string.Compare(sw, switchName, StringComparison.Ordinal) == 0);
        }

        public static ProcessedArguments Process(string[] arguments)
        {
            var switches = new List<string>();
            var filenames = new List<string>();

            foreach (var argument in arguments)
            {
                if (argument.StartsWith('-') || argument.StartsWith('/'))
                {
                    var thisSwitch = argument.TrimStart('-', '/');
                    if (!string.IsNullOrEmpty(thisSwitch))
                        switches.Add(thisSwitch);
                }
                else
                {
                    filenames.Add(argument);
                }
            }

            return new ProcessedArguments(switches, filenames);
        }

        public static ProcessedArguments Create(IList<string> fileNames, IEnumerable<string> switches)
        {
            return new ProcessedArguments(switches.ToList(), fileNames);
        }
    }
}
