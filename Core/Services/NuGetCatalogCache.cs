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
        _inner = inner;
    }

    private T? GetOrAdd<T>(string key, Func<Task<T?>> factory)
    {
        if (_cache.TryGetValue(key, out T? value))
            return value;

        var result = factory().Result;
        if (result != null)
        {
            _cache.Set(key, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _ttl,
                Size = 1
            });
        }

        return result;
    }

    public Task<IReadOnlyList<NuGetPackageInfo>> SearchPackagesAsync(string query)
        => Task.FromResult(GetOrAdd($"search:{query}", () => _inner.SearchPackagesAsync(query))!);

    public Task<NuGetPackageInfo?> GetPackageMetadataAsync(string packageId)
        => Task.FromResult(GetOrAdd($"meta:{packageId}", () => _inner.GetPackageMetadataAsync(packageId)));

    public Task<Stream?> DownloadPackageAsync(string packageId, string version)
        => _inner.DownloadPackageAsync(packageId, version); // kein Cache für Streams

    public Task<string?> GetReadmeMarkdownAsync(string packageId, string version)
        => Task.FromResult(GetOrAdd($"readme:{packageId}:{version}", () => _inner.GetReadmeMarkdownAsync(packageId, version)));
}
