using NetScad.UI.Views;
using ReactiveUI;

namespace NetScad.UI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        // Set MainView as the initial content
        public object _mainViewContent;

        //public MainWindowViewModel() => _mainViewContent = new MainView();
        public MainWindowViewModel() => _mainViewContent = new CreateAxesView(); // Start with this view
        public MainWindowViewModel(object currentView) => _mainViewContent = currentView;

        public object MainViewContent
        {
            get => _mainViewContent;
            set => this.RaiseAndSetIfChanged(ref _mainViewContent, value);
        }

        public void LoadCreateAxesView() => _mainViewContent = new CreateAxesView();
    }
}