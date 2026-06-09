using NuGet.Common;
using NuGet.Configuration;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jnana.Services;

public sealed class NuGetCatalogService : INuGetCatalogService
{
    private readonly SourceRepository _repo;
    private readonly ILogger? _logger;

    public NuGetCatalogService(string sourceUrl, ILogger? logger = null)
    {
        _repo = Repository.Factory.GetCoreV3(sourceUrl);
        _logger = logger;
    }

    public async IAsyncEnumerable<PackageId> SearchPackagesAsync(
        string prefix,
        [EnumeratorCancellation] CancellationToken token)
    {
        var search = await _repo.GetResourceAsync<PackageSearchResource>(token);
        var filter = new SearchFilter(includePrerelease: true);

        var results = await search.SearchAsync(prefix, filter, 0, 200, _logger, token);

        foreach (var r in results)
            yield return new PackageId(r.Identity.Id);
    }

    public async Task<PackageVersionInfo?> GetLatestStableVersionAsync(
        string packageId,
        CancellationToken token)
    {
        var search = await _repo.GetResourceAsync<PackageSearchResource>(token);
        var filter = new SearchFilter(includePrerelease: true);

        var results = await search.SearchAsync(packageId, filter, 0, 1, _logger, token);

        var pkg = results.FirstOrDefault();
        if (pkg is null)
            return null;

        var versions = await pkg.GetVersionsAsync();

        var latestStable = versions
            .Where(v => !v.Version.IsPrerelease)
            .OrderBy(v => v.Version)
            .LastOrDefault();

        if (latestStable is null)
            return null;

        return new PackageVersionInfo(
            pkg.Identity.Id,
            latestStable.Version.ToString(),
            pkg.IconUrl,
            pkg.Description,
            pkg.Summary
        );
    }

    public async Task<PackageContent> DownloadPackageAsync(
        string id,
        NuGetVersion version,
        CancellationToken token)
    {
        var find = await _repo.GetResourceAsync<FindPackageByIdResource>(token);

        using var cache = new SourceCacheContext();
        using var stream = new MemoryStream();

        bool ok = await find.CopyNupkgToStreamAsync(
            id, version, stream, cache, _logger, token);

        if (!ok)
            throw new InvalidOperationException($"Could not download {id} {version}");

        stream.Position = 0;
        using var reader = new PackageArchiveReader(stream);

        var dlls = reader.GetFiles()
            .Where(f => f.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            .ToList();

        // TODO: Use ExtractFile to load the files
        //        var readme = await reader.GetReadmeAsync(token);
        //        var icon = await reader.GetIconAsync(token);

        string readme = null;
        byte[] icon = null;


        return new PackageContent(
            id,
            version,
            dlls,
            readme,
            icon
        );
    }
}
