﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataContracts.Responses
{
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Response", Namespace = "http://schemas.datacontract.org/2004/07/DataContracts.Responses")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DataContracts.Responses.SearchResponse))]
    public partial class Response : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private bool SuccessField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success
        {
            get
            {
                return this.SuccessField;
            }
            set
            {
                this.SuccessField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SearchResponse", Namespace = "http://schemas.datacontract.org/2004/07/DataContracts.Responses")]
    public partial class SearchResponse : DataContracts.Responses.Response
    {

        private string[] LinesField;

        private int PageAmountField;

        private int PageNoField;

        private int PageSizeField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Lines
        {
            get
            {
                return this.LinesField;
            }
            set
            {
                this.LinesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PageAmount
        {
            get
            {
                return this.PageAmountField;
            }
            set
            {
                this.PageAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PageNo
        {
            get
            {
                return this.PageNoField;
            }
            set
            {
                this.PageNoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PageSize
        {
            get
            {
                return this.PageSizeField;
            }
            set
            {
                this.PageSizeField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(Namespace = "DataContracts", ConfigurationName = "ILineService")]
public interface ILineService
{

    // CODEGEN: Generating message contract since the wrapper name (UploadedFile) of message UploadedFile does not match the default value (UploadFile)
    [System.ServiceModel.OperationContractAttribute(Action = "DataContracts/ILineService/UploadFile", ReplyAction = "DataContracts/ILineService/UploadFileResponse")]
    Response UploadFile(UploadedFile request);

    [System.ServiceModel.OperationContractAttribute(Action = "DataContracts/ILineService/UploadFile", ReplyAction = "DataContracts/ILineService/UploadFileResponse")]
    System.Threading.Tasks.Task<Response> UploadFileAsync(UploadedFile request);

    [System.ServiceModel.OperationContractAttribute(Action = "DataContracts/ILineService/FindLines", ReplyAction = "DataContracts/ILineService/FindLinesResponse")]
    DataContracts.Responses.SearchResponse FindLines(string substring, int pageNo, int pageSize);

    [System.ServiceModel.OperationContractAttribute(Action = "DataContracts/ILineService/FindLines", ReplyAction = "DataContracts/ILineService/FindLinesResponse")]
    System.Threading.Tasks.Task<DataContracts.Responses.SearchResponse> FindLinesAsync(string substring, int pageNo, int pageSize);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(WrapperName = "UploadedFile", WrapperNamespace = "DataContracts", IsWrapped = true)]
public partial class UploadedFile
{

    [System.ServiceModel.MessageHeaderAttribute(Namespace = "DataContracts")]
    public string FileName;

    [System.ServiceModel.MessageHeaderAttribute(Namespace = "DataContracts")]
    public long Length;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "DataContracts", Order = 0)]
    public System.IO.Stream FileByteStream;

    public UploadedFile()
    {
    }

    public UploadedFile(string FileName, long Length, System.IO.Stream FileByteStream)
    {
        this.FileName = FileName;
        this.Length = Length;
        this.FileByteStream = FileByteStream;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(WrapperName = "Response", WrapperNamespace = "DataContracts", IsWrapped = true)]
public partial class Response
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "DataContracts", Order = 0)]
    public bool Success;

    public Response()
    {
    }

    public Response(bool Success)
    {
        this.Success = Success;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface ILineServiceChannel : ILineService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class LineServiceClient : System.ServiceModel.ClientBase<ILineService>, ILineService
{

    public LineServiceClient()
    {
    }

    public LineServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public LineServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public LineServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public LineServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    Response ILineService.UploadFile(UploadedFile request)
    {
        return base.Channel.UploadFile(request);
    }

    public bool UploadFile(string FileName, long Length, System.IO.Stream FileByteStream)
    {
        UploadedFile inValue = new UploadedFile();
        inValue.FileName = FileName;
        inValue.Length = Length;
        inValue.FileByteStream = FileByteStream;
        Response retVal = ((ILineService)(this)).UploadFile(inValue);
        return retVal.Success;
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<Response> ILineService.UploadFileAsync(UploadedFile request)
    {
        return base.Channel.UploadFileAsync(request);
    }

    public System.Threading.Tasks.Task<Response> UploadFileAsync(string FileName, long Length, System.IO.Stream FileByteStream)
    {
        UploadedFile inValue = new UploadedFile();
        inValue.FileName = FileName;
        inValue.Length = Length;
        inValue.FileByteStream = FileByteStream;
        return ((ILineService)(this)).UploadFileAsync(inValue);
    }

    public DataContracts.Responses.SearchResponse FindLines(string substring, int pageNo, int pageSize)
    {
        return base.Channel.FindLines(substring, pageNo, pageSize);
    }

    public System.Threading.Tasks.Task<DataContracts.Responses.SearchResponse> FindLinesAsync(string substring, int pageNo, int pageSize)
    {
        return base.Channel.FindLinesAsync(substring, pageNo, pageSize);
    }
}
