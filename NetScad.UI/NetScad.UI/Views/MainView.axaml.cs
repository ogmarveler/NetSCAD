using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using NetScad.UI.ViewModels;

namespace NetScad.UI.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

    private async void OpenFolderButton_Clicked(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions { Title = "Select a Folder" });

        if (folders.Count >= 1)
        {
            //var folderPath = folders.Path.LocalPath;
            // Use the selected folder path
        }
    }
}