namespace ExcelCellTranslator
{
    public class ImportedRowStatistics
    {
        public string SheetName { get; }

        public int RowId { get; }

        public int CellCount { get; }

        public ImportedRowStatistics(string sheetName, int row, int cellCount)
        {
            SheetName = sheetName;
            RowId = row;
            CellCount = cellCount;
        }
    }
}
