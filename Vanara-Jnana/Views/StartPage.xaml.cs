using ClassicSamplesBrowser.Vanara.NuGet;
using ClassicSamplesBrowser.Vanara.Reflection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using System.Collections.ObjectModel;
using NuGet.Packaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace ClassicSamplesBrowser.Views;

/// <summary><completionlist cref="=StartPage"></completionlist>
/// StartPage is the main page that is shown when the app is launched
/// and serves as a navigation hub for the various samples in the app.
/// </summary>
public sealed partial class StartPage : Page
{
    const string framework = "net8.0";
    private const string Prefix = "Vanara";
    readonly List<IPackageSearchMetadata> packages = [];
    static readonly ILogger logger = NullLogger.Instance; // TODO: Replace with actual logger if needed
    static readonly CancellationToken cancellationToken = CancellationToken.None;

    //internal ObservableCollection<IElementInfo> RootItems { get; } = [];
    public ObservableCollection<IPackageSearchMetadata> RootItems { get; } = [];



    public StartPage()
    {
        InitializeComponent();
        this.DataContext = this;

        this.Loading += StartPage_Loading;
    }

    private void StartPage_Loading(FrameworkElement sender, object args)
    {


    }

    private void OpenExplorer_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(ApiExplorerPage));
    }

    private void OpenSamples_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(SamplesPage));
    }

    private void LoadAssemblies_Click(object sender, RoutedEventArgs e)
    {
        Task.Factory.StartNew(async () =>
        {
            await foreach (var package in NuGetUtils.LoadNuGetPackageListAsync(Prefix, logger, cancellationToken))
                if (package.Identity.Id.StartsWith(Prefix + '.', StringComparison.OrdinalIgnoreCase))
                    packages.Add(package);
        }, cancellationToken);

        
    }
}
