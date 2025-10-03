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

        public void GetAxesListView(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowViewModel(new AxesListView());
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