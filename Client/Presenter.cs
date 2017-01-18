namespace Client
{
    using System;
    using System.Collections.Generic;
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
                    Result = Client.FileUploadingResult.Fail,
                });
                return;
            }

            bool success;
            using (var lineClient = new LineServiceClient())
            {
                success = await lineClient.SaveLinesAsync(File.ReadAllLines(fileInfo.FullName));
            }

            if (success)
            {
                OnFileUploadingResult(new FileUploadingResultEventArgs
                {
                    Message = "The file is uploaded",
                    Result = Client.FileUploadingResult.Ok,
                });
            }
            else
            {
                OnFileUploadingResult(new FileUploadingResultEventArgs
                {
                    Message = "The file is not uploaded",
                    Result = Client.FileUploadingResult.Fail
                });
            }
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

        public event Action<object, FileUploadingResultEventArgs> FileUploadingComplete;
        public event Action<object, FileUploadingProgressEventArgs> FileUploadingProgress;
        public event Action<object, LineSearchCompleteEventArgs> LineSearchComplete; 

        protected virtual void OnFileUploadingResult(FileUploadingResultEventArgs eventArgs)
        {
            FileUploadingComplete?.Invoke(this, eventArgs);
        }

        protected virtual void OnFileUploadingProgress(FileUploadingProgressEventArgs eventArgs)
        {
            FileUploadingProgress?.Invoke(this, eventArgs);
        }

        protected virtual void OnLineSearchComplete(LineSearchCompleteEventArgs eventArgs)
        {
            LineSearchComplete?.Invoke(this, eventArgs);
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
