using CommunityToolkit.Mvvm.Input;
using Jnana.Services;
using Jnana.Vanara.Controls;
using Jnana.ViewModels;
using Jnana.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Input;
using Vanara_Jnana.exe.Models.Contracts;
using Vanara_Jnana.exe.Services.Navigation.Providers;
using Vanara_Jnana.exe.Views.Pages;
using Vanara_Jnana.exe.Views.Tools;
using static Vanara_Jnana.exe.Models.Contracts.INavigationService;

namespace Jnana.Views;

public sealed partial class ShellPage : Page
{
    private NuGetsAreaViewModel NuGetsViewModel { get; }
    private GitHubAreaViewModel GitHubVM { get; }
    private SamplesAreaViewModel SamplesVM { get; }
    private NavigationService _navigationService { get; }
    // TODO: Consider making this a user setting that can be persisted across sessions, or determining it based on the last visited area
    // TODO: @dahall this is where you currently set the default navigation target...
    private readonly INavigationService.NavigationArea defaultNavigationTarget = NavigationArea.Void; // INavigationService.NavigationArea.Void;
    private ObservableCollection<TabViewItem> Tabs { get; }

    public StandardUICommand NavigateCommand => new(StandardUICommandKind.Open);
    public ICommand OpenNewTabCommand { get; }
    public TabViewItem? SelectedTab { get; set; } = null;
    public NavigationArea DefaultNavigationTarget => this.defaultNavigationTarget;

    public ObservableCollection<FeatureTile> FeatureTiles { get; }

