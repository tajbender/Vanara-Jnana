namespace Jnana.Core.Services;

public sealed class NuGetPackageInfo
{
    private readonly string _id = string.Empty;
    private readonly string _version = string.Empty;
    private readonly string? _description;
    private readonly long _downloads;

    public string Id
    {
        get => _id;
        init => _id = value;
    }

    public string Version
    {
        get => _version;
        init => _version = value;
    }

    public string? Description
    {
        get => _description;
        init => _description = value;
    }

    public long Downloads
    {
        get => _downloads;
        init => _downloads = value;
    }
}