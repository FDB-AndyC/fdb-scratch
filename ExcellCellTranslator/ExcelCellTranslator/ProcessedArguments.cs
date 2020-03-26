using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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

        public static ProcessedArguments Process(string[] arguments)
        {
            var switches = new List<string>();
            var filenames = new List<string>();

            foreach (var argument in arguments)
            {
                if (argument.StartsWith('-') || argument.StartsWith('/'))
                {
                    switches.Add(argument.TrimStart('-', '/'));
                }
                else
                {
                    filenames.Add(argument);
                }
            }

            return new ProcessedArguments(arguments, filenames);
        }
    }
}
