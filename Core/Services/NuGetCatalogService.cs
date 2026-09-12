using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace Jnana.Core.Services;

/// <summary>
///     Service for interacting with the NuGet catalog, allowing for searching packages,
///     retrieving metadata, downloading packages, and extracting README files.
/// </summary>
public sealed class NuGetCatalogService : INuGetCatalogService
{
    private readonly SourceCacheContext _cache = new();

    /// <summary>
    ///     The NuGet repository used for interacting with the NuGet catalog.
    /// </summary>
    private readonly SourceRepository _nuGetSourceRepository;

    private NuGetCatalogService()
    {
        var source = new PackageSource("https://api.nuget.org/v3/index.json");
        _nuGetSourceRepository = Repository.Factory.GetCoreV3(source);
    }

    /// <summary>
    ///     Gets the singleton instance of the NuGetCatalogService.
    /// </summary>
    public static NuGetCatalogService Instance { get; } = new();

    /// <summary>
    ///     Searches for NuGet packages based on the provided query string.
    /// </summary>
    /// <param name="query">The query string to search for.</param>
    /// <returns>A list of matching NuGet packages.</returns>
    public async Task<IReadOnlyList<NuGetPackageInfo>> SearchPackagesAsync(string query)
    {
        var search = await _nuGetSourceRepository.GetResourceAsync<PackageSearchResource>();
        Debug.Assert(search != null, nameof(search) + " != null");
        IEnumerable<IPackageSearchMetadata> results = await search.SearchAsync(query, new SearchFilter(true), 0, 50,
            NullLogger.Instance, CancellationToken.None);

        return
        [
            .. results.Select(r => new NuGetPackageInfo
            {
                Id = r.Identity.Id,
                Version = r.Identity.Version.ToString(),
                Description = r.Description,
                Downloads = r.DownloadCount ?? 0
            })
        ];
    }

    public async Task<NuGetPackageInfo?> GetPackageMetadataAsync(string packageId)
    {
        var meta = await _nuGetSourceRepository.GetResourceAsync<PackageMetadataResource>();
        Debug.Assert(meta != null, nameof(meta) + " != null");

        IEnumerable<IPackageSearchMetadata>? results = await meta.GetMetadataAsync(
            packageId,
            true,
            false,
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
        var download = await _nuGetSourceRepository.GetResourceAsync<DownloadResource>();
        Debug.Assert(download != null, nameof(download) + " != null");
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
        await using var pkg = await DownloadPackageAsync(packageId, version);
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