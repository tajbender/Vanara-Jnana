using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jnana.Services;

public record PackageId(string Id);

public record PackageVersionInfo(
    string Id,
    string Version,
    Uri? IconUrl,
    string? Description,
    string? Summary
);

public record PackageContent(
    string Id,
    NuGetVersion Version,
    IReadOnlyList<string> Assemblies,
    string? Readme,
    Stream? ReadmeStream,
    byte[]? IconBytes,
    Stream? IconBytesStream
);

public interface INuGetCatalogService
{
    IAsyncEnumerable<PackageId> SearchPackagesAsync(string prefix, CancellationToken token);
    Task<PackageVersionInfo?> GetLatestStableVersionAsync(string packageId, CancellationToken token);
    Task<PackageContent> DownloadPackageAsync(string id, NuGetVersion version, CancellationToken token);
}
