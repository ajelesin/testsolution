namespace Client.EventArgs
{
    using System;

    public class ProgressChangedEventArgs : EventArgs
    {
        public long BytesRead { get; set; }
        public long Length { get; set; }
    }
}
