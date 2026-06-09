using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jnana.Services;

public interface INuGetCatalogService
{
    IAsyncEnumerable<PackageId> SearchPackagesAsync(string prefix, CancellationToken token);
    Task<PackageVersionInfo?> GetLatestStableVersionAsync(string packageId, CancellationToken token);
    Task<PackageContent> DownloadPackageAsync(string id, NuGetVersion version, CancellationToken token);
}
