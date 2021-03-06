﻿namespace Client
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Customs;
    using NLog;
    using Results;

    public class Presenter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<Result> UploadFileAsync(FileInfo fileInfo,
            CancellationToken token = new CancellationToken(), IProgress<double> reporter = null)
        {
            try
            {
                if (fileInfo == null || !fileInfo.Exists)
                {
                    return Result.FileNotExists();
                }

                var stopwatch = new Stopwatch();
                Response response;
                using (var lineClient = new LineServiceClient())
                using (var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
                using (var progressedStream = new ProgressedStream(stream, token))
                {
                    progressedStream.ProgressChanged += (sender, e) =>
                    {
                        reporter?.Report((e.Length == 0)
                            ? 0.0
                            : (double) e.BytesRead/(double) e.Length);
                    };

                    stopwatch.Start();
                    response = await lineClient.UploadFileAsync(fileInfo.FullName, fileInfo.Length, progressedStream);
                    stopwatch.Stop();
                }

                return response.Success
                    ? Result.FileUploaded(stopwatch.ElapsedMilliseconds)
                    : Result.FileNotUploaded();
            }
            catch (OperationCanceledException)
            {
                return Result.Cancelled();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.ToString);
                return Result.Fail();
            }
        }

        public async Task<SearchResult> FindLinesAsync(string substring, int pageNo, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(substring))
            {
                return Result.EmptySubstring();
            }

            using (var lineClient = new LineServiceClient())
            {
                var response = await lineClient.FindLinesAsync(substring, pageNo, pageSize);
                return Result.LinesFound(response.Lines, response.PageSize, response.PageNo, response.PageAmount);
            }
        }
    }
}
