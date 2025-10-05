using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;
using NetScad.UI.ViewModels;

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

        public void GetCreateAxesView(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowViewModel(new CreateAxesView());
        }

        private async void _OpenFolderPickerAsync(object? sender, RoutedEventArgs e)
        {
            var folderPickerDataContext = new FolderPickerViewModel(this);
            await folderPickerDataContext.OpenFolderPickerAsync();
        }

        private async void _OpenFolderAsync(object? sender, RoutedEventArgs e)
        {
            var folderPickerDataContext = new FolderPickerViewModel(this);
            await folderPickerDataContext.OpenFolderAsync();
        }

        public void ToggleTheme(object sender, RoutedEventArgs e)
        {
            Application.Current.RequestedThemeVariant =
                Application.Current.ActualThemeVariant == ThemeVariant.Light
                    ? ThemeVariant.Dark
                    : ThemeVariant.Light;
        }
    }
}