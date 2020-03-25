using System;
using System.Data;
using System.Data.SqlClient;
using ExcelService;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TranslationService;

namespace ExcelCellTranslator
{
    public class BatchTranslator : TranslatorBase
    {
        private readonly string DatabaseConnectionString;
        private SqlConnection CachedConnection;

        public BatchTranslator(string inputFilename, string outputFilename, string connectionString, ILanguageTranslator languageTranslator,
            IFeedbackReceiver feedbackReceiver) 
            : base(inputFilename, outputFilename, languageTranslator, feedbackReceiver)
        {
            this.DatabaseConnectionString = connectionString;
        }

        public override void Translate()
        {
            using var connection = ConnectToDatabase();

            this.CachedConnection = connection;
            Import();
            Process();
            Export();
            this.CachedConnection = null;
            DisconnectFromDatabase(connection);
        }

        private static void DisconnectFromDatabase(IDbConnection connection)
        {
            connection.Close();
        }

        private SqlConnection ConnectToDatabase()
        {
            var connection = new SqlConnection(this.DatabaseConnectionString);

            connection.Open();

            return connection;
        }

        private void Import()
        {
            var input = new XSSFWorkbook(this.InputFilename);
            IWorkbookIterator worker = new WorkbookCellIterator(input, this.ImportCell, this.FeedbackReceiver);

            worker.Iterate();

            throw new NotImplementedException();
        }

        private void ImportCell(ICell cell)
        {
            using var command = CreateImportCommand(cell);

            command.ExecuteNonQuery();
        }

        private SqlCommand CreateImportCommand(ICell cell)
        {
            var command = this.CachedConnection.CreateCommand();

            command.CommandText = $"INSERT INTO [Imported] ([SheetName], [RowId], [ColumnId], [Text]) VALUES @SheetName, @RowId, @ColumnId, @ImportText";

            command.Parameters.Add("SheetName", SqlDbType.NVarChar, 256).Value = cell.Sheet.SheetName;
            command.Parameters.Add("RowId", SqlDbType.Int).Value = cell.RowIndex;
            command.Parameters.Add("ColumnId", SqlDbType.Int).Value = cell.ColumnIndex;
            command.Parameters.Add("ImportText", SqlDbType.NVarChar, -1).Value = cell.StringCellValue;

            return command;
        }

        private void Process()
        {
            throw new NotImplementedException();
        }

        private void Export()
        {
            //var continueProcessing = true;

            //while (continueProcessing)
            //{
            //    var batch = GetNextProcessingBatch(connection);

            //    continueProcessing = batch.Length() > 0;

            //    if (continueProcessing)
            //        this.ProcessBatch(batch);
            //}

            throw new NotImplementedException();
        }

        private object GetNextProcessingBatch()
        {
            throw new NotImplementedException();
        }
    }
}
