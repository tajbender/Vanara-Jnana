using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace Jnana.Workbench.Panels;

public struct ClipboardHistoryPanelItem
{
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public string Source { get; set; }
    public ClipboardHistoryPanelItemType Type { get; set; }
    public string Format { get; set; }
    public string Metadata { get; set; }
    public string AdditionalInfo { get; set; }
    public string User { get; set; }
    public string Application { get; set; }

    public string GetContentPreviewText(int maxPreviewLength = 100)
    {
        if (this.Content.Length > maxPreviewLength)
        {
            return this.Content[..maxPreviewLength] + "...";
        }
        else
        {
            return this.Content;
        }
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

    public ClipboardHistoryPanel()
    {
        this.InitializeComponent();

        this.Loaded += async (_, _) =>
        {
            // Load initial clipboard items on panel load
            await LoadInitialClipboardItems();
        };

        Clipboard.HistoryChanged += async (_, _) =>
        {
            var result = await Clipboard.GetHistoryItemsAsync();

            await DispatcherQueue.EnqueueAsync(() =>
            {
                ClipboardItems.Clear();

                foreach (var item in result.Items)
                {
                    ClipboardItems.Add(new ClipboardHistoryPanelItem
                    {
                        Content = "Unknown",
                        //Content = item.GetTextAsync().Result,
                        //Timestamp = DateTime.Now,
                        //Source = "Clipboard",
                        Type = ClipboardHistoryPanelItemType.Text,
                        //Format = "Plain Text",
                        //Metadata = "Metadata from clipboard",
                        //AdditionalInfo = "Additional info from clipboard",
                        //User = Environment.UserName,
                        //Application = "Clipboard"
                    });
                }
            });
        };
    }

    public async Task LoadInitialClipboardItems()
    {
        var result = await Clipboard.GetHistoryItemsAsync();
        await DispatcherQueue.EnqueueAsync(() =>
        {
            ClipboardItems.Clear();
            foreach (var item in result.Items)
            {
                ClipboardItems.Add(new ClipboardHistoryPanelItem
                {
                    Content = "Unknown",
                    //Content = item.GetTextAsync().Result,
                    //Timestamp = DateTime.Now,
                    //Source = "Clipboard",
                    Type = ClipboardHistoryPanelItemType.Text,
                    //Format = "Plain Text",
                    //Metadata = "Metadata from clipboard",
                    //AdditionalInfo = "Additional info from clipboard",
                    //User = Environment.UserName,
                    //Application = "Clipboard"
                });
            }
        });
    }

    public void ClearClipboardHistory()
    {
        Clipboard.ClearHistory();
        ClipboardItems.Clear();
    }
}
