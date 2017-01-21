namespace Client
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Customs;
    using Enums;
    using EventArgs;
    using Responses;

    public class Presenter
    {
        public async Task<OperationResponse> UploadFileAsync(
            FileInfo fileInfo,
            EventHandler<ProgressEventArgs> progressChangedEventHandler = null)
        {
            if (fileInfo == null || !fileInfo.Exists)
            {
                return new OperationResponse
                {
                    Message = "The file does not exist",
                    Result = OperationResult.Fail,
                };
            }
            

            using (var lineClient = new LineServiceClient())
            using (var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            using (var progressedStream = new ProgressedStream(stream))
            {
                var stopwatch = new Stopwatch();

                progressedStream.ProgressChanged += (sender, e) => {
                    progressChangedEventHandler?.Invoke(sender, new ProgressEventArgs {
                        Percent = (e.Length == 0) ? 0.0 : (double) e.BytesRead/(double) e.Length,
                    });
                };

                stopwatch.Start();
                var result = await lineClient.UploadFileAsync(fileInfo.FullName, fileInfo.Length, progressedStream);
                stopwatch.Stop();

                if (result.Success)
                {
                    return new OperationResponse
                    {
                        Message = "The file is uploaded in " + stopwatch.ElapsedMilliseconds / 1000 + "s",
                        Result = OperationResult.Ok,
                    };
                }

                return new OperationResponse
                {
                    Message = "The file is not uploaded",
                    Result = OperationResult.Fail,
                };
            }
        }

        public async Task<SearchLinesResponse> FindLinesAsync(string substring, int pageNo, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(substring))
            {
                return new SearchLinesResponse
                {
                    Message = "Please enter a string to search",
                    Result = OperationResult.Fail,
                };
            }

            using (var lineClient = new LineServiceClient())
            {
                var result = await lineClient.FindLinesAsync(substring, pageNo, pageSize);

                return new SearchLinesResponse
                {
                    Message = "Found",
                    Result = OperationResult.Ok,
                    FoundLines = result.Lines,
                    PageSize = result.PageSize,
                    PageNo = result.PageNo,
                    PageAmount = result.PageAmount,
                };
            }
        }
    }
}
