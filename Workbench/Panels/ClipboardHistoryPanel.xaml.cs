using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace Jnana.Workbench.Panels;

public struct ClipboardHistoryPanelItem
{
    public DataPackageView Content { get; set; }
    public DateTime Timestamp { get; set; }
    public string Source { get; set; }
    public ClipboardHistoryPanelItemType Type { get; set; }
    public string Format { get; set; }
    public string Metadata { get; set; }
    public string AdditionalInfo { get; set; }
    public string User { get; set; }
    public string Application { get; set; }

    public string PreviewText { get; set; }

    public static ClipboardHistoryPanelItem FromClipboardHistoryItem(ClipboardHistoryItem item)
    {
        return new ClipboardHistoryPanelItem
        {
            Content = item.Content, // TODO: Get content from item
            Timestamp = DateTime.Now, // TODO: Get timestamp from item
            Source = "Clipboard", // TODO: Get source from item
            Type = ClipboardHistoryPanelItemType.Text, // TODO: Determine type from item
            Format = "Text", // TODO: Get format from item
            Metadata = "Metadata from clipboard", // TODO: Get metadata from item
            AdditionalInfo = "Additional info from clipboard", // TODO: Get additional info from item
            User = Environment.UserName, // TODO: Get user from item
            Application = "Clipboard", // TODO: Get application from item
            PreviewText = "Dummy preview text", // TODO: Implement preview text generation based on content and format
        };
    }
}

public enum ClipboardHistoryPanelItemType // TODO: Use Vanara ClipboardHistoryItemType instead of this enum
{
    Text,
    Url,
    Image,
    File,
    Html
}

/// <summary>
/// See https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/Clipboard for full example.
/// 
/// TODO: Don't clear the whole list on every change, instead add/remove items based on the change event.
/// </summary>
public sealed partial class ClipboardHistoryPanel : UserControl
{
    private ObservableCollection<ClipboardHistoryPanelItem> clipboardItems = [];

    public ObservableCollection<ClipboardHistoryPanelItem> ClipboardItems { get => this.clipboardItems; }

    public static bool IsSystemClipboardHistoryEnabled => Clipboard.IsHistoryEnabled();

    public ClipboardHistoryPanel()
    {
        this.InitializeComponent();

        this.Loaded += async (_, _) =>
        {
            // Load initial clipboard items on panel loading
            await this.LoadItemsFromClipboardHistory();
        };

        // TODO: Implement clipboard content changed event handling
        //Clipboard.ContentChanged += this.Clipboard_ContentChanged;

        Clipboard.HistoryChanged += async (_, _) =>
        {
            var result = await Clipboard.GetHistoryItemsAsync();

            await this.DispatcherQueue.EnqueueAsync(() =>
            {
                this.ClipboardItems.Clear();

                foreach (var item in result.Items)
                {
                    this.ClipboardItems.Add(ClipboardHistoryPanelItem.FromClipboardHistoryItem(item));
                }
            });
        };
    }

    public async Task LoadItemsFromClipboardHistory()
    {
        if (!IsSystemClipboardHistoryEnabled)
        {
            // TODO: add current single clipboard item
            return;
        }

        var result = await Clipboard.GetHistoryItemsAsync();
        await this.DispatcherQueue.EnqueueAsync(() =>
        {
            this.ClipboardItems.Clear();
            foreach (var item in result.Items)
            {
                this.ClipboardItems.Add(ClipboardHistoryPanelItem.FromClipboardHistoryItem(item));
            }
        });
    }

    //    public void ClearSystemClipboardHistory()
    //    {
    //        Clipboard.ClearHistory();
    //        ClipboardItems.Clear();
    //    }
}
