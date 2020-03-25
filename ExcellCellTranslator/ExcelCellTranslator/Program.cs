using System;
using ExcelService;
using Google;
using TranslationService;

namespace ExcelCellTranslator
{
    class Program
    {
        private const string DbConnectionString =
            "Data Source=.\\SQL2019;Initial Catalog=Translations;Persist Security Info=True;User ID=TranslationService";

        static void Main(string[] args)
        {
            IFeedbackReceiver feedbackReceiver = new ConsoleFeedbackReceiver();
            ILanguageTranslator translator = new ToEnglishTranslator();
            //ILanguageTranslator translator = new CloneTranslator();

            if (args.Length == 2)
            {
                TranslateFile(args[0], args[1], translator, feedbackReceiver);
            }
            else
            {
                feedbackReceiver.Error("insufficient arguments");
            }
        }

        private static void TranslateFile(string inputFilename, string outputFilename, ILanguageTranslator languageTranslator,
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
