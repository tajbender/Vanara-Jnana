using CommunityToolkit.Mvvm.Input;
using Jnana.Services;
using Jnana.ViewModels;
using Jnana.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Vanara_Jnana.exe.Models.Contracts;
using Vanara_Jnana.exe.Views.DockPanels;
using Vanara_Jnana.exe.Views.Pages;
using static Vanara_Jnana.exe.Models.Contracts.INavigationService;

namespace Jnana.Views;

public sealed partial class ShellPage : Page
{
    private NuGetsAreaViewModel NuGetViewModel { get; }
    private GitHubAreaViewModel GitHubVM { get; }
    private SamplesAreaViewModel SamplesVM { get; }
    private NavigationService _navigationService { get; }
    // TODO: Consider making this a user setting that can be persisted across sessions, or determining it based on the last visited area
    // TODO: @dahall this is where you currently set the default navigation target...
    private readonly INavigationService.Area defaultNavigationTarget = Area.Void; // INavigationService.Area.Void;
    private ObservableCollection<TabViewItem> Tabs { get; }

    public StandardUICommand NavigateCommand => new(StandardUICommandKind.Open);
    public ICommand OpenNewTabCommand { get; }
    public TabViewItem? SelectedTab { get; set; } = null;
    public Area DefaultNavigationTarget => this.defaultNavigationTarget;

    public ShellPage()
    {
        InitializeComponent();
        Tabs = new ObservableCollection<TabViewItem>();
        NuGetViewModel = new NuGetsAreaViewModel();
        GitHubVM = new GitHubAreaViewModel();
        SamplesVM = new SamplesAreaViewModel();
        _navigationService = new NavigationService(WorkbenchFrame); // TODO: Use dependency injection to provide the NavigationService instance, and consider making it a singleton if it doesn't need to maintain any state

        _ = NuGetViewModel.SynchronizePackageCacheAsync();

        OpenNewTabCommand = new RelayCommand<string>(OpenNewTab);
        NavigateCommand.ExecuteRequested += NavigateCommand_ExecuteRequested;

        // OnLoading: Navigate to the default area (Void) to ensure the main content area
        // is populated with a page, and to establish a consistent starting point for navigation
        // TODO: WARN: This is the initial navigation target, but it should be determined based on user settings or the last visited area to provide a more personalized experience
        _navigationService.NavigateTo(NavigationService.Area.Void);


        //AddNewTab("GitHub: Vanara", new NuGetsPage(NuGetViewModel));  // TODO: The NuGetsPage is currently crashing due to a NullReferenceException in the NuGetViewModel. Investigate and resolve this issue before enabling this tab.
        //AddNewTab("Samples", new SamplesPage(SamplesVM)); TODO: The SamplesPage is currently crashing due to a NullReferenceException in the SamplesViewModel. Investigate and resolve this issue before enabling this tab.
        //AddNewTab("NuGets", new NuGetsPage(NuGetViewModel));
        AddNewTab("Disassembler", new DisassemblerPage());
        AddNewTab("Utilities", new UtilitiesPage());
        AddNewTab("Settings", new SettingsPage());
        AddNewTab("Handle Inspector", new HandleInspectorPage());
        AddNewTab("Hex Editor", new HexEditorPage());
        //AddNewTab("Void", new VoidPage());
        //AddNewTab("File Opus", new FileManagementPage());
    }

    private void AddNewTab(string header, Page page, IconSource? iconSource = null)
    {
        Debug.Print($"Adding new tab with header: {header}, page: {page.GetType().Name}, icon: {(iconSource != null ? iconSource.GetType().Name : "null")}");

        try
        {
            var tabViewIconSource = iconSource ?? new SymbolIconSource { Symbol = Symbol.Document };
            var newTab = new TabViewItem
            {
                Content = new Frame(),
                Header = header,
                IconSource = tabViewIconSource,
            };

            // Navigate the new tab's frame to the specified page
            ((Frame)newTab.Content).Navigate(page.GetType());

            // Add new tab and select it
            MainTabView.TabItems.Add(newTab);
            MainTabView.SelectedItem = newTab;
        }
        catch (Exception ex)
        {
            Debug.Fail($"Failed to to create new tab {header}: {ex}");
        }
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

    private void MainTabView_AddTabButtonClick(TabView sender, object args)
    {
        Debug.WriteLine($"MainTabView_AddTabButtonClick({args}) Breadcrumb item clicked.");
        AddNewTab("Void", new VoidPage());
    }

    private void NavBreadcrumb_ItemClicked(object sender, BreadcrumbBarItemClickedEventArgs args)
    {
        Debug.WriteLine($"Breadcrumb item clicked: {args}");
        var clicked = args.Item.ToString();
    }

    private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        Debug.WriteLine($"Search query submitted: {args.QueryText} (TODO: Handle search query submission)");
        var query = args.QueryText;
        query = query.Trim();
    }

    private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        Debug.WriteLine($"Search box text changed: {sender.Text} {args.ToString()} (TODO: Handle text change)");
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            sender.ItemsSource = new List<string> { "ShellItem", "ShellFolder", "IShellItem", "ExplorerBrowser" };
        }
    }
}
