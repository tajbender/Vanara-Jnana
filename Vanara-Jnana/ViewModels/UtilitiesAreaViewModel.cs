namespace Jnana.ViewModels;

public class SysInfoIdentityViewModel
{
    public string MachineName => Environment.MachineName;
    public string OSVersion => Environment.OSVersion.ToString();
    public string WinAppSdkVersion => typeof(Microsoft.UI.Xaml.Application).Assembly.GetName().Version.ToString();
    public string WindowsSdkVersion => "10.0.22621"; // TODO: später dynamisch
    public string WorkbenchVersion => "Vanara Jnana version: 5.0.5"; // TODO: später dynamisch
}


internal class UtilitiesAreaViewModel
{
    private readonly SysInfoIdentityViewModel _sysInfoIdentityViewModel;

    public string MachineName => _sysInfoIdentityViewModel.MachineName;
    public string OSVersion => _sysInfoIdentityViewModel.OSVersion;
    public string WinAppSdkVersion => _sysInfoIdentityViewModel.WinAppSdkVersion;
    public string WindowsSdkVersion => _sysInfoIdentityViewModel.WindowsSdkVersion;
    public string WorkbenchVersion => _sysInfoIdentityViewModel.WorkbenchVersion;

    public UtilitiesAreaViewModel()
    {
        _sysInfoIdentityViewModel = new SysInfoIdentityViewModel();
    }
}
