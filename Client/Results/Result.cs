namespace Client.Results
{
    using System.Collections.Generic;

    public class Result
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public static Result Ok()
        {
            return new Result {Success = true, Message = "OK"};
        }

        public static Result Fail()
        {
            return new Result {Success = false, Message = "Something realy bad"};
        }

        public static Result Cancelled()
        {
            return new Result {Success = false, Message = "Operation was cancelled"};
        }

        public static Result FileNotExists()
        {
            return new Result {Success = false, Message = "The file does not exist"};
        }

        public static Result FileUploaded(long? milliseconds = null)
        {
            var message = (milliseconds.HasValue)
                ? $"The file is uploaded in {milliseconds/1000.0}s"
                : "The file is uploaded";
            return new Result {Success = true, Message = message};
        }

        public static Result FileNotUploaded()
        {
            return new Result {Success = false, Message = "The file is not uploaded"};
        }

        public static SearchResult EmptySubstring()
        {
            return new SearchResult {Success = false, Message = "Please enter a string to search"};
        }

        public static SearchResult LinesFound(IEnumerable<string> foundLines, int pageSize, int pageNo, int pageAmount )
        {
            return new SearchResult
            {
                Message = "Lines found",
                Success = true,
                FoundLines = foundLines,
                PageSize = pageSize,
                PageAmount = pageAmount,
                PageNo = pageNo
            };
        }
    }
}
