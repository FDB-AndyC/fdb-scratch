using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using ExcelService;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelCellTranslator
{
    public class BatchExporter : DatabaseTranslator, IBatchProcessor
    {
        private readonly string OutputFilename;

        public BatchExporter(SqlConnection connection, IFeedbackReceiver feedbackReceiver, string outputFilename)
            : base(connection, feedbackReceiver)
        {
            this.OutputFilename = outputFilename;
        }

        public void Execute()
        {
            var workbook = new XSSFWorkbook();
            var stats = FetchStatistics();
            ISheet currentSheet = null;
            int rowId = 0;

            if (stats == null)
            {
                FeedbackReceiver.Message("Nothing to export");
                return;
            }

            foreach (var rowStats in stats)
            {
                if (currentSheet == null || string.CompareOrdinal(currentSheet.SheetName, rowStats.SheetName) != 0)
                {
                    currentSheet = workbook.CreateSheet(rowStats.SheetName);
                }

                ProcessRow(rowStats, rowId++, currentSheet);
            }

            SaveWorkbook(workbook);
        }

        private void SaveWorkbook(XSSFWorkbook workbook)
        {
            using var outputStream = File.Create(this.OutputFilename);

            workbook.Write(outputStream);
            workbook.Close();
        }

        private void ProcessRow(ImportedRowStatistics rowStats, in int rowId, ISheet worksheet)
        {
            FeedbackReceiver.Message($"Exporting row {rowStats.SheetName}.{rowStats.RowId} ({rowStats.CellCount} cells)");
            var rowData = FetchRowData(rowStats);

            if (rowId != rowStats.RowId)
                InsertBlankRow(worksheet, rowId);
            else if (ValidateRow(rowStats, rowData))
                ExportRow(rowData, worksheet, rowId);
            else
                InsertDummyRow(worksheet, rowId);
        }

        private void InsertDummyRow(ISheet worksheet, in int rowId)
        {
            var row = worksheet.CreateRow(rowId);

            row.CreateCell(0).SetCellValue("Dummy");
        }

        private void InsertBlankRow(ISheet worksheet, in int rowId)
        {
            worksheet.CreateRow(rowId);
        }

        private void ExportRow(IList<TranslationData> rowData, ISheet worksheet, in int rowId)
        {
            var row = worksheet.CreateRow(rowId);

            for (var columnIndex = rowData.Min(rd => rd.ColumnId);
                columnIndex <= rowData.Max(rd => rd.ColumnId);
                ++columnIndex)
            {
                var cellData = rowData.SingleOrDefault(rd => rd.ColumnId == columnIndex);

                if (cellData == null)
                {
                    row.CreateCell(columnIndex, CellType.Blank);
                }
                else
                {
                    var cell = row.CreateCell(columnIndex, CellType.String);
                    cell.SetCellValue(cellData.Text);
                }
            }
        }

        private bool ValidateRow(ImportedRowStatistics rowStats, IList<TranslationData> rowData)
        {
            if (rowStats.CellCount != rowData.Count)
            {
                FeedbackReceiver.Error($"Inconsistent data/stats ({rowData.Count}/{rowStats.CellCount}");
                return false;
            }

            return true;
        }

        private IList<TranslationData> FetchRowData(ImportedRowStatistics rowStats)
        {
            using var command = CachedConnection.GenerateGetNextProcessedRowCommand(rowStats.SheetName, rowStats.RowId);
            using var data = command.ExecuteReader();
            var translated = new List<TranslationData>();

            while (data.Read())
            {
                var cellData = new TranslationData(rowStats.SheetName, rowStats.RowId,
                    int.Parse(data["ColumnId"].ToString()), data["Text"].ToString());
                translated.Add(cellData);
            }

            return translated;
        }

        private IList<ImportedRowStatistics> FetchStatistics()
        {
            using var command = CachedConnection.GenerateRowStatisticsCommand();
            using var data = command.ExecuteReader();

            if (!data.HasRows)
                return null;

            var stats = new List<ImportedRowStatistics>();

            while (data.Read())
            {
                stats.Add(new ImportedRowStatistics(data["SheetName"].ToString(), int.Parse(data["RowId"].ToString()),
                    int.Parse(data["CellCount"].ToString())));
            }

            return stats;
        }
    }
}
