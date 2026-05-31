using ClassicSamplesBrowser.Models.Contracts;
using ClassicSamplesBrowser.Services;
using ClassicSamplesBrowser.Vanara.NuGet;
using ClassicSamplesBrowser.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
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
        TabNavigationService.Initialize(MainTabs);
        Loading += StartPage_Loading;

        //global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///Views/StartPage.xaml");
        //var resourceInfo = Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Views/StartPage.xaml").AsTask().Result;
        //Debug.WriteLine($"Resource locator: {resourceLocator}");

    }

    private void StartPage_Loading(FrameworkElement sender, object args)
    {
        try
        {
            TabNavigationService.AddNuGetsPageTab(selectTab: false);
            TabNavigationService.AddGitHubPageTab(selectTab: false);
            TabNavigationService.AddSamplesPageTab(selectTab: false);
            TabNavigationService.AddSettingsPageTab(selectTab: false);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"StartPage_Loading() {ex.Message}");
        }
    }

    private void NuGetsFeatureTile_OnClick(object? sender, EventArgs e) { Debug.WriteLine($"FeatureTile_OnClick({sender}, {e})"); }
    private void GitHubFeatureTile_OnClick(object? sender, EventArgs e) { Debug.WriteLine($"FeatureTile_OnClick({sender}, {e})"); }
    private void AssembliesFeatureTile_OnClick(object? sender, EventArgs e) { Debug.WriteLine($"FeatureTile_OnClick({sender}, {e})"); }
    private void SamplesFeatureTile_OnClick(object? sender, EventArgs e) { Debug.WriteLine($"FeatureTile_OnClick({sender}, {e})"); }
    private void UtilitiesFeatureTile_OnClick(object? sender, EventArgs e) { Debug.WriteLine($"FeatureTile_OnClick({sender}, {e})"); }
    private void MainTabs_AddTabButtonClick(TabView sender, object args)
    {
        Debug.WriteLine($"MainTabs_AddTabButtonClick({sender}, {args})");
        try
        {
            // var tab = new TabViewItem { Header = "NuGet", Content = new NuGetsPage { DataContext = NuGetVM } };
            // sender.TabItems.Add(tab);
            // sender.SelectedItem = tab;
            TabNavigationService.AddGitHubPageTab();  // TODO: .NavigateTo()
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MainTabs_AddTabButtonClick() {ex.Message}");
        }
    }
    private void MainTabs_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        Debug.WriteLine($"MainTabs_SelectionChanged({sender}, {args})");

        if (MainTabs.SelectedItem is TabViewItem tab &&
            tab.Content is Frame frame)
        {
            Debug.WriteLine($".Content: {frame}");
            // TODO: TabViewContentPresenter.Content = frame.Content;
        }
    }
    private void NavBreadcrumb_ItemClicked(object sender, BreadcrumbBarItemClickedEventArgs args)
    {
        var clicked = args.Item.ToString();
        Debug.WriteLine($"Breadcrumb item clicked: {clicked} (TODO: Navigate to the clicked item)");
    }
    private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var query = args.QueryText;
        Debug.WriteLine($"Search query submitted: {query} (TODO: Handle search query submission)");
        query = query.Trim();
    }
    private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        Debug.WriteLine($"Search box text changed: {sender.Text} {args.ToString} (TODO: Handle text change)");
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            sender.ItemsSource = new List<string> { "ShellItem", "ShellFolder", "IShellItem", "ExplorerBrowser" };
        }
    }
}
