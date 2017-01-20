namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public class Presenter
    {
        public async void UploadFile(FileInfo fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
            {
                OnFileUploadingResult(new FileUploadingResultEventArgs
                {
                    Message = "The file does not exist",
                    Result = FileUploadingResult.Fail,
                });
                return;
            }

            Result result;
            var stopwatch = new Stopwatch();
            using (var lineClient = new LineServiceClient())
            {
                using (var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
                {
                    using (var progressedStream = new ProgressedStream(stream))
                    {
                        progressedStream.ProgressChanged += ProgressedStreamOnProgressChanged;
                        stopwatch.Start();
                        result = await lineClient.UploadFileAsync(fileInfo.FullName, fileInfo.Length, progressedStream);
                        stopwatch.Stop();
                    }
                }
            }
            if (result.Value)
            {
                OnFileUploadingResult(new FileUploadingResultEventArgs
                {
                    Message = "The file is uploaded in " + stopwatch.ElapsedMilliseconds / 1000 + "s",
                    Result = FileUploadingResult.Ok,
                });
            }
            else
            {
                OnFileUploadingResult(new FileUploadingResultEventArgs
                {
                    Message = "The file is not uploaded",
                    Result = FileUploadingResult.Fail
                });
            }
        }

        private void ProgressedStreamOnProgressChanged(object sender, ProgressedStream.ProgressChangedEventArgs e)
        {
            var percent = (e.Length == 0)
                ? 0.0
                : (double) e.BytesRead/(double) e.Length;

            OnFileUploadingProgress(new FileUploadingProgressEventArgs {Percent = percent});
        }

        public async void FindLines(string substring)
        {
            if (string.IsNullOrWhiteSpace(substring))
            {
                OnLineSearchComplete(new LineSearchCompleteEventArgs
                {
                    Result = new List<string>(),
                });
                return;
            }

            IEnumerable<string> foundLines;
            using (var lineClient = new LineServiceClient())
            {
                foundLines = await lineClient.FindLinesAsync(substring);
                lineClient.Close();
            }

            OnLineSearchComplete(new LineSearchCompleteEventArgs
            {
                Result = foundLines,
            });
        }

        public event EventHandler<FileUploadingResultEventArgs> FileUploadingComplete;
        public event EventHandler<FileUploadingProgressEventArgs> FileUploadingProgress;
        public event EventHandler<LineSearchCompleteEventArgs> LineSearchComplete; 

        protected virtual void OnFileUploadingResult(FileUploadingResultEventArgs e)
        {
            FileUploadingComplete?.Invoke(this, e);
        }

        protected virtual void OnFileUploadingProgress(FileUploadingProgressEventArgs e)
        {
            FileUploadingProgress?.Invoke(this, e);
        }

        protected virtual void OnLineSearchComplete(LineSearchCompleteEventArgs e)
        {
            LineSearchComplete?.Invoke(this, e);
        }
    }


    public enum FileUploadingResult
    {
        None = 0,
        Ok,
        Fail
    }

    public class FileUploadingResultEventArgs : EventArgs
    {
        public FileUploadingResult Result { get; set; }
        public string Message { get; set; }
    }

    public class FileUploadingProgressEventArgs : EventArgs
    {
        public double Percent { get; set; }
    }

    public class LineSearchCompleteEventArgs : EventArgs
    {
        public IEnumerable<string> Result { get; set; } 
    }


}
