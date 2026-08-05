namespace Jnana.Core.Services;

public sealed class NuGetPackageInfo
{
    public string Id { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
    public string? Description { get; init; }
    public long Downloads { get; init; }
}
