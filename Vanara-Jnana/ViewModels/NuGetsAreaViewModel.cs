using ClassicSamplesBrowser.Vanara.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Services;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Jnana.ViewModels;

public partial class NuGetsAreaViewModel : ObservableObject
{
    private INuGetCatalogService nuGetCatalogService;
    private INuGetCatalogService nuGetCatalogMemoryCache;
    private INuGetCatalogService NuGetCatalogDiskCache;
    public ObservableCollection<PackageVersionInfo> Packages { get; } = new();

    public NuGetsAreaViewModel(ILogger? logger = null)
    {
        nuGetCatalogService = new NuGetCatalogService("https://api.nuget.org/v3/index.json", logger);
        nuGetCatalogMemoryCache = new NuGetCatalogMemoryCache(nuGetCatalogService);

        var cachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Jnana", "nuget-cache.json");

        NuGetCatalogDiskCache = new NuGetCatalogDiskCache(nuGetCatalogMemoryCache, cachePath);
    }

    public async Task LoadPackagesAsync()
    {
        Packages.Clear();

        await foreach (var pkg in NuGetCatalogDiskCache.SearchPackagesAsync("Vanara.", CancellationToken.None))
        {
            var latest = await NuGetCatalogDiskCache.GetLatestStableVersionAsync(pkg.Id, CancellationToken.None);
            if (latest != null)
                Packages.Add(latest);
        }
    }
}
