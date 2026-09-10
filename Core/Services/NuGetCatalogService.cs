using NuGet.Common;
using NuGet.Configuration;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jnana.Core.Services;

/// <summary>
///  Service for interacting with the NuGet catalog, allowing for searching packages,
///  retrieving metadata, downloading packages, and extracting README files.
/// </summary>
public sealed class NuGetCatalogService : INuGetCatalogService
{
    /// <summary>
    /// Gets the singleton instance of the NuGetCatalogService.
    /// </summary>
    public static NuGetCatalogService Instance { get; } = new();
    /// <summary>
    /// The NuGet repository used for interacting with the NuGet catalog.
    /// </summary>
    private readonly SourceRepository _nuGetSourceRepository;
    private readonly SourceCacheContext _cache = new();
    private NuGetCatalogService()
    {
        PackageSource source = new PackageSource("https://api.nuget.org/v3/index.json");
        this._nuGetSourceRepository = Repository.Factory.GetCoreV3(source);
    }

    /// <summary>
    ///  Searches for NuGet packages based on the provided query string.
    /// </summary>
    /// <param name="query">The query string to search for.</param>
    /// <returns>A list of matching NuGet packages.</returns>
    public async Task<IReadOnlyList<NuGetPackageInfo>> SearchPackagesAsync(string query)
    {
        PackageSearchResource? search = await this._nuGetSourceRepository.GetResourceAsync<PackageSearchResource>();
        Debug.Assert(search != null, nameof(search) + " != null");
        IEnumerable<IPackageSearchMetadata> results = await search.SearchAsync(query, new SearchFilter(true), 0, 50, NullLogger.Instance, CancellationToken.None);

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
        PackageMetadataResource? meta = await this._nuGetSourceRepository.GetResourceAsync<PackageMetadataResource>();
        Debug.Assert(meta != null, nameof(meta) + " != null");

        IEnumerable<IPackageSearchMetadata>? results = await meta.GetMetadataAsync(
            packageId,
            includePrerelease: true,
            includeUnlisted: false,
            this._cache,
            NullLogger.Instance,
            CancellationToken.None);

        IPackageSearchMetadata? latest = results?
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
        DownloadResource? download = await this._nuGetSourceRepository.GetResourceAsync<DownloadResource>();
        Debug.Assert(download != null, nameof(download) + " != null");
        DownloadResourceResult? result = await download.GetDownloadResourceResultAsync(
            new PackageIdentity(packageId, NuGetVersion.Parse(version)),
            new PackageDownloadContext(new SourceCacheContext()),
            Path.GetTempPath(),
            NullLogger.Instance,
            CancellationToken.None);

        return result?.PackageStream;
    }

    public async Task<string?> GetReadmeMarkdownAsync(string packageId, string version)
    {
        await using Stream? pkg = await this.DownloadPackageAsync(packageId, version);
        if (pkg == null)
            return null;

        using ZipArchive archive = new ZipArchive(pkg, ZipArchiveMode.Read);
        ZipArchiveEntry? entry = archive.Entries.FirstOrDefault(e =>
            e.FullName.EndsWith("readme.md", StringComparison.OrdinalIgnoreCase));

        if (entry == null)
            return null;

        using StreamReader reader = new StreamReader(entry.Open());
        return await reader.ReadToEndAsync();
    }
}
