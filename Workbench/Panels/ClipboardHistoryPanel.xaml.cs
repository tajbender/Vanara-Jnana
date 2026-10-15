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

    public static ClipboardHistoryPanelItem FromClipboardHistoryItem(ClipboardHistoryItem item)
    {
        return new ClipboardHistoryPanelItem
        {
            Content = item.Content , // TODO: Get content from item
            Timestamp = DateTime.Now, // TODO: Get timestamp from item
            Source = "Clipboard", // TODO: Get source from item
            Type = ClipboardHistoryPanelItemType.Text, // TODO: Determine type from item
            Format = "Text", // TODO: Get format from item
            Metadata = "Metadata from clipboard", // TODO: Get metadata from item
            AdditionalInfo = "Additional info from clipboard", // TODO: Get additional info from item
            User = Environment.UserName, // TODO: Get user from item
            Application = "Clipboard" // TODO: Get application from item
        };
    }

    public string GetContentPreviewText(int maxPreviewLength = 100)
    {
        // TODO: See https://learn.microsoft.com/en-us/uwp/api/windows.applicationmodel.datatransfer.datapackageview?view=winrt-28000
        //        if(this.Format == "Plain Text" && this.Content != null)
        //        {
        //            var text = this.Content.GetTextAsync().AsTask().Result;
        //            if (text.Length > maxPreviewLength)
        //            {
        //                return text[..maxPreviewLength] + "...";
        //            }
        //            else
        //            {
        //                return text;
        //            }
        //        }

        return this.Content?.ToString() ?? string.Empty;
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
