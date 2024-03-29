﻿using System.Collections.Generic;
using TranslationService;

namespace ExcelCellTranslator
{
    public class CloneTranslator : ILanguageTranslator
    {
        public IList<string> Translate(IList<string> inputs)
        {
            return new List<string>(inputs);
        }

        public string Translate(string input)
        {
            return input;
        }
    }
}
