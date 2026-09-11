using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Jnana.Core.Services;

public sealed class NuGetCatalogCache(INuGetCatalogService inner) : INuGetCatalogService
{
    private readonly MemoryCache _cache = new(new MemoryCacheOptions
    {
        SizeLimit = 2000
    });

    private readonly TimeSpan _ttl = TimeSpan.FromHours(24);

    public Task<IReadOnlyList<NuGetPackageInfo>> SearchPackagesAsync(string query)
    {
        return Task.FromResult(GetOrAdd($"search:{query}", () => inner.SearchPackagesAsync(query))!);
    }

    public Task<NuGetPackageInfo?> GetPackageMetadataAsync(string packageId)
    {
        return Task.FromResult(GetOrAdd($"meta:{packageId}", () => inner.GetPackageMetadataAsync(packageId)));
    }

    public Task<Stream?> DownloadPackageAsync(string packageId, string version)
    {
        return inner.DownloadPackageAsync(packageId, version);
        // kein Cache für Streams
    }

    public Task<string?> GetReadmeMarkdownAsync(string packageId, string version)
    {
        return Task.FromResult(GetOrAdd($"readme:{packageId}:{version}",
            () => inner.GetReadmeMarkdownAsync(packageId, version)));
    }

    private T? GetOrAdd<T>(string key, Func<Task<T?>> factory)
    {
        if (_cache.TryGetValue(key, out T? value))
            return value;

        var result = factory().Result;
        if (result != null)
            _cache.Set(key, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _ttl,
                Size = 1
            });

        return result;
    }
}