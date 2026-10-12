using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;

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

    public ObservableCollection<ClipboardItem> ClipboardItems { get => this.clipboardItems; set => this.clipboardItems = value; }

    public ClipboardHistoryPanel()
    {
        InitializeComponent();
    }
}
