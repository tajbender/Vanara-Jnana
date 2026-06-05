using ClassicSamplesBrowser.Services;
using CommunityToolkit.Mvvm.Input;
using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using System;
using Windows.Foundation.Collections;
using Windows.Foundation;

namespace ClassicSamplesBrowser.Views;

public sealed partial class ShellPage : Page
{
    static readonly CancellationToken CancellationToken = CancellationToken.None;
    private NuGetAreaViewModel NuGetVM { get; }
    private GitHubAreaViewModel GitHubVM { get; }
    private SamplesAreaViewModel SamplesVM { get; }
    private NavigationService _navigationService { get; }
    public ShellPage()
    {
        InitializeComponent();
        NuGetVM = new NuGetAreaViewModel();
        GitHubVM = new GitHubAreaViewModel();
        SamplesVM = new SamplesAreaViewModel();

        _navigationService = new NavigationService(MainFrame); // TODO: Use dependency injection to provide the NavigationService instance, and consider making it a singleton if it doesn't need to maintain any state

        // OnLoading: Navigate to the default area (Void) to ensure the main content area is populated with a page, and to establish a consistent starting point for navigation
        _navigationService.NavigateTo(INavigationService.Area.Void);
        //_navigationService.NavigateTo(INavigationService.Area.Settings);
    }

    public ICommand NavigateCommand => new RelayCommand<string>(areaName =>
    {
        if (Enum.TryParse(areaName, out INavigationService.Area area))
            _navigationService.NavigateTo(area);
    });

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
