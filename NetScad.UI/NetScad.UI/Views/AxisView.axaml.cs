using Avalonia.Controls;
using Markdown.Avalonia;
using NetScad.UI.ViewModels;
using ReactiveUI;
using System;
using System.IO;

namespace NetScad.UI.Views;

public partial class AxisView : UserControl
{
    public AxisView()
    {
        InitializeComponent();
        DataContext = new AxisViewModel();
        LoadMarkdownAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/Guides/Axis.markdown"));  // Relative or absolute path
    }

    private async void LoadMarkdownAsync(string filePath)
    {
        try
        {
            var markdownContent = await File.ReadAllTextAsync(filePath);
            if (MarkdownView is MarkdownScrollViewer viewer)
            {
                viewer.Markdown = markdownContent; // Use Markdown property, not Document
                viewer.Plugins.HyperlinkCommand = ReactiveCommand.Create<string>(url =>
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                });
            }
        }
        catch (Exception ex)
        {
            // Fallback: Display error in the viewer
            if (MarkdownView is MarkdownScrollViewer viewer)
            {
                viewer.Markdown = $"**Error loading Markdown file:** {ex.Message}";
            }
        }
    }
}