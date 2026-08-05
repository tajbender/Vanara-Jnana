using NuGet.Common;
using NuGet.Configuration;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jnana.Core.Services;

/// <summary>
/// Service for interacting with the NuGet catalog, allowing for searching packages, retrieving metadata, downloading packages, and extracting README files.
/// WARN: These Namespaces have to be accessible to make the `Repository.Factory.GetCoreV3(source)` magic work.
///       Removal may result in undefined behavior: `Repository.Factory` is a static class extendened with various decorators.
/// -      using NuGet.Protocol;
/// -      using NuGet.Protocol.Core.Types;
/// -      using NuGet.Configuration;
/// </summary>
public sealed class NuGetCatalogService : INuGetCatalogService
{
    private readonly SourceRepository _repo;
    private readonly SourceCacheContext _cache = new();


    public NuGetCatalogService()
    {
        var source = new PackageSource("https://api.nuget.org/v3/index.json");
        _repo = Repository.Factory.GetCoreV3(source);
    }

    public async Task<IReadOnlyList<NuGetPackageInfo>> SearchPackagesAsync(string query)
    {
        var search = await _repo.GetResourceAsync<PackageSearchResource>();
        var results = await search.SearchAsync(query, new SearchFilter(true), 0, 50, NullLogger.Instance, CancellationToken.None);

        return results.Select(r => new NuGetPackageInfo
        {
            Id = r.Identity.Id,
            Version = r.Identity.Version.ToString(),
            Description = r.Description,
            Downloads = r.DownloadCount ?? 0
        }).ToList();
    }

    public async Task<NuGetPackageInfo?> GetPackageMetadataAsync(string packageId)
    {
        var meta = await _repo.GetResourceAsync<PackageMetadataResource>();

        var results = await meta.GetMetadataAsync(
            packageId,
            includePrerelease: true,
            includeUnlisted: false,
            _cache,
            NullLogger.Instance,
            CancellationToken.None);

        var latest = results?
            .OrderByDescending(m => m.Identity.Version)
            .FirstOrDefault();

        if (latest == null)
            return null;

        return new NuGetPackageInfo
        {
            Id = latest.Identity.Id,
            Version = latest.Identity.Version.ToString(),
            Description = latest.Description,
            Downloads = latest.DownloadCount ?? 0
        };
    }


    public async Task<Stream?> DownloadPackageAsync(string packageId, string version)
    {
        var download = await _repo.GetResourceAsync<DownloadResource>();
        var result = await download.GetDownloadResourceResultAsync(
            new PackageIdentity(packageId, NuGetVersion.Parse(version)),
            new PackageDownloadContext(new SourceCacheContext()),
            Path.GetTempPath(),
            NullLogger.Instance,
            CancellationToken.None);

        return result?.PackageStream;
    }

    public async Task<string?> GetReadmeMarkdownAsync(string packageId, string version)
    {
        using var pkg = await DownloadPackageAsync(packageId, version);
        if (pkg == null)
            return null;

        using var archive = new ZipArchive(pkg, ZipArchiveMode.Read);
        var entry = archive.Entries.FirstOrDefault(e =>
            e.FullName.EndsWith("readme.md", StringComparison.OrdinalIgnoreCase));

        if (entry == null)
            return null;

        using var reader = new StreamReader(entry.Open());
        return await reader.ReadToEndAsync();
    }
}
