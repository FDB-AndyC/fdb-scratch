﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using ExcelService;
using Google;
using TranslationService;

namespace ExcelCellTranslator
{
    class Program
    {
        private const string DbConnectionString =
            "Data Source=.\\SQL2019;Initial Catalog=Translations;Persist Security Info=True;User ID=TranslationService;Password=c3rqHUoews!6EyyZ#iK1";

        static void Main(string[] args)
        {
            IFeedbackReceiver feedbackReceiver = new ConsoleFeedbackReceiver();
            ILanguageTranslator translator = new ToEnglishTranslator();

            var arguments = ProcessedArguments.Process(args);

            if (arguments.Filenames.Count == 2)
            {
                var connection = new SqlConnection(DbConnectionString);
                var executors = GetProcessorsFromArguments(arguments, feedbackReceiver,
                    connection, translator);

                Process(connection, executors, feedbackReceiver);
            }
            else
            {
                feedbackReceiver.Error("insufficient arguments");
            }
        }

        private static void Process(SqlConnection connection, IEnumerable<IBatchProcessor> processors,
            IFeedbackReceiver feedbackReceiver)
        {
            try
            {
                connection.Open();
                foreach (var processor in processors)
                {
                    processor.Execute();
                }
            }
            catch (Exception x)
            {
                feedbackReceiver.Error(x.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private static IList<IBatchProcessor> GetProcessorsFromArguments(ProcessedArguments arguments, IFeedbackReceiver feedbackReceiver, SqlConnection connection, ILanguageTranslator translator)
        {
            var executors = new List<IBatchProcessor>();
            var noSwitches = !arguments.Switches.Any();

            if (noSwitches || arguments.ContainsSwitch("import"))
                executors.Add(new BatchImporter(connection, feedbackReceiver, arguments.Filenames.First()));
            
            if (noSwitches || arguments.ContainsSwitch("translate") || arguments.ContainsSwitch("process"))
                executors.Add(new BatchTranslator(connection, translator, feedbackReceiver));

            if (noSwitches || arguments.ContainsSwitch("export"))
                executors.Add(new BatchExporter(connection, feedbackReceiver, arguments.Filenames.Last()));

            return executors;
        }
    }
}
