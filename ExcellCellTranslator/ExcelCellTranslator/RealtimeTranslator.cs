﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ExcelService;
using Google;
using NPOI.XSSF.UserModel;
using TranslationService;

namespace ExcelCellTranslator
{
    internal class RealtimeTranslator
    {
        private const int MaxRetryCount = 10;
        private const int RetryPauseSeconds = 10;

        private readonly string InputFilename;
        private readonly string OutputFilename;
        private readonly ILanguageTranslator Translator;
        private readonly IFeedbackReceiver FeedbackReceiver;

        public RealtimeTranslator(string inputFilename, string outputFilename, ILanguageTranslator languageTranslator,
            IFeedbackReceiver feedbackReceiver)
        {
            this.InputFilename = inputFilename;
            this.OutputFilename = outputFilename;
            this.Translator = languageTranslator;
            this.FeedbackReceiver = feedbackReceiver;
        }

        public void TranslateFile()
        {
            using var outputStream = File.Create(this.OutputFilename);
            var input = new XSSFWorkbook(this.InputFilename);
            var output = new XSSFWorkbook();
            IWorkbookIterator worker = new WorkbookRowIterator(input, output, this.ListWorker, this.FeedbackReceiver);

            worker.Iterate();

            input.Close();
            output.Write(outputStream);
            output.Close();
        }

        private IList<string> ListWorker(IList<string> texts)
        {
            var retries = 0;

            do
            {
                try
                {
                    return this.Translator.Translate(texts);
                }
                catch (GoogleApiException gax)
                {
                    this.FeedbackReceiver.Error($"{retries+1}/{MaxRetryCount} - {gax.Message}");
                    Thread.Sleep(RetryPauseSeconds*1000);
                }
            } while (++retries < MaxRetryCount);

            throw new Exception("Translation API retry limit exceeded");
        }
    }
}