//using System.Runtime.CompilerServices;
//using NuGet.Common;
//using NuGet.Packaging;
//using NuGet.Protocol;
//using NuGet.Protocol.Core.Types;
//using NuGet.Versioning;
//namespace Vanara.WinUI.Extensions.Services.NuGet;
//
//public sealed class NuGetCatalogMemoryCache : INuGetCatalogService
//{
//    private readonly INuGetCatalogService _inner;
//    private readonly TimeSpan _ttl = TimeSpan.FromHours(24);
//
//    private readonly Dictionary<string, (DateTime Timestamp, PackageVersionInfo Info)> _cache
//        = new();
//
//    public NuGetCatalogMemoryCache(INuGetCatalogService inner)
//    {
//        _inner = inner;
//    }
//
//    public async IAsyncEnumerable<PackageId> SearchPackagesAsync(
//        string prefix,
//        [EnumeratorCancellation] CancellationToken token)
//    {
//        await foreach (var pkg in _inner.SearchPackagesAsync(prefix, token))
//            yield return pkg;
//    }
//
//    public async Task<PackageVersionInfo?> GetLatestStableVersionAsync(
//        string packageId,
//        CancellationToken token)
//    {
//        if (_cache.TryGetValue(packageId, out var entry))
//        {
//            if (DateTime.UtcNow - entry.Timestamp < _ttl)
//                return entry.Info;
//        }
//
//        var info = await _inner.GetLatestStableVersionAsync(packageId, token);
//        if (info != null)
//            _cache[packageId] = (DateTime.UtcNow, info);
//
//        return info;
//    }
//
//    public Task<PackageContent> DownloadPackageAsync(
//        string id,
//        NuGetVersion version,
//        CancellationToken token)
//        => _inner.DownloadPackageAsync(id, version, token);
//}
