namespace DataContracts.Responses
{
    using System.ServiceModel;

    [MessageContract]
    public class Response
    {
        [MessageBodyMember]
        public bool Success { get; set; }
    }
}
