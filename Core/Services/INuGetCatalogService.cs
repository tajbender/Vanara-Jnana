using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jnana.Core.Services;

public interface INuGetCatalogService
{
    Task<IReadOnlyList<NuGetPackageInfo>> SearchPackagesAsync(string query);
    Task<NuGetPackageInfo?> GetPackageMetadataAsync(string packageId);
    Task<Stream?> DownloadPackageAsync(string packageId, string version);
    Task<string?> GetReadmeMarkdownAsync(string packageId, string version);
}
