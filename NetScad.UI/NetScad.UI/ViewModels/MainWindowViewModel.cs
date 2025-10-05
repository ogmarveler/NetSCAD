using Avalonia;
using NetScad.UI.Views;
using ReactiveUI;
using System.Windows.Input;

namespace NetScad.UI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        // Set MainView as the initial content
        public object _mainViewContent;

        //public MainWindowViewModel() => _mainViewContent = new MainView();
        public MainWindowViewModel()
        {
            _mainViewContent = new CreateAxesView(); // Start with this view
            // Initialize menu commands
            NewCommand = ReactiveCommand.Create(() => { /* Handle New */ });
            OpenCommand = ReactiveCommand.Create(() => { /* Handle Open */ });
            CutCommand = ReactiveCommand.Create(() => { /* Handle Cut */ });
            CopyCommand = ReactiveCommand.Create(() => { /* Handle Copy */ });
            PasteCommand = ReactiveCommand.Create(() => { /* Handle Paste */ });
            AboutCommand = ReactiveCommand.Create(() => { /* Show About dialog */ });
        }
        public MainWindowViewModel(object currentView) => _mainViewContent = currentView;

        public object MainViewContent
        {
            get => _mainViewContent;
            set => this.RaiseAndSetIfChanged(ref _mainViewContent, value);
        }

        public void LoadCreateAxesView() => _mainViewContent = new CreateAxesView();

        public ICommand NewCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand CutCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand AboutCommand { get; }
    }
}