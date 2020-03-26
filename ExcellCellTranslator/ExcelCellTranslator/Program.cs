using System;
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
            //ILanguageTranslator translator = new CloneTranslator();

            var arguments = ProcessedArguments.Process(args);

            if (arguments.Filenames.Count == 2)
            {
                TranslateFile(arguments, translator, feedbackReceiver);
            }
            else
            {
                feedbackReceiver.Error("insufficient arguments");
            }
        }

        private static void TranslateFile(ProcessedArguments arguments, ILanguageTranslator languageTranslator,
            IFeedbackReceiver feedbackReceiver)
        {
            try
            {
                var translator =
                    //new RealtimeTranslator(inputFilename, outputFilename, languageTranslator, feedbackReceiver);
                    new BatchTranslator(inputFilename, outputFilename, DbConnectionString, languageTranslator, feedbackReceiver);

                translator.Translate();
            }
            catch (Exception x)
            {
                 feedbackReceiver.Error(x.Message);
            }
        }
    }
}
