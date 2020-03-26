using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ExcelService;
using NPOI.SS.Formula.Functions;
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
            var continueProcessing = true;
            var workbook = new XSSFWorkbook();
            var stats = FetchStatistics();
            var rowId = 0;
            string cachedSheetName = string.Empty;

            if (stats == null)
                return;

            foreach (var rowStats in stats)
            {
                var rowData = FetchRowData(rowStats);

                if (string.Compare(rowStats.SheetName, cachedSheetName, StringComparison.Ordinal) != 0)
                {
                    CreateSheet(rowStats.SheetName, workbook);
                }

                if (ValidateRow(rowStats, rowData))
                    ExportRow(rowData, workbook);
                else
                    InsertDummyRow(workbook);
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

            while (data.NextResult())
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

            while (data.NextResult())
            {
                stats.Add(new ImportedRowStatistics(data["SheetName"].ToString(), int.Parse(data["RowId"].ToString()),
                    int.Parse(data["CellCount"].ToString())));
            }

            return stats;
        }

        //private IList<TranslationData> ExportBatch(IList<TranslationData> batch, IWorkbook workbook)
        //{
        //    foreach (var workItem in batch)
        //    {
                
        //    }

        //    return translated;
        //}

        
    }
}
