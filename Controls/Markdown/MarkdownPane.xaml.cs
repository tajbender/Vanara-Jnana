using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Controls;

public sealed partial class MarkdownPane : UserControl
{
    private MarkdownPaneViewModel ViewModel => (MarkdownPaneViewModel)DataContext;
}

internal class MarkdownPaneViewModel
{
    private readonly ICommand _openCommand;
    private readonly ICommand _saveCommand;
    private readonly ICommand _togglePreviewCommand;
    private string _markdownText = string.Empty;
    private readonly bool _isPreviewVisible1 = true;

    public MarkdownPaneViewModel()
    {
        _openCommand = new RelayCommand(Open);
        _saveCommand = new RelayCommand(Save);
        _togglePreviewCommand = new RelayCommand(TogglePreview);
    }

    public ICommand OpenCommand => _openCommand;

    public ICommand SaveCommand => _saveCommand;

    public ICommand TogglePreviewCommand => _togglePreviewCommand;

    public string MarkdownText
    {
        get => _markdownText;
        set => _markdownText = value;
    }

    private bool _isPreviewVisible => _isPreviewVisible1;

    public Visibility PreviewVisibility => _isPreviewVisible ? Visibility.Visible : Visibility.Collapsed;

    private void Open()
    {
        // Implementation for opening a file
    }

    private void Save()
    {
        // Implementation for saving a file
    }

    private void TogglePreview()
    {
        // Implementation for toggling preview
    }
}