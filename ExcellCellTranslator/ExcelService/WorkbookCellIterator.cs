using System;
using NPOI.SS.UserModel;

namespace ExcelService
{
    public class WorkbookCellIterator : IWorkbookIterator
    {
        private readonly IWorkbook InputWorkbook;
        private readonly Action<ICell> CellWorkerFunc;
        private readonly IFeedbackReceiver FeedbackReceiver;

        public WorkbookCellIterator(IWorkbook input, Action<ICell> cellWorkerFunc, IFeedbackReceiver feedbackReceiver)
        {
            this.InputWorkbook = input;
            this.CellWorkerFunc = cellWorkerFunc;
            this.FeedbackReceiver = feedbackReceiver;
        }

        public void Iterate()
        {
            var numSheets = InputWorkbook.NumberOfSheets;

            for (var sheetIndex = 0; sheetIndex < numSheets; ++sheetIndex)
            {
                var inputSheet = InputWorkbook.GetSheetAt(sheetIndex);

                this.FeedbackReceiver.Message($"Processing sheet {sheetIndex + 1} of {numSheets}");
                this.IterateCellsInSheet(inputSheet);
            }
        }
        private void IterateCellsInSheet(ISheet inputSheet)
        {
            var numRows = inputSheet.LastRowNum - inputSheet.FirstRowNum + 1;

            for (var rowIndex = inputSheet.FirstRowNum; rowIndex <= inputSheet.LastRowNum; ++rowIndex)
            {
                var inputRow = inputSheet.GetRow(rowIndex);

                this.FeedbackReceiver.Message($"Processing row {rowIndex + 1} of {numRows}");
                this.IterateCellsInRow(inputRow);
            }
        }
        private void IterateCellsInRow(IRow inputRow)
        {
            foreach (var inputCell in inputRow.Cells)
            {
                this.CellWorkerFunc(inputCell);
            }
        }

    }
}
