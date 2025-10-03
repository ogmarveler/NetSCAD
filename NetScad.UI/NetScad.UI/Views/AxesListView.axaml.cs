using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using NetScad.UI.ViewModels;

namespace NetScad.UI.Views;

public partial class AxesListView : UserControl
{
    public AxesListView()
    {
        InitializeComponent();
        DataContext = new AxesListViewModel();
    }
}