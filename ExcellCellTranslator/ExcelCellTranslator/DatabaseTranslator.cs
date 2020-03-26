using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ExcelService;

namespace ExcelCellTranslator
{
    public abstract class DatabaseTranslator
    {
        protected const int FetchBatchSize = 1000;

        protected readonly SqlConnection CachedConnection;
        protected readonly IFeedbackReceiver FeedbackReceiver;

        protected DatabaseTranslator(SqlConnection connection, IFeedbackReceiver feedbackReceiver)
        {
            this.CachedConnection = connection;
            this.FeedbackReceiver = feedbackReceiver;
        }

        protected virtual IList<TranslationData> GetNextProcessingBatch(Func<SqlConnection, int, SqlCommand> getBatchFunc)
        {
            using var command = getBatchFunc(CachedConnection, FetchBatchSize);
            using var data = command.ExecuteReader();

            if (!data.HasRows)
                return null;

            var batch = new List<TranslationData>();

            while (data.NextResult())
            {
                batch.Add(new TranslationData(data["SheetName"].ToString(), int.Parse(data["RowId"].ToString()),
                    int.Parse(data["ColumnId"].ToString()), data["Text"].ToString()));
            }

            return batch;
        }
    }
}