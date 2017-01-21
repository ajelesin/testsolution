namespace Client.Customs
{
    using System;
    using System.IO;
    using EventArgs;

    public class ProgressedStream : Stream
    {
        private readonly FileStream _file;
        private readonly long _length;
        private long _bytesRead;

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public ProgressedStream(FileStream file)
        {
            _file = file;
            _length = file.Length;
            _bytesRead = 0;
            ProgressChanged?.Invoke(this, new ProgressChangedEventArgs {BytesRead = _bytesRead, Length = _length});
        }

        public double GetProgress()
        {
            return ((double)_bytesRead) / _file.Length;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override void Flush() { }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get { return _bytesRead; }
            set { throw new NotImplementedException();}
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var result = _file.Read(buffer, offset, count);
            _bytesRead += result;
            ProgressChanged?.Invoke(this, new ProgressChangedEventArgs { BytesRead = _bytesRead, Length = _length });
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
