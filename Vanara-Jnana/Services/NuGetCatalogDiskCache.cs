using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jnana.Services;

public sealed class NuGetCatalogDiskCache : INuGetCatalogService
{
    private readonly INuGetCatalogService _inner;
    private readonly string _cacheFile;
    private readonly TimeSpan _ttl = TimeSpan.FromHours(24);

    private Dictionary<string, PackageVersionInfo>? _cache;
    private DateTime _timestamp;

    public NuGetCatalogDiskCache(INuGetCatalogService inner, string cacheFile)
    {
        _inner = inner;
        _cacheFile = cacheFile;

        _ = LoadFromDisk();
    }

    private async Task LoadFromDisk()
    {
        if (!File.Exists(_cacheFile))
            return;

        try
        {
            var json = File.ReadAllText(_cacheFile);
            var data = JsonSerializer.Deserialize<CacheData>(json);

            if (data != null && DateTime.UtcNow - data.Timestamp < _ttl)
            {
                _timestamp = data.Timestamp;
                _cache = data.Packages.ToDictionary(p => p.Id, p => p);

                Debug.WriteLine("LoadFromDisk(): Cache dated {0} successfully loaded from disk ", _timestamp);
            }
        }
        catch
        {
            // we can ignore any errors here - if the cache is corrupted or unreadable, we'll just start fresh
        }
    }

    private void SaveToDisk()
    {
        var data = new CacheData
        {
            Timestamp = DateTime.UtcNow,
            Packages = _cache?.Values.ToList() ?? new()
        };

        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        Directory.CreateDirectory(Path.GetDirectoryName(_cacheFile)!);
        File.WriteAllText(_cacheFile, json);
    }

    public async IAsyncEnumerable<PackageId> SearchPackagesAsync(
        string prefix,
        [EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var pkg in _inner.SearchPackagesAsync(prefix, token))
            yield return pkg;
    }

    public async Task<PackageVersionInfo?> GetLatestStableVersionAsync(
        string packageId,
        CancellationToken token)
    {
        _cache ??= new();

        if (_cache.TryGetValue(packageId, out var info))
            return info;

        var latest = await _inner.GetLatestStableVersionAsync(packageId, token);
        if (latest != null)
        {
            _cache[packageId] = latest;
            SaveToDisk();
        }

        return latest;
    }

    public Task<PackageContent> DownloadPackageAsync(
        string id,
        NuGetVersion version,
        CancellationToken token)
        => _inner.DownloadPackageAsync(id, version, token);

    private sealed class CacheData
    {
        public DateTime Timestamp { get; set; }
        public List<PackageVersionInfo> Packages { get; set; } = new();
    }
}
