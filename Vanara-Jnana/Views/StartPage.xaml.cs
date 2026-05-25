using ClassicSamplesBrowser.Models.Contracts;
using ClassicSamplesBrowser.Services;
using ClassicSamplesBrowser.Vanara.NuGet;
using ClassicSamplesBrowser.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;

namespace ClassicSamplesBrowser.Views;

/// <summary><completionlist cref="StartPage"></completionlist>
/// StartPage is the main page that is shown when the app is launched
/// and serves as a navigation hub for the various Views in the app.
/// </summary>
public sealed partial class StartPage : Page,
    INavigationAware
{
    const string Framework = "net8.0";      // Imported from dahall's code, but not currently used. Consider removing if not needed.
    private const string Prefix = "Vanara"; // The prefix to filter NuGet packages by. This is a simple string match and can be adjusted as needed.
    readonly List<IPackageSearchMetadata> _packages = [];
    static readonly ILogger Nuget = NullLogger.Instance; // TODO: Replace with actual nuget if needed
    static readonly CancellationToken CancellationToken = CancellationToken.None;

    private NuGetViewModel NuGetVM { get; }
    private GitHubViewModel GitHubVM { get; }
    private SamplesViewModel SamplesVM { get; }

    public StartPage()
    {
        InitializeComponent();
        DataContext = this;

        TabNavigationService.Initialize(MainTabView);
        Loading += StartPage_Loading;
        
        NuGetVM = new NuGetViewModel();
        GitHubVM = new GitHubViewModel();
        SamplesVM = new SamplesViewModel();

        TabNavigationService.AddApiExplorerPageTab(typeof(StartPage));
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
            Header = "NuGet",
            Content = new NuGetsPage { DataContext = NuGetVM }
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
            TabViewContentPresenter.Content = frame.Content;
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
