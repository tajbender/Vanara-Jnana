using CommunityToolkit.Mvvm.ComponentModel;
using Jnana.Services;
using NuGet.Common;
using System.Collections.ObjectModel;

namespace Jnana.ViewModels;

public partial class NuGetsAreaViewModel : ObservableObject
{
    private readonly NuGetCatalogService nuGetCatalogService;
    private readonly NuGetCatalogMemoryCache nuGetCatalogMemoryCache;
    private readonly NuGetCatalogDiskCache _nuGetCatalogDiskCache;

    /// <summary>Get the current INuGetCatalogService that is in use.</summary>
    public INuGetCatalogService NuGetCatalogService => _nuGetCatalogDiskCache;

    public ObservableCollection<PackageVersionInfo> Packages { get; } = new();

    public NuGetsAreaViewModel(ILogger? logger = null)
    {
        nuGetCatalogService = new NuGetCatalogService("https://api.nuget.org/v3/index.json", logger);
        nuGetCatalogMemoryCache = new NuGetCatalogMemoryCache(nuGetCatalogService);

        var cachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jnana", "nuget-cache.json");

        _nuGetCatalogDiskCache = new NuGetCatalogDiskCache(nuGetCatalogMemoryCache, cachePath);
    }

    public async Task LoadPackagesAsync()
    {
        Packages.Clear();

        await foreach (var pkg in _nuGetCatalogDiskCache.SearchPackagesAsync("Vanara.", CancellationToken.None))
        {
            var latest = await _nuGetCatalogDiskCache.GetLatestStableVersionAsync(pkg.Id, CancellationToken.None);
            if (latest != null)
                Packages.Add(latest);
        }
    }
}
