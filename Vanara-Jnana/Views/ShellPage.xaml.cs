using CommunityToolkit.Mvvm.Input;
using Jnana.Models;
using Jnana.Services;
using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Jnana.Views;

public sealed partial class ShellPage : Page
{
    static readonly CancellationToken CancellationToken = CancellationToken.None;
    private NuGetsAreaViewModel NuGetsVM { get; }
    private GitHubAreaViewModel GitHubVM { get; }
    private SamplesAreaViewModel SamplesVM { get; }
    private NavigationService _navigationService { get; }
    // TODO: Consider making this a user setting that can be persisted across sessions, or determining it based on the last visited area
    // TODO: @dahall this is where you currently set the default navigation target...
    private readonly INavigationService.Area defaultNavigationTarget = INavigationService.Area.Void;
    private ObservableCollection<TabViewItem> Tabs { get; }

    public StandardUICommand NavigateCommand => new(StandardUICommandKind.Open);
    public ICommand OpenNewTabCommand { get; }

    public TabViewItem SelectedTab { get; set; }

    public ShellPage()
    {
        InitializeComponent();
        NuGetsVM = new NuGetsAreaViewModel();
        GitHubVM = new GitHubAreaViewModel();
        SamplesVM = new SamplesAreaViewModel();
        _navigationService = new NavigationService(MainFrame); // TODO: Use dependency injection to provide the NavigationService instance, and consider making it a singleton if it doesn't need to maintain any state

        // OnLoading: Navigate to the default area (Void) to ensure the main content area
        // is populated with a page, and to establish a consistent starting point for navigation
        // TODO: WARN: This is the initial navigation target, but it should be determined based on user settings or the last visited area to provide a more personalized experience
        _navigationService.NavigateTo(defaultNavigationTarget);

        OpenNewTabCommand = new RelayCommand<string>(OpenNewTab);
        NavigateCommand.ExecuteRequested += NavigateCommand_ExecuteRequested;
    }

    private void AddNewTab(string header, Page page)
    {
        var newTab = new TabViewItem
        {
            Header = header,
            IconSource = new SymbolIconSource { Symbol = Symbol.Document },
            Content = new Frame()
        };

        // Navigate the new tab's frame to the specified page
        ((Frame)newTab.Content).Navigate(page.GetType());

        // Add new tab and select it
        MainTabView.TabItems.Add(newTab);
        MainTabView.SelectedItem = newTab;
    }

    private void OpenNewTab(string? header)
    {
        var tab = new TabViewItem
        {
            Header = header ?? "New Tab",
            Content = new Frame()
        };

        ((Frame)tab.Content).Navigate(typeof(VoidPage));

        Tabs.Add(tab);
        SelectedTab = tab;
    }



    private void NavigateCommand_ExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
    {
        Debug.Print("TODO: NavigateCommand_ExecuteRequested()");
    }

    //public ICommand NavigateCommand => new RelayCommand<string>(areaName =>
    //{
    //    if (Enum.TryParse(areaName, out INavigationService.Area area))
    //        _navigationService.NavigateTo(area);
    //    else
    //    {
    //        Debug.WriteLine($"Invalid navigation target: {areaName}, navigating to default area instead");
    //        _navigationService.NavigateTo(defaultNavigationTarget);
    //    }
    //});

    //private void FeatureTile_Click(object sender, EventArgs e)
    //{
    //    NavigateCommand.Execute("GitHub"); // TODO: Determine the area to navigate to based on the clicked tile, this is just a placeholder
    //}

    private void MainTabView_AddTabButtonClick(TabView sender, object args)
    {
        AddNewTab("NuGets", new NuGetsPage());
    }

    private void NavBreadcrumb_ItemClicked(object sender, BreadcrumbBarItemClickedEventArgs args)
    {
        var clicked = args.Item.ToString();
        Debug.WriteLine($"Breadcrumb item clicked: {clicked} (TODO: Navigate to the clicked item)");
    }

    private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        var query = args.QueryText;
        query = query.Trim();
        Debug.WriteLine($"Search query submitted: {query} (TODO: Handle search query submission)");
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
