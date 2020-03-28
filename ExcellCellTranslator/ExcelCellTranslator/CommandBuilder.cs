using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using NPOI.SS.UserModel;

namespace ExcelCellTranslator
{
    public static class CommandBuilder
    {
        public static SqlCommand GenerateImportInsertCommand(this SqlConnection connection, ICell cell)
        {
            var command = connection.CreateCommand();

            command.CommandText = $"INSERT INTO [Imported] ([SheetName], [RowId], [ColumnId], [Text]) VALUES (@SheetName, @RowId, @ColumnId, @ImportText)";

            command.Parameters.Add("SheetName", SqlDbType.NVarChar, 256).Value = cell.Sheet.SheetName;
            command.Parameters.Add("RowId", SqlDbType.Int).Value = cell.RowIndex;
            command.Parameters.Add("ColumnId", SqlDbType.Int).Value = cell.ColumnIndex;
            var valueParameter = command.Parameters.Add("ImportText", SqlDbType.NVarChar, -1);

            if (cell.CellType == CellType.Blank)
                valueParameter.Value = DBNull.Value;
            else
                valueParameter.Value = cell.ToString();

            return command;
        }

        public static SqlCommand GenerateGetNextImportedBatchCommand(this SqlConnection connection, int batchSize)
        {
            var command = connection.CreateCommand();

            command.CommandText =
                $"SELECT TOP({batchSize}) [i].[SheetName], [i].[RowId], [i].[ColumnId], [i].[Text] FROM [Imported] AS[i] LEFT OUTER JOIN [Processed] AS[p] ON[i].[SheetName] = [p].[SheetName] AND [i].[RowId] = [p].[RowId] AND [i].[ColumnId] = [p].[ColumnId] WHERE p.[Text] IS NULL AND NOT ([i].[Text] IS NULL)";

            return command;
        }

        ////public static SqlCommand GenerateGetNextProcessedBatchCommand(this SqlConnection connection, int batchSize)
        ////{
        ////    var command = connection.CreateCommand();

        ////    command.CommandText =
        ////        $"SELECT TOP({batchSize}) SELECT TOP 1000 [i].[SheetName], [i].[RowId], [i].[ColumnId], ISNULL([p].[Text], [i].[Text]) AS [Text] FROM [Translations].[dbo].[Imported] AS[i] LEFT OUTER JOIN [Processed] AS [p] ON [i].[SheetName] = [p].[SheetName] AND [i].[RowId] = [p].[RowId] AND [i].[ColumnId] = [p].[ColumnId]";

        ////    return command;
        ////}

        public static SqlCommand GenerateGetNextProcessedRowCommand(this SqlConnection connection, string sheetName,
            int rowId)
        {
            var command = connection.CreateCommand();

            command.CommandText =
                "SELECT [i].[ColumnId], ISNULL([p].[Text], [i].[Text]) AS [Text] FROM [Imported] AS [i] LEFT OUTER JOIN [Processed] AS [p] ON [i].[SheetName] = [p].[SheetName] AND [i].[RowId] = [p].[RowId] AND [i].[ColumnId] = [p].[ColumnId] WHERE [i].[SheetName] = @SheetName AND [i].[RowId] = @RowId";

            command.Parameters.Add("SheetName", SqlDbType.NVarChar, 256).Value = sheetName;
            command.Parameters.Add("RowId", SqlDbType.Int).Value = rowId;

            return command;
        }

        public static SqlCommand GenerateProcessedInsertCommand(this SqlConnection connection, TranslationData workItem)
        {
            var command = connection.CreateCommand();

            command.CommandText =
                $"INSERT INTO [Processed] ([SheetName], [RowId], [ColumnId], [Text]) VALUES(@SheetName, @RowId, @ColumnId, @ImportText)";

            command.Parameters.Add("SheetName", SqlDbType.NVarChar, 256).Value = workItem.SheetName;
            command.Parameters.Add("RowId", SqlDbType.Int).Value = workItem.RowId;
            command.Parameters.Add("ColumnId", SqlDbType.Int).Value = workItem.ColumnId;
            command.Parameters.Add("ImportText", SqlDbType.NVarChar, -1).Value = workItem.Text;

            return command;
        }

        public static SqlCommand GenerateRowStatisticsCommand(this SqlConnection connection)
        {
            var command = connection.CreateCommand();

            command.CommandText =
                "SELECT [SheetName], [RowId], COUNT([ColumnId]) As [CellCount] FROM [Imported] GROUP BY [SheetName], [RowId] ORDER BY [SheetName], [RowId]";

            return command;
        }
    }
}
