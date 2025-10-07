using Avalonia;
using Avalonia.Styling;
using NetScad.UI.Views;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetScad.UI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        // Set MainView as the initial content
        private object _mainViewContent = new AxisView();

        //public MainWindowViewModel() => _mainViewContent = new MainView();
        public MainWindowViewModel()
        {
            MainViewContent = _mainViewContent; // Start with this view
            // Initialize menu commands
            NewCommand = ReactiveCommand.Create(LoadCreateAxesView);
            OpenCommand = ReactiveCommand.Create(() => { using Task _ = new MainWindow().OpenFolderAsync(); });
            ToggleCommand = ReactiveCommand.Create(ToggleTheme);
            AxisViewCommand = ReactiveCommand.Create(LoadAxisView);
        }

        public object MainViewContent
        {
            get => _mainViewContent;
            set => this.RaiseAndSetIfChanged(ref _mainViewContent, value);
        }

        // SPA - Swap out views
        public void LoadCreateAxesView() => MainViewContent = new CreateAxesView();
        public void LoadAxisView() => MainViewContent = new AxisView();

        public void ToggleTheme() => Application.Current?.RequestedThemeVariant =
                Application.Current.ActualThemeVariant == ThemeVariant.Light
                    ? ThemeVariant.Dark
                    : ThemeVariant.Light;

        public ICommand NewCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand AxisViewCommand { get; }
        public ICommand ToggleCommand { get; }
    }
}