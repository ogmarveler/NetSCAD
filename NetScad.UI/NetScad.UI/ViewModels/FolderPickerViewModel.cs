using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NetScad.UI.ViewModels
{
    public partial class FolderPickerViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _selectedFolderPath;

        private readonly Window _parentWindow;

        public FolderPickerViewModel(Window parentWindow)
        {
            _parentWindow = parentWindow;
            SelectedFolderPath = string.Empty;
        }

        [RelayCommand]
        public async Task OpenFolderPickerAsync()
        {
            var folderPicker = _parentWindow.StorageProvider;
            var options = new FolderPickerOpenOptions
            {
                Title = "Select a Folder",
                AllowMultiple = false
            };

            var result = await folderPicker.OpenFolderPickerAsync(options);
            if (result.Count > 0 && result[0].TryGetLocalPath() is string path)
            {
                SelectedFolderPath = path;
            }
        }
    }
}