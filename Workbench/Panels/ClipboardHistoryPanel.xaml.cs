using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.DataTransfer;

namespace Jnana.Workbench.Panels;

public sealed partial class ClipboardHistoryPanel : UserControl
{
    public struct ClipboardItem
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

    private ObservableCollection<ClipboardItem> clipboardItems = [];

    public ObservableCollection<ClipboardItem> ClipboardItems { get => this.clipboardItems; }

    public ClipboardHistoryPanel()
    {
        InitializeComponent();

        Clipboard.ContentChanged += Clipboard_ContentChanged;
        //        Clipboard.GetContentAsync().AsTask().ContinueWith(task =>
        //        {
        //            if (task.Result != null)
        //            {
        //                var content = task.Result;
        //                if (content.Contains(StandardDataFormats.Text))
        //                {
        //                    var text = content.GetTextAsync().AsTask().Result;
        //                    this.clipboardItems.Add(new ClipboardItem
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

        this.clipboardItems =
        [
            new ClipboardItem
            {
                Content = "Sample text 1",
                Timestamp = DateTime.Now,
                Source = "Application A",
                Type = "Text",
                Format = "Plain Text",
                Metadata = "Metadata 1",
                AdditionalInfo = "Additional info 1",
                User = "User A",
                Application = "App A"
            },
            new ClipboardItem
            {
                Content = "Sample text 2",
                Timestamp = DateTime.Now.AddMinutes(-5),
                Source = "Application B",
                Type = "Text",
                Format = "Rich Text",
                Metadata = "Metadata 2",
                AdditionalInfo = "Additional info 2",
                User = "User B",
                Application = "App B"
            }
        ];
    }

    private void Clipboard_ContentChanged(object? sender, object e)
    {
        this.clipboardItems.Add(new ClipboardItem
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
