using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace Jnana.Controls;

public sealed partial class MarkdownPane : UserControl
{
    private MarkdownPaneViewModel ViewModel => (MarkdownPaneViewModel)DataContext;
    public MarkdownPane()
    {
        //InitializeComponent();
    }
}


internal class MarkdownPaneViewModel
{
    public MarkdownPaneViewModel()
    {
        OpenCommand = new RelayCommand(Open);
        SaveCommand = new RelayCommand(Save);
        TogglePreviewCommand = new RelayCommand(TogglePreview);
    }

    public ICommand OpenCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand TogglePreviewCommand { get; }
    public string MarkdownText { get; set; } = string.Empty;
    private bool _isPreviewVisible { get; set; } = true;

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
