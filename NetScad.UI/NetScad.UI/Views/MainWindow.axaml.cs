using Avalonia.Controls;
using Avalonia.Interactivity;
using NetScad.UI.ViewModels;
using System.Threading.Tasks;

namespace NetScad.UI.Views
{
    public partial class MainWindow : Window
    {
        // Set MainView as the initial content
        private object _mainViewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mainViewModel;
        }

        private async void OpenFolderPickerAsync(object? sender, RoutedEventArgs e)
        {
            var folderPickerDataContext = new FolderPickerViewModel(this);
            await folderPickerDataContext.OpenFolderPickerAsync();
        }

        public async Task OpenFolderAsync()
        {
            var folderPickerDataContext = new FolderPickerViewModel(this);
            await folderPickerDataContext.OpenFolderAsync();
        }
    }
}