namespace ExcelCellTranslator
{
    public class TranslationData
    {
        public string SheetName { get; }
        public int RowId { get; }
        public int ColumnId { get; }
        public string Text { get;  }

        public TranslationData(string name, int row, int column, string text)
        {
            SheetName = name;
            RowId = row;
            ColumnId = column;
            Text = text;
        }

        public TranslationData Morph(string newText)
        {
            return new TranslationData(SheetName, RowId, ColumnId, newText);
        }
    }
}