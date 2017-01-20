namespace DataContracts
{
    using System.ServiceModel;

    [ServiceContract(Namespace = "DataContracts")]
    public interface ILineService
    {
        [OperationContract]
        Result UploadFile(UploadedFile file);
        
        [OperationContract]
        SearchResult FindLines(string substring, int pageNo, int pageSize);
    }
}
