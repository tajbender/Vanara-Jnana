namespace Jnana.Workbench.Pages.NuGets;

public class NuGetPackageModel
{
    private string _title = "";
    private string _version = "";
    private string _description = "";

    public string Title
    {
        get => _title;
        set => _title = value;
    }

    public string Version
    {
        get => _version;
        set => _version = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }
}