namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    public enum OperationResult
    {
        None = 0,
        Ok,
        Fail
    }

    public class ProgressEventArgs : EventArgs
    {
        public double Percent { get; set; }
    }

    public class OperationResponse
    {
        public string Message { get; set; }
        public OperationResult Result { get; set; }
    }

    public class SearchLinesResponse : OperationResponse
    {
        public IEnumerable<string> FoundLines { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageAmount { get; set; }
    }


    public class Presenter
    {
        public event EventHandler<ProgressEventArgs> FileUploadingProgress;

        public async Task<OperationResponse> UploadFileAsync(FileInfo fileInfo)
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
                progressedStream.ProgressChanged += ProgressedStreamOnProgressChanged;
                stopwatch.Start();
                var result = await lineClient.UploadFileAsync(fileInfo.FullName, fileInfo.Length, progressedStream);
                stopwatch.Stop();

                if (result.Value)
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

        protected virtual void OnFileUploadingProgress(ProgressEventArgs e)
        {
            FileUploadingProgress?.Invoke(this, e);
        }

        private void ProgressedStreamOnProgressChanged(object sender, ProgressedStream.ProgressChangedEventArgs e)
        {
            var percent = (e.Length == 0)
                ? 0.0
                : (double)e.BytesRead / (double)e.Length;

            OnFileUploadingProgress(new ProgressEventArgs { Percent = percent });
        }
    }
}
