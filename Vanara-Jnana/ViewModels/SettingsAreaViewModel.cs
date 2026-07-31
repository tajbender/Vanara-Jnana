using Jnana.Services;
using Jnana.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using Vanara_Jnana.exe.Models.Contracts;

namespace Jnana.ViewModels;

public class SettingsAreaViewModel
{
    public string MachineName { get; set; } = "{ Unknown }";
    public string WinAppSdkVersion { get; set; } = "Unknown";
    public string WindowsSdkVersion { get; set; } = "Unknown";
    public string WorkbenchVersion { get; set; } = "Unknown";
    public SettingsAreaViewModel()
    {
    }
}
