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
    private readonly INuGetCatalogService _catalog;

    public ObservableCollection<PackageVersionInfo> Packages { get; } = new();

    public NuGetsAreaViewModel(ILogger? logger = null)
    {
        _catalog = new NuGetCatalogService("https://api.nuget.org/v3/index.json", logger);
    }

    public async Task LoadPackagesAsync()
    {
        Packages.Clear();

        await foreach (var pkg in _catalog.SearchPackagesAsync("Vanara.", CancellationToken.None))
        {
            var latest = await _catalog.GetLatestStableVersionAsync(pkg.Id, CancellationToken.None);
            if (latest != null)
                Packages.Add(latest);
        }
    }
}
