using ExcelService;
using TranslationService;

namespace ExcelCellTranslator
{
    public abstract class TranslatorBase
    {
        protected const int MaxRetryCount = 10;
        protected const int RetryPauseSeconds = 10;

        protected readonly string InputFilename;
        protected readonly string OutputFilename;
        protected readonly ILanguageTranslator Translator;
        protected readonly IFeedbackReceiver FeedbackReceiver;

        protected TranslatorBase(string inputFilename, string outputFilename, ILanguageTranslator languageTranslator,
            IFeedbackReceiver feedbackReceiver)
        {
            this.InputFilename = inputFilename;
            this.OutputFilename = outputFilename;
            this.Translator = languageTranslator;
            this.FeedbackReceiver = feedbackReceiver;
        }

        public abstract void Translate();
    }
}