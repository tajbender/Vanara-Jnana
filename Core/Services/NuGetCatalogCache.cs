using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jnana.Core.Services;

public sealed class NuGetCatalogCache : INuGetCatalogService
{
    private readonly INuGetCatalogService _inner;
    private readonly MemoryCache _cache = new(new MemoryCacheOptions
    {
        SizeLimit = 2000
    });
    private readonly TimeSpan _ttl = TimeSpan.FromHours(24);

    public NuGetCatalogCache(INuGetCatalogService inner)
    {
        this._inner = inner;
    }

    private T? GetOrAdd<T>(string key, Func<Task<T?>> factory)
    {
        if (this._cache.TryGetValue(key, out T? value))
            return value;

        var result = factory().Result;
        if (result != null)
        {
            this._cache.Set(key, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = this._ttl,
                Size = 1
            });
        }

        return result;
    }

    public Task<IReadOnlyList<NuGetPackageInfo>> SearchPackagesAsync(string query)
        => Task.FromResult(this.GetOrAdd($"search:{query}", () => this._inner.SearchPackagesAsync(query))!);

    public Task<NuGetPackageInfo?> GetPackageMetadataAsync(string packageId)
        => Task.FromResult(this.GetOrAdd($"meta:{packageId}", () => this._inner.GetPackageMetadataAsync(packageId)));

    public Task<Stream?> DownloadPackageAsync(string packageId, string version)
        => this._inner.DownloadPackageAsync(packageId, version); // kein Cache für Streams

    public Task<string?> GetReadmeMarkdownAsync(string packageId, string version)
        => Task.FromResult(this.GetOrAdd($"readme:{packageId}:{version}", () => this._inner.GetReadmeMarkdownAsync(packageId, version)));
}
