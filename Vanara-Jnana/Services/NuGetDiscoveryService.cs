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
    NuGetVersion Version,
    Uri? IconUrl,
    string? Description,
    string? Summary
);

public record PackageContent(
    string Id,
    NuGetVersion Version,
    IReadOnlyList<string> Assemblies,
    string? Readme,
    byte[]? IconBytes
);

internal class NuGetDiscoveryService
{
}
