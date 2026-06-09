using NuGet.Common;
using NuGet.Configuration;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System.Runtime.CompilerServices;

namespace Jnana.Services;

[Obsolete("This class has been replaced by NuGetCatalogService")]
public sealed class NuGetDiscoveryService
{
    private readonly SourceRepository _repo;
    private readonly ILogger? _logger;

    public NuGetDiscoveryService(string sourceUrl, ILogger? logger = null)
    {
        var provider = new SourceRepositoryProvider(
            new PackageSourceProvider(NullSettings.Instance),
            Repository.Provider.GetCoreV3());

        _repo = provider.CreateRepository(new PackageSource(sourceUrl));
        _logger = logger;
    }

    // Search for package IDs matching the prefix
    public async IAsyncEnumerable<PackageId> SearchPackagesAsync(string prefix, [EnumeratorCancellation] CancellationToken token)
    {
        var search = await _repo.GetResourceAsync<PackageSearchResource>(token);
        var filter = new SearchFilter(includePrerelease: false);

        var results = await search.SearchAsync(prefix, filter, 0, 200, _logger, token);

        foreach (var r in results)
            yield return new PackageId(r.Identity.Id);
    }

    public static async IAsyncEnumerable<IPackageSearchMetadata> LoadLatestStablePackagesAsync(string prefix, ILogger logger, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
        var search = await repository.GetResourceAsync<PackageSearchResource>(cancellationToken);

        var filter = new SearchFilter(includePrerelease: true); // Let's filter manually
        var results = await search.SearchAsync(prefix, filter, skip: 0, take: 200, logger, cancellationToken);

        foreach (var pkg in results)
        {
            var versions = await pkg.GetVersionsAsync();

            var latestStable = versions
                .Where(v => !v.Version.IsPrerelease)
                .OrderBy(v => v.Version)
                .LastOrDefault();

            if (latestStable is null)
                continue;

            yield return pkg;
        }
    }

    public async Task<PackageContent> DownloadPackageAsync(string id, NuGetVersion version, CancellationToken token)
    {
        var download = await _repo.GetResourceAsync<DownloadResource>(token);

        var result = await download.GetDownloadResourceResultAsync(
            new PackageIdentity(id, version),
            new PackageDownloadContext(new SourceCacheContext()),
            SettingsUtility.GetGlobalPackagesFolder(NullSettings.Instance),
            _logger,
            token);

        using var reader = result.PackageReader;

        var dlls = reader.GetLibItems()
            .SelectMany(i => i.Items)
            .Where(f => f.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            .ToList();

        // TODO: var readme = await reader.GetReadmeAsync(token);
        // TODO: var icon = await reader.GetIconAsync(token);
        // return new PackageContent(id, version, dlls, readme, icon);

        return new PackageContent(id, version, dlls, null, null);
    }

    public static async Task<IReadOnlyList<string>> GetAssemblyFilesAsync(
        string id,
        NuGetVersion version,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
        var find = await repository.GetResourceAsync<FindPackageByIdResource>(cancellationToken);

        using var cache = new SourceCacheContext();
        using var stream = new MemoryStream();

        bool ok = await find.CopyNupkgToStreamAsync(
            id, version, stream, cache, logger, cancellationToken);

        if (!ok)
            return Array.Empty<string>();

        stream.Position = 0;
        using var reader = new PackageArchiveReader(stream);

        var dlls = reader.GetFiles()
            .Where(f => f.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            .ToList();

        return dlls;
    }

}
