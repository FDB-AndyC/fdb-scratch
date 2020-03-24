using System;
using System.Collections.Generic;
using System.Text;
using TranslationService;

namespace ExcelCellTranslator
{
    public class CloneTranslator : ILanguageTranslator
    {
        public IList<string> Translate(IList<string> inputs)
        {
            return new List<string>(inputs);
        }
    }
}
