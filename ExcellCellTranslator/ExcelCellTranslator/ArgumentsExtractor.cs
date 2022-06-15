using System.Linq;

namespace ExcelCellTranslator
{
    public static class ArgumentsExtractor
    {
        public static string ExtractDatabaseConnectionString(ref ProcessedArguments arguments)
        {
            var dbSwitch = arguments.Switches.SingleOrDefault(sw => sw.StartsWith("db="));

            if (string.IsNullOrEmpty(dbSwitch))
                return null;

            arguments = ProcessedArguments.Create(arguments.Filenames,
                arguments.Switches.Where(sw => !sw.StartsWith("db=")));

            return dbSwitch.Substring(3);
        }
    }
}
