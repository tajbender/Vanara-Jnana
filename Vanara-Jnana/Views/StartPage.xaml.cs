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
    }

    //private void LoadAssemblies_Click(object sender, RoutedEventArgs e)
    //{
    //    Task.Factory.StartNew(async () =>
    //    {
    //        await foreach (var package in NuGetUtils.LoadNuGetPackageListAsync(Prefix, Nuget, CancellationToken))
    //            if (package.Identity.Id.StartsWith(Prefix + '.', StringComparison.OrdinalIgnoreCase))
    //                _packages.Add(package);
    //    }, CancellationToken);
    //}

    private void FeatureTile_OnClick(object? sender, EventArgs e)
    {
        Debug.WriteLine("Feature tile clicked.");
    }

    private void MainTabs_AddTabButtonClick(TabView sender, object args)
    {
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
            HomeFrame.Content = frame.Content;
        }
    }

}
