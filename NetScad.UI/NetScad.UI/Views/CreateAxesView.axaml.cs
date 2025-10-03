using Avalonia.Controls;
using NetScad.UI.ViewModels;
using static NetScad.Core.Measurements.Selector;
using Avalonia.Interactivity;
using Avalonia;
using System.Threading.Tasks;

namespace NetScad.UI.Views;

public partial class CreateAxesView : UserControl
{
    public CreateAxesView()
    {
        InitializeComponent();
        DataContext = new CreateAxesViewModel();
    }

    // Convert from one unit to another
    private async void _ConvertInputsAsync(object? sender, RoutedEventArgs e)
    {
        if (DataContext is CreateAxesViewModel viewModel) { await viewModel.ConvertInputs(viewModel._decimalPlaces); }
    }
    private async void _CreateCustomAxisAsync(object? sender, RoutedEventArgs e)
    {
        if (DataContext is CreateAxesViewModel viewModel) { await viewModel.CreateCustomAxisAsync(); }
    }
    private async void _ClearInputsAsync(object? sender, RoutedEventArgs e)
    {
        if (DataContext is CreateAxesViewModel viewModel) { await viewModel.ClearInputs(); }
    }
}