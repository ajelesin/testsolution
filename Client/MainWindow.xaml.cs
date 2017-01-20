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
        private const int PageSize = 25;
        private int _pageNo;
        private int _pageAmount;

        public MainWindow(Presenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            
            _presenter.FileUploadingProgress += FileUploadingProgress;
        }

        private void ButtonOpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog {Filter = "*.txt|*.txt"};
            if (openDialog.ShowDialog() == true)
            {
                TextBoxFilename.Text = openDialog.FileName;
            }
        }

        private async void ButtonLoadFile_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonLoadFile.IsEnabled = false;

            var filename = TextBoxFilename.Text;
            var uploadResult = await _presenter.UploadFileAsync(new FileInfo(filename));

            ButtonLoadFile.IsEnabled = true;
            ProgressBarFileUploading.Value = 0;

            MessageBox.Show(uploadResult.Message);
        }

        private void ButtonFindLine_OnClick(object sender, RoutedEventArgs e)
        {
            _pageNo = 1;
            FindLineEventHandler();
        }

        private void ButtonFirst_OnClick(object sender, RoutedEventArgs e)
        {
            if (_pageNo <= 1) return;

            _pageNo = 1;
            FindLineEventHandler();
        }

        private void ButtonPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            if (_pageNo <= 1) return;

            _pageNo -= 1;
            FindLineEventHandler();
        }

        private void ButtonNext_OnClick(object sender, RoutedEventArgs e)
        {
            if (_pageNo >= _pageAmount) return;

            _pageNo += 1;
            FindLineEventHandler();
        }

        private void ButtonLast_OnClick(object sender, RoutedEventArgs e)
        {
            if (_pageNo >= _pageAmount) return;

            _pageNo = _pageAmount;
            FindLineEventHandler();
        }

        private async void FindLineEventHandler()
        {
            ButtonFindLine.IsEnabled = false;
            ButtonFirst.IsEnabled = false;
            ButtonPrevious.IsEnabled = false;
            ButtonNext.IsEnabled = false;
            ButtonLast.IsEnabled = false;

            var searchedText = TextBoxFindLine.Text;
            var result = await _presenter.FindLinesAsync(searchedText, _pageNo, PageSize);

            ButtonFindLine.IsEnabled = true;
            ButtonFirst.IsEnabled = true;
            ButtonPrevious.IsEnabled = true;
            ButtonNext.IsEnabled = true;
            ButtonLast.IsEnabled = true;
            ListBoxFoundLines.Items.Clear();

            if (result.Result == OperationResult.Fail)
            {
                MessageBox.Show(result.Message);
                return;
            }

            _pageNo = result.PageNo;
            _pageAmount = result.PageAmount;

            result.FoundLines.ToList().ForEach(o => ListBoxFoundLines.Items.Add(o));
            ButtonCurrent.Content = $"{result.PageNo}/{result.PageAmount}";
        }

        private void FileUploadingProgress(object sender, ProgressEventArgs e)
        {
            Dispatcher.Invoke(() => {
                ProgressBarFileUploading.Value = e.Percent;
            });
        }
    }
}
