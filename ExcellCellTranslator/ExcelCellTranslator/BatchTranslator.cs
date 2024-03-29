﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using ExcelService;
using Google;
using TranslationService;

namespace ExcelCellTranslator
{
    public class BatchTranslator : DatabaseTranslator, IBatchProcessor
    {
        private const int MaxRetryCount = 10;
        private const int RetryPauseSeconds = 10;

        private readonly ILanguageTranslator Translator;

        public BatchTranslator(SqlConnection connection, ILanguageTranslator languageTranslator,
            IFeedbackReceiver feedbackReceiver) 
            : base(connection, feedbackReceiver)
        {
            this.Translator = languageTranslator;
        }

        public void Execute()
        {
            var continueProcessing = true;
            var processed = 0;

            while (continueProcessing)
            {
                var batch = GetNextProcessingBatch(CommandBuilder.GenerateGetNextImportedBatchCommand);

                continueProcessing = batch?.Count > 0;

                if (continueProcessing)
                {
                    FeedbackReceiver.Message($"Processing items {processed+1}..{processed+batch.Count}");
                    var translated = ProcessBatch(batch);
                    UpdateProcessedBatch(translated);
                    processed += batch.Count;
                }
            }
        }

        private void UpdateProcessedBatch(IList<TranslationData> batch)
        {
            foreach (var workItem in batch)
            {
                UpdateProcessedItem(workItem);
            }
        }

        private void UpdateProcessedItem(TranslationData workItem)
        {
            this.FeedbackReceiver.Message($"Processing {workItem.SheetName}.R{workItem.RowId}C{workItem.ColumnId}");

            using var command = CachedConnection.GenerateProcessedInsertCommand(workItem);

            command.ExecuteNonQuery();
        }

        private IList<TranslationData> ProcessBatch(IList<TranslationData> batch)
        {
            var translated = new List<TranslationData>(batch.Count);

            foreach (var workItem in batch)
            {
                var cached = GetCachedOrDefault(workItem.Text);

                translated.Add(workItem.Morph(cached ?? ExecuteTranslate(workItem.Text)));
            }

            return translated;
        }

        private string GetCachedOrDefault(string text)
        {
            using var command = CachedConnection.GenerateGetTranslationCommand(text);
            var returned = command.ExecuteScalar();

            if (returned != null && !returned.Equals(DBNull.Value))
                return returned.ToString();

            return null;
        }

        private string ExecuteTranslate(string source)
        {
            var retries = 0;

            do
            {
                try
                {
                    return this.Translator.Translate(source);
                }
                catch (GoogleApiException gax)
                {
                    this.FeedbackReceiver.Error($"{retries + 1}/{MaxRetryCount} - {gax.Message}");
                    Thread.Sleep(RetryPauseSeconds * 1000);
                }
            } while (++retries < MaxRetryCount);

            throw new Exception("Translation API retry limit exceeded");
        }
    }
}
