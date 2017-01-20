namespace DataContracts
{
    using System.Collections.Generic;
    
    public class SearchResult
    {
        public IEnumerable<string> Lines { get; set; }
        public int PageSize { get; set; }
        public int PageAmount { get; set; }
        public int PageNo { get; set; }
    }
}
