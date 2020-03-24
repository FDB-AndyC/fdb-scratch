using System.Collections.Generic;
using System.Linq;
using Google.Cloud.Translation.V2;

namespace TranslationService
{
    public class ToEnglishTranslator : ILanguageTranslator
    {
        //private const string GcpApiKey = "AIzaSyAD20gXoCZf2yfyUvjgPybFZy4KdGufAvg";
        private const string GcpApiKey = "AIzaSyDQPtoYW-FMkxMQ4VrbhrxAZAnwJFbTW68";

        private readonly TranslationClient Translator = TranslationClient.CreateFromApiKey(GcpApiKey);

        public IList<string> Translate(IList<string> inputs)
        {
            var results = Translator.TranslateText(inputs, LanguageCodes.English);

            return results.Select(r => r.TranslatedText).ToList();
        }
    }
}
