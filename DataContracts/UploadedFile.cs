namespace DataContracts
{
    using System;
    using System.IO;
    using System.ServiceModel;

    [MessageContract]
    public class UploadedFile : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public long Length { get; set; }

        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream { get; set; }

        public void Dispose()
        {
            if (FileByteStream == null)
                return;
            
            FileByteStream.Close();
            FileByteStream = null;
        }
    }

    [MessageContract]
    public class Result
    {
        [MessageBodyMember]
        public bool Value { get; set; }
    }
}
