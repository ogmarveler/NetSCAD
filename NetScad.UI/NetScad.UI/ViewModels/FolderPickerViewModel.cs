using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetScad.Core.Utility;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace NetScad.UI.ViewModels
{
    public partial class FolderPickerViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _selectedFolderPath;

        [ObservableProperty]
        private string? _folderPath;
        private readonly Window _parentWindow;

        public FolderPickerViewModel(Window parentWindow)
        {
            _parentWindow = parentWindow;
            SelectedFolderPath = Path.Combine(PathHelper.GetProjectRoot(),"Scad");
            _folderPath = Path.Combine(PathHelper.GetProjectRoot(), "Scad");
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

        [RelayCommand]
        public Task OpenFolderAsync()
        {
            if (string.IsNullOrEmpty(_folderPath))
            {
                return Task.CompletedTask;
            }

            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        Arguments = $"\"{_folderPath}\"",
                        UseShellExecute = true
                    });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "open",
                        Arguments = $"\"{_folderPath}\"",
                        UseShellExecute = true
                    });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "xdg-open",
                        Arguments = $"\"{_folderPath}\"",
                        UseShellExecute = true
                    });
                }
            }
            catch
            {
                // Handle exceptions silently to avoid UI disruption
            }

            return Task.CompletedTask;
        }
    }
}