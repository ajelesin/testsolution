namespace Client.EventArgs
{
    using System;

    public class ProgressEventArgs : EventArgs
    {
        public double Percent { get; set; }
    }
}
