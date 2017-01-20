﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataContracts
{
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SearchResult", Namespace = "http://schemas.datacontract.org/2004/07/DataContracts")]
    public partial class SearchResult : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string[] LinesField;

        private int PageAmountField;

        private int PageNoField;

        private int PageSizeField;

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
    Result UploadFile(UploadedFile request);

    [System.ServiceModel.OperationContractAttribute(Action = "DataContracts/ILineService/UploadFile", ReplyAction = "DataContracts/ILineService/UploadFileResponse")]
    System.Threading.Tasks.Task<Result> UploadFileAsync(UploadedFile request);

    [System.ServiceModel.OperationContractAttribute(Action = "DataContracts/ILineService/FindLines", ReplyAction = "DataContracts/ILineService/FindLinesResponse")]
    DataContracts.SearchResult FindLines(string substring, int pageNo, int pageSize);

    [System.ServiceModel.OperationContractAttribute(Action = "DataContracts/ILineService/FindLines", ReplyAction = "DataContracts/ILineService/FindLinesResponse")]
    System.Threading.Tasks.Task<DataContracts.SearchResult> FindLinesAsync(string substring, int pageNo, int pageSize);
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
[System.ServiceModel.MessageContractAttribute(WrapperName = "Result", WrapperNamespace = "DataContracts", IsWrapped = true)]
public partial class Result
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "DataContracts", Order = 0)]
    public bool Value;

    public Result()
    {
    }

    public Result(bool Value)
    {
        this.Value = Value;
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
    Result ILineService.UploadFile(UploadedFile request)
    {
        return base.Channel.UploadFile(request);
    }

    public bool UploadFile(string FileName, long Length, System.IO.Stream FileByteStream)
    {
        UploadedFile inValue = new UploadedFile();
        inValue.FileName = FileName;
        inValue.Length = Length;
        inValue.FileByteStream = FileByteStream;
        Result retVal = ((ILineService)(this)).UploadFile(inValue);
        return retVal.Value;
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<Result> ILineService.UploadFileAsync(UploadedFile request)
    {
        return base.Channel.UploadFileAsync(request);
    }

    public System.Threading.Tasks.Task<Result> UploadFileAsync(string FileName, long Length, System.IO.Stream FileByteStream)
    {
        UploadedFile inValue = new UploadedFile();
        inValue.FileName = FileName;
        inValue.Length = Length;
        inValue.FileByteStream = FileByteStream;
        return ((ILineService)(this)).UploadFileAsync(inValue);
    }

    public DataContracts.SearchResult FindLines(string substring, int pageNo, int pageSize)
    {
        return base.Channel.FindLines(substring, pageNo, pageSize);
    }

    public System.Threading.Tasks.Task<DataContracts.SearchResult> FindLinesAsync(string substring, int pageNo, int pageSize)
    {
        return base.Channel.FindLinesAsync(substring, pageNo, pageSize);
    }
}
