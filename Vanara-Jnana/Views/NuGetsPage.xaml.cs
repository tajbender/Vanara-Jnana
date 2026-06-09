using Jnana.Helpers;
using Jnana.Services;
using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;
using NuGet.Common;
using System.Diagnostics;

namespace Jnana.Views;

public sealed partial class NuGetsPage : Page
{
    private readonly ILogger? logger;
    private INuGetCatalogService nuGetCatalogService;
    private INuGetCatalogService nuGetCatalogCache;
    public NuGetsAreaViewModel ViewModel { get; }
    private CancellationToken cancelToken = CancellationToken.None;


    public NuGetsPage()
    {
        InitializeComponent();

        logger = new NullLogger();
        ViewModel = new NuGetsAreaViewModel(logger);

        nuGetCatalogService = new NuGetCatalogService("https://api.nuget.org/v3/index.json", logger);
        nuGetCatalogCache = new NuGetCatalogCache(nuGetCatalogService);

        ViewModel.LoadPackagesAsync().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Print($"Failed to load NuGet packages: {t.Exception}");
                //logger?.LogError(t.Exception, "Failed to load NuGet packages");
            }
        }, cancelToken);

    }
}
