using Avalonia.Controls;
using NetScad.UI.ViewModels;

namespace NetScad.UI.Views;

public partial class AboutView : UserControl
{
    public AboutView()
    {
        InitializeComponent();
        DataContext = new AboutViewModel();
    }
}