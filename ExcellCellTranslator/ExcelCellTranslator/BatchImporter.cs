using System.Data.SqlClient;
using ExcelService;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelCellTranslator
{
    public class BatchImporter : DatabaseTranslator, IBatchProcessor
    {
        private readonly string InputFilename;
        private const int ApiBatchSize = 10000;

        public BatchImporter(SqlConnection connection, IFeedbackReceiver feedbackReceiver, string inputFilename)
            : base(connection, feedbackReceiver)
        {
            this.InputFilename = inputFilename;
        }

        public void Execute()
        {
            var input = new XSSFWorkbook(this.InputFilename);
            IWorkbookIterator worker = new WorkbookCellIterator(input, this.ImportCell, this.FeedbackReceiver);

            worker.Iterate();
        }

        private void ImportCell(ICell cell)
        {
            using var command = CachedConnection.GenerateImportInsertCommand(cell);

            command.ExecuteNonQuery();
        }

    }
}