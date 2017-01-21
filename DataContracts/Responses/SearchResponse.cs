namespace DataContracts.Responses
{
    using System.Collections.Generic;

    public class SearchResponse : Response
    {
        public IEnumerable<string> Lines { get; set; }
        public int PageSize { get; set; }
        public int PageAmount { get; set; }
        public int PageNo { get; set; }
    }
}
