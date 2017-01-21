namespace Client.Responses
{
    using System.Collections.Generic;

    public class SearchLinesResponse : OperationResponse
    {
        public IEnumerable<string> FoundLines { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageAmount { get; set; }
    }
}
