namespace DataContracts
{
    using System.ServiceModel;
    using Requests;
    using Responses;

    [ServiceContract(Namespace = "DataContracts")]
    public interface ILineService
    {
        [OperationContract]
        Response UploadFile(UploadedFile file);
        
        [OperationContract]
        SearchResponse FindLines(string substring, int pageNo, int pageSize);
    }
}
