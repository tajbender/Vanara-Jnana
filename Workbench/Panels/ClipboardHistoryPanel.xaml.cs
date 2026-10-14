using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.DataTransfer;

namespace Jnana.Workbench.Panels;

public struct ClipboardHistoryItem
{
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public string Source { get; set; }
    public string Type { get; set; }
    public string Format { get; set; }
    public string Metadata { get; set; }
    public string AdditionalInfo { get; set; }
    public string User { get; set; }
    public string Application { get; set; }
}

public sealed partial class ClipboardHistoryPanel : UserControl
{

    private ObservableCollection<ClipboardHistoryItem> clipboardItems = [];

    public ObservableCollection<ClipboardHistoryItem> ClipboardItems { get => this.clipboardItems; }

    public ClipboardHistoryPanel()
    {
        this.InitializeComponent();

        Clipboard.ContentChanged += this.Clipboard_ContentChanged;
    }

    private void Clipboard_ContentChanged(object? sender, object e)
    {
//        Clipboard.GetContentAsync().AsTask().ContinueWith(task =>
//        {
//            if (task.Result != null)
//            {
//                var content = task.Result;
//                if (content.Contains(StandardDataFormats.Text))
//                {
//                    var text = content.GetTextAsync().AsTask().Result;
//                    this.clipboardItems.Add(new ClipboardHistoryItem
//                    {
//                        Content = text,
//                        Timestamp = DateTime.Now,
//                        Source = "Clipboard",
//                        Type = "Text",
//                        Format = "Plain Text",
//                        Metadata = "Metadata from clipboard",
//                        AdditionalInfo = "Additional info from clipboard",
//                        User = Environment.UserName,
//                        Application = "Clipboard"
//                    });
//                }
//            }
//        });

        this.clipboardItems.Add(new ClipboardHistoryItem
        {
            Content = "New clipboard item",
            Timestamp = DateTime.Now,
            Source = "Clipboard",
            Type = "Text",
            Format = "Plain Text",
            Metadata = "Metadata from clipboard",
            AdditionalInfo = "Additional info from clipboard",
            User = Environment.UserName,
            Application = "Clipboard"
        });
    }
}
