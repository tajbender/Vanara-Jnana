using ClassicSamplesBrowser.Models.Contracts;
using ClassicSamplesBrowser.Vanara.NuGet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace ClassicSamplesBrowser.Views;

/// <summary><completionlist cref="StartPage"></completionlist>
/// StartPage is the main page that is shown when the app is launched
/// and serves as a navigation hub for the various Views in the app.
/// </summary>
public sealed partial class StartPage : Page,
    INavigationAware
{
    const string Framework = "net8.0";
    private const string Prefix = "Vanara";
    readonly List<IPackageSearchMetadata> _packages = [];
    static readonly ILogger Nuget = NullLogger.Instance; // TODO: Replace with actual nuget if needed
    static readonly CancellationToken CancellationToken = CancellationToken.None;

    //internal ObservableCollection<IElementInfo> RootItems { get; } = [];
    //public ObservableCollection<IPackageSearchMetadata> RootItems { get; } = [];

    public StartPage()
    {
        InitializeComponent();
        DataContext = this;

        Loading += StartPage_Loading;
    }

    private void StartPage_Loading(FrameworkElement sender, object args)
    {
        try 
        {
            Task.Factory.StartNew(async () =>
            {
                await foreach (var package in NuGetUtils.LoadNuGetPackageListAsync(Prefix, Nuget, CancellationToken))
                    if (package.Identity.Id.StartsWith(Prefix + '.', StringComparison.OrdinalIgnoreCase))
                        _packages.Add(package);
            }, CancellationToken);

            NavBreadcrumb.ItemsSource = new List<string> { "Home", "APIs", "Shell", "IShellItem" };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading packages: {ex.Message}");
        }
    }

    private void FeatureTile_OnClick(object? sender, EventArgs e)
    {
        Debug.WriteLine("Feature tile clicked.");
    }

    private void MainTabs_AddTabButtonClick(TabView sender, object args)
    {
        Debug.WriteLine("Add tab button clicked.");
        var tab = new TabViewItem
        {
            Header = "New Tab",
            Content = new Frame()
        };

        sender.TabItems.Add(tab);
        sender.SelectedItem = tab;
    }
    private void MainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MainTabView.SelectedItem is TabViewItem tab &&
            tab.Content is Frame frame)
        {
            Debug.WriteLine("Tab selection changed.");
            HomeFrame.Content = frame.Content;
        }
    }

    private void NavBreadcrumb_ItemClicked(object sender, BreadcrumbBarItemClickedEventArgs args)
    {
        var clicked = args.Item.ToString();
        Debug.WriteLine($"Breadcrumb item clicked: {clicked} (TODO: Navigate to the clicked item)");
    }

    private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        Debug.WriteLine($"Search box text changed: {sender.Text} {args.ToString} (TODO: Handle text change)");
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            sender.ItemsSource = new List<string> { "ShellItem", "ShellFolder", "IShellItem", "ExplorerBrowser" };
        }
    }

    private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var query = args.QueryText;
        Debug.WriteLine($"Search query submitted: {query} (TODO: Handle search query submission)");
        query = query.Trim();
    }
}
