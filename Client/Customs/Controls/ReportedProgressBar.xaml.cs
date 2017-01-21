namespace Client.Customs.Controls
{
    using System;

    /// <summary>
    /// Interaction logic for ReportedProgressBar.xaml
    /// </summary>
    public partial class ReportedProgressBar: IProgress<double>
    {
        public ReportedProgressBar()
        {
            InitializeComponent();
        }

        public void Report(double value)
        {
            this.Dispatcher.Invoke(() => this.Value = value);
        }
    }
}
