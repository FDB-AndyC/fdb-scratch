using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;

namespace ExcelService
{
    public class WorkbookRowIterator : IWorkbookIterator
    {
        private readonly IWorkbook InputWorkbook;
        private readonly IWorkbook OutputWorkbook;
        private readonly Func<IList<string>, IList<string>> ListWorkerFunc;
        private readonly IFeedbackReceiver FeedbackReceiver;

        public WorkbookRowIterator(IWorkbook input, IWorkbook output, Func<IList<string>, IList<string>> listWorkerFunc, IFeedbackReceiver feedbackReceiver)
        {
            this.InputWorkbook = input;
            this.OutputWorkbook = output;
            this.ListWorkerFunc = listWorkerFunc;
            this.FeedbackReceiver = feedbackReceiver;
        }

        public void Iterate()
        {
            this.IterateCellsInWorkbook();
        }

        private void IterateCellsInWorkbook()
        {
            var numSheets = InputWorkbook.NumberOfSheets;

            for (var sheetIndex = 0; sheetIndex < numSheets; ++sheetIndex)
            {
                var inputSheet = InputWorkbook.GetSheetAt(sheetIndex);
                var outputSheet = this.OutputWorkbook.CreateSheet(inputSheet.SheetName);

                this.FeedbackReceiver.Message($"Processing sheet {sheetIndex+1} of {numSheets}");
                this.IterateCellsInSheet(inputSheet, outputSheet);
            }
        }

        private void IterateCellsInSheet(ISheet inputSheet, ISheet outputSheet)
        {
            var numRows = inputSheet.LastRowNum - inputSheet.FirstRowNum + 1;

            for (var rowIndex = inputSheet.FirstRowNum; rowIndex <= inputSheet.LastRowNum; ++rowIndex)
            {
                var inputRow = inputSheet.GetRow(rowIndex);
                var outputRow = outputSheet.CreateRow(inputRow.RowNum);

                this.FeedbackReceiver.Message($"Processing row {rowIndex+1} of {numRows}");
                this.IterateCellsInRow(inputRow, outputRow);
            }
        }

        private void IterateCellsInRow(IRow inputRow, IRow outputRow)
        {
            var texts = inputRow.Cells.Select(c => c.ToString()).ToList();
            var translations = this.ListWorkerFunc(texts);
            var translationEnumerator = translations.GetEnumerator();

            foreach (var inputCell in inputRow.Cells)
            {
                var outputCell = outputRow.CreateCell(inputCell.ColumnIndex, inputCell.CellType);

                outputCell.CellStyle.CloneStyleFrom(inputCell.CellStyle);
                if (translationEnumerator.MoveNext())
                    outputCell.SetCellValue(translationEnumerator.Current);
            }

            translationEnumerator.Dispose();
        }
    }
}