    public ShellPage()
    {
        InitializeComponent();
        Tabs = new ObservableCollection<TabViewItem>();
        NuGetsViewModel = new NuGetsAreaViewModel();
        GitHubVM = new GitHubAreaViewModel();
        SamplesVM = new SamplesAreaViewModel();
        FeatureTiles = new ObservableCollection<FeatureTile>();

        _navigationService = new NavigationService(WorkbenchFrame);
        _navigationService.RegisterProvider(new WebViewProvider());
        _navigationService.RegisterProvider(new SettingsProvider());
        // TODO: _navigationService.RegisterProvider(new WorkbenchProvider());
        // TODO:_navigationService.RegisterProvider(new WebViewProvider());

        _navigationService.Navigated += (node) =>
        {
            ArgumentNullException.ThrowIfNull(node);
            Debug.WriteLine($"ShellPage.Navigated() to: {node.Title} ({node.PageType.Name})");
            Frame.Navigate(node.PageType, node.ViewModel);
        };


        _ = NuGetsViewModel.SynchronizePackageCacheAsync();

        OpenNewTabCommand = new RelayCommand<string>(OpenNewTab);
        NavigateCommand.ExecuteRequested += NavigateCommand_ExecuteRequested;

        // TODO: Set icon source e.g. <FontIconSource Glyph="&#xE7B8;" FontFamily="Segoe Fluent Icons" Foreground="{ThemeResource TextFillColorPrimaryBrush}" />

        FeatureTiles.Add(new FeatureTile().SetCommand(NavigateCommand).SetTitle("NuGets").SetSubtitle("Official Package Releases. Latest Release: 5.0.5"));
        FeatureTiles.Add(new FeatureTile().SetCommand(NavigateCommand).SetTitle("GitHub").SetSubtitle("Vanara on GitHub."));
        FeatureTiles.Add(new FeatureTile().SetCommand(NavigateCommand).SetTitle("Samples").SetSubtitle("Vanara Science Laboratory, Examples and Unit Tests."));
        FeatureTiles.Add(new FeatureTile().SetCommand(NavigateCommand).SetTitle("Assemblies").SetSubtitle("Explore Types, Members and Interfaces."));
        FeatureTiles.Add(new FeatureTile().SetCommand(NavigateCommand).SetTitle("Utilities").SetSubtitle("Tools, Helpers and Generators. Dump extended System Info."));
        FeatureTiles.Add(new FeatureTile().SetCommand(NavigateCommand).SetTitle("Settings").SetSubtitle("Settings, Version Info and Search for Help."));

        //                < controls:FeatureTile Command = "{x:Bind NavigateCommand}" Icon = "&#xE7B8;" Title = "NuGets" Subtitle = "Official Package Releases. Latest Release: 5.0.5" />
        //                < controls:FeatureTile Command = "{x:Bind NavigateCommand}" Icon = "&#xE9D5;" Title = "GitHub" Subtitle = "Vanara on GitHub." />
        //                < controls:FeatureTile Command = "{x:Bind NavigateCommand}" Icon = "&#xE8F1;" Title = "Samples" Subtitle = "Vanara Science Laboratory, Examples and Unit Tests." />
        //                < AppBarSeparator Margin = "4,0" />
        //                < controls:FeatureTile Command = "{x:Bind NavigateCommand}" Icon = "&#xEA86;" Title = "Assemblies" Subtitle = "Explore Types, Members and Interfaces." />
        //                < controls:FeatureTile Command = "{x:Bind NavigateCommand}" Icon = "&#xE90F;" Title = "Utilities" Subtitle = "Tools, Helpers and Generators. Dump extended System Info." />
        //                < AppBarSeparator Margin = "4,0" />
        //                < controls:FeatureTile Command = "{x:Bind NavigateCommand}" Icon = "&#xE713;" Title = "Settings" Subtitle = "Settings, Version Info and Search for Help." />



        // OnLoading: Navigate to the default area (Void) to ensure the main content area
        // is populated with a page, and to establish a consistent starting point for navigation
        // TODO: WARN: This is the initial navigation target, but it should be determined based on user settings or the last visited area to provide a more personalized experience
        // _navigationService.NavigateTo(NavigationArea.Void);


        //AddNewTab("GitHub: Vanara", new NuGetsPage(NuGetsViewModel));  // TODO: The NuGetsPage is currently crashing due to a NullReferenceException in the NuGetsViewModel. Investigate and resolve this issue before enabling this tab.
        //AddNewTab("Samples", new SamplesPage(SamplesVM)); TODO: The SamplesPage is currently crashing due to a NullReferenceException in the SamplesViewModel. Investigate and resolve this issue before enabling this tab.
        //AddNewTab("NuGets", new NuGetsPage(NuGetsViewModel));
        //AddNewTab("Disassembler", new DisassemblerPage());
        //AddNewTab("Utilities", new UtilitiesPage());
        AddNewTab("File Opus", new FileManagementPage());
        AddNewTab("Handle Inspector", new HandleInspectorPage());
        AddNewTab("Hex Editor", new HexEditorPage());
        //AddNewTab("WebView", new WebViewPanel());
        //AddNewTab("Void", new VoidPage(NuGetsViewModel));
        AddNewTab("Settings", new SettingsPage());
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

            if (page.GetType() == typeof(VoidPage))
            {
                Debug.WriteLine("Navigating to VoidPage with NuGetsViewModel.");
                var navOptions = new FrameNavigationOptions
                {
                    IsNavigationStackEnabled = true,
                    //TransitionInfoOverride = new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo()
                };

                // TODO: THis crashes. However, navigate to the New Tab created
                ((Frame)newTab.Content).NavigateToType(page.GetType(), NuGetsViewModel, navOptions);
            }
            else
            {
                Debug.WriteLine($"Navigating to {page.GetType().Name} without ViewModel.");
                ((Frame)newTab.Content).Navigate(page.GetType());
            }



            // Navigate the new tab's frame to the specified page
            //((Frame)newTab.Content).Navigate(page.GetType());
            //
            //((Frame)newTab.Content).NavigateToType(page.GetType(), 
            //    page.ViewModel, 
            //    FrameNavigationOptions.None);

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
        Debug.WriteLine($"MainTabView_AddTabButtonClick({args}) clicked. Adding new void tab.");
        AddNewTab("Void", new VoidPage(NuGetsViewModel));
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
