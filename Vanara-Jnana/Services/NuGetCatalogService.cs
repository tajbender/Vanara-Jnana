using Jnana.Helpers;
using Microsoft.UI.Xaml.Media.Imaging;
using NuGet.Common;
using NuGet.Packaging;
using NuGet.Protocol.Core.Types;
using NuGet.Protocol;
using NuGet.Versioning;
using System.Runtime.CompilerServices;

namespace Jnana.Services;

public sealed class NuGetCatalogService : INuGetCatalogService
{
    private readonly SourceRepository _repo;
    private readonly ILogger? _logger;

    public NuGetCatalogService(string sourceUrl, ILogger? logger = null)
    {
        _repo = Repository.Factory.GetCoreV3(sourceUrl);
        _logger = logger ?? NullLogger.Instance;
    }

    public async IAsyncEnumerable<PackageId> SearchPackagesAsync(
        string prefix,
        [EnumeratorCancellation] CancellationToken token)
    {
        var search = await _repo.GetResourceAsync<PackageSearchResource>(token);
        var filter = new SearchFilter(includePrerelease: true);

        // TODO: @dahall NuGet's search API doesn't support prefix searching, so we may fetch a bunch of results and then would have to filter them ourselves.
        // TODO: See the results, they include `MonitoringUtils` and `LeadManager.Common`. Note these appear in VS NuGet Pane, too.
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
            pkg.Summary,
            Icon: null
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
        var packageFiles = reader.GetFiles().ToList();

        // First, extract all DLLs
        var dlls = packageFiles
            .Where(f => f.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Extract the readme if it exists
        string? readmeText = null;
        var readmePath = packageFiles.FirstOrDefault(f =>
            f.EndsWith("readme.md", StringComparison.OrdinalIgnoreCase));

        if (readmePath != null)
        {
            using var readmeStream = await reader.GetStreamAsync(readmePath, token);
            using var sr = new StreamReader(readmeStream);
            readmeText = await sr.ReadToEndAsync();
        }

        // Extract the icon if it exists
        byte[]? iconBytes = null;
        var iconPath = packageFiles.FirstOrDefault(f =>
            f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
            f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase));

        BitmapImage? Icon = null;
        if (iconPath != null)
        {
            using var iconStream = await reader.GetStreamAsync(iconPath, token);
            using var ms = new MemoryStream();
            await iconStream.CopyToAsync(ms, token);
            iconBytes = ms.ToArray();

            Icon = ImageExtensions.ToBitmapImage(iconBytes);
        }

        return new PackageContent(
            id,
            version,
            dlls,
            readmeText,
            iconBytes,
            Icon
        );
    }
}
