using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Services;
using NuGet.Common;
using NuGet.Versioning;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.ApplicationModel.Background;

namespace Jnana.ViewModels;

public partial class NuGetsAreaViewModel : ObservableObject
{
    private readonly NuGetCatalogService nuGetCatalogService;
    private readonly NuGetCatalogMemoryCache nuGetCatalogMemoryCache;
    private readonly NuGetCatalogDiskCache nuGetCatalogDiskCache;

    /// <summary>Get the current INuGetCatalogService that is in use.</summary>
    public INuGetCatalogService NuGetCatalogService => nuGetCatalogDiskCache;

    public ObservableCollection<PackageVersionInfo> Packages { get; } = new();

    public NuGetPackageDetailViewModel PackageDetailViewModel { get; }

    public ICommand SelectVersionCommand { get; }

    public NuGetsAreaViewModel(ILogger? logger = null)
    {
        nuGetCatalogService = new NuGetCatalogService("https://api.nuget.org/v3/index.json", logger);
        nuGetCatalogMemoryCache = new NuGetCatalogMemoryCache(nuGetCatalogService);

        var cachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jnana", "nuget-cache.json");

        nuGetCatalogDiskCache = new NuGetCatalogDiskCache(nuGetCatalogMemoryCache, cachePath);
        PackageDetailViewModel = new NuGetPackageDetailViewModel(nuGetCatalogDiskCache);

        SelectVersionCommand = new RelayCommand<PackageVersionInfo?>(OnVersionSelected);
    }

    public async Task SynchronizePackageCacheAsync()
    {
        // TODO: Instead of clearing the entire cache, consider implementing a more efficient synchronization strategy that only updates changed packages.
        Packages.Clear();

        await foreach (var pkg in nuGetCatalogDiskCache.SearchPackagesAsync("Vanara.", CancellationToken.None))
        {
            var latest = await nuGetCatalogDiskCache.GetLatestStableVersionAsync(pkg.Id, CancellationToken.None);
            if (latest != null)
                Packages.Add(latest);
        }
    }

    private async void OnVersionSelected(PackageVersionInfo? version)
    {
        if (version != null)
        {
            // TODO: Update package Detail view with the selected version
            await PackageDetailViewModel.LoadAsync(version.Id, NuGetVersion.Parse(version.Version), CancellationToken.None);
        }
        else
        {
            // Make sure all values are cleared if no version is selected
            PackageDetailViewModel.Clear();
        }
        // TODO: Update Details pane to show the selected version's details
    }
}
