namespace Jnana.ViewModels;

internal class SettingsAreaViewModel
{
    public string MachineName { get; set; } = "{ Unknown }";
    public string WinAppSdkVersion { get; set; } = "Unknown";
    public string WindowsSdkVersion { get; set; } = "Unknown";
    public string WorkbenchVersion { get; set; } = "Unknown";
    public SettingsAreaViewModel()
    {
    }
}
