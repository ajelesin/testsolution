namespace DataContracts
{
    using System.ServiceModel;

    [ServiceContract(Namespace = "DataContracts")]
    public interface ILineService
    {
        [OperationContract]
        Result UploadFile(UploadedFile file);
        
        [OperationContract]
        string[] FindLines(string substring);
    }
}
