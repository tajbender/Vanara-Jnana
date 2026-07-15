namespace Jnana.ViewModels;

public class SysInfoIdentityViewModel
{
    public string OSVersion => Environment.OSVersion.ToString();
    public string MachineName => Environment.MachineName;
    public string WorkbenchVersion => "5.0.5";

    public string WinAppSdkVersion => typeof(Microsoft.UI.Xaml.Application).Assembly.GetName().Version.ToString();
    public string WindowsSdkVersion => "10.0.22621"; // später dynamisch
}


internal class UtilitiesAreaViewModel
{
    private readonly SysInfoIdentityViewModel _sysInfoIdentityViewModel;

    public UtilitiesAreaViewModel()
    {
        _sysInfoIdentityViewModel = new SysInfoIdentityViewModel();
    }
}
