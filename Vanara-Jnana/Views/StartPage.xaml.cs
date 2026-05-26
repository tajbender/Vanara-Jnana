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
    static readonly CancellationToken CancellationToken = CancellationToken.None;

    private NuGetViewModel NuGetVM { get; }
    private GitHubViewModel GitHubVM { get; }
    private SamplesViewModel SamplesVM { get; }

    public StartPage()
    {
        InitializeComponent();
        DataContext = this;
        NuGetVM = new NuGetViewModel();
        GitHubVM = new GitHubViewModel();
        SamplesVM = new SamplesViewModel();
        TabNavigationService.Initialize(MainTabView);
        Loading += StartPage_Loading;
    }

    private void StartPage_Loading(FrameworkElement sender, object args)
    {
        try
        {
            TabNavigationService.AddNuGetsPageTab();
            TabNavigationService.AddShellPageTab();
            TabNavigationService.AddSamplesPageTab();
            TabNavigationService.AddApiExplorerPageTab(typeof(ApiExplorerPage));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"StartPage_Loading() {ex.Message}");
        }
    }

    private void FeatureTile_OnClick(object? sender, EventArgs e)
    {
        Debug.WriteLine($"FeatureTile_OnClick({sender}, {e})");
    }

    private void MainTabs_AddTabButtonClick(TabView sender, object args)
    {
        Debug.WriteLine($"MainTabs_AddTabButtonClick({sender}, {args})");
        TabNavigationService.AddApiExplorerPageTab(typeof(ApiExplorerPage));
        //        var tab = new TabViewItem
        //        {
        //            Header = "NuGet",
        //            Content = new NuGetsPage { DataContext = NuGetVM }
        //        };
        //
        //        sender.TabItems.Add(tab);
        //        sender.SelectedItem = tab;
    }

    private void MainTabs_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        Debug.WriteLine($"MainTabs_SelectionChanged({sender}, {args})");

        if (MainTabView.SelectedItem is TabViewItem tab &&
            tab.Content is Frame frame)
        {
            Debug.WriteLine($".Content: {frame}");
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
