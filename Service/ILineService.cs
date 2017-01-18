namespace Service
{
    using System.ServiceModel;

    [ServiceContract(Namespace = "Service")]
    public interface ILineService
    {
        [OperationContract]
        bool SaveLines(string[] lines);
        
        [OperationContract]
        string[] FindLines(string substring);
    }
}
