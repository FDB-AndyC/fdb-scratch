using System.Collections.Generic;

namespace TranslationService
{
    public interface ILanguageTranslator
    {
        IList<string> Translate(IList<string> inputs);
    }
}