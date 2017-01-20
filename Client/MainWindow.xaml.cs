namespace Client
{
    using System.IO;
    using System.Linq;
    using System.Windows;
    using Microsoft.Win32;


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Presenter _presenter;

        public MainWindow(Presenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;

            _presenter.FileUploadingComplete += FileUploadingComplete;
            _presenter.LineSearchComplete += LineSearchComplete;
            _presenter.FileUploadingProgress += FileUploadingProgress;
        }

        private void ButtonFindLine_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonFindLine.IsEnabled = false;
            var textToFind = TextBoxFindLine.Text;
            _presenter.FindLines(textToFind);
        }

        private void ButtonOpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == true)
            {
                TextBoxFilename.Text = openDialog.FileName;
            }
        }

        private void ButtonLoadFile_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonLoadFile.IsEnabled = false;
            ButtonLoadFile.Content = "WAIT...";
            var filename = TextBoxFilename.Text;
            _presenter.UploadFile(new FileInfo(filename));
        }

        private void FileUploadingProgress(object sender, FileUploadingProgressEventArgs e)
        {
            Dispatcher.Invoke(() => ProgressBarFileUploading.Value = e.Percent);
        }

        private void FileUploadingComplete(object sender, FileUploadingResultEventArgs e)
        {
            Dispatcher.Invoke(() => {
                ProgressBarFileUploading.Value = 0;
                ButtonLoadFile.Content = "Upload File";
                ButtonLoadFile.IsEnabled = true;
                MessageBox.Show(e.Message);
            });
        }

        private void LineSearchComplete(object sender, LineSearchCompleteEventArgs e)
        {
            Dispatcher.Invoke(() => {
                ButtonFindLine.IsEnabled = true;
                var lines = e.Result.ToList();
                ListBoxFoundLines.Items.Clear();
                if (lines.Count == 0)
                {
                    MessageBox.Show("No lines found");
                }
                else
                {
                    lines.ForEach(o => ListBoxFoundLines.Items.Add(o));
                }
            });
        }
    }
}
