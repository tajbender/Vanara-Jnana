using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.DataTransfer;


namespace Jnana.Views.DockPanels;

public sealed partial class QuickLaunchDockPanel : UserControl
{
    private QuickLaunchViewModel ViewModel { get; } = new QuickLaunchViewModel();

    public QuickLaunchDockPanel()
    {
        InitializeComponent();
        this.Drop += OnDrop;
        this.DragOver += OnDragOver;
    }

    private void OnDragOver(object sender, DragEventArgs e)
    {
        e.AcceptedOperation = DataPackageOperation.Copy;
    }

    private async void OnDrop(object sender, DragEventArgs e)
    {
        if (e.DataView.Contains(StandardDataFormats.StorageItems))
        {
            var items = await e.DataView.GetStorageItemsAsync();
            foreach (var item in items)
            {
                // TODO: QuickLaunchViewModel.AddItem(item.Path);
            }
        }
    }
}

public class QuickLaunchItem
{
    public string Path { get; set; }
    public string DisplayName { get; set; }
    public string Glyph { get; set; }
}

public class QuickLaunchViewModel
{
    public ObservableCollection<QuickLaunchItem> Items { get; } = new();

    public void AddItem(string path)
    {
        Items.Add(new QuickLaunchItem
        {
            Path = path,
            DisplayName = System.IO.Path.GetFileName(path),
            Glyph = GlyphFor(path)
        });

        // TODO: Persistenz in SQLite
    }

    private string GlyphFor(string path)
    {
        if (Directory.Exists(path)) return "\uE8B7"; // Ordner
        if (path.EndsWith(".exe")) return "\uE7C3";  // Rocket / Launch
        if (path.EndsWith(".txt")) return "\uE8A5";  // Document
        return "\uE8A5"; // Default
    }
}
