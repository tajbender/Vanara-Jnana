using Microsoft.UI.Xaml.Controls;
using Microsoft.WindowsAppSDK;
using System;

namespace Jnana.Workbench.Pages.SysInfo;

public sealed partial class SysInfoPage : Page
{
    public SysInfoViewModel ViewModel { get; } = new();

    public SysInfoPage()
    {
        InitializeComponent();
        DataContext = ViewModel;
        LoadData();
    }

    private void LoadData()
    {
        ViewModel.CPU = "TODO: bind CPU Info.";
        ViewModel.GPU = "TODO: bind GPU Info.";
        ViewModel.HandleCount = 0; // TODO: bind to an PerformanceCounter
        ViewModel.MachineName = Environment.MachineName;
        ViewModel.OSVersion = Environment.OSVersion.VersionString;
        ViewModel.EnvironmentPathVariable = Environment.GetEnvironmentVariable("PATH") ?? "";
        ViewModel.ProcessName = Environment.ProcessPath ?? "unknown";
        ViewModel.ProcessId = Environment.ProcessId;
        ViewModel.RAM = "TODO: bind RAM Info.";
        ViewModel.ThreadCount = Environment.ProcessorCount;
        ViewModel.Uptime = $"{Environment.TickCount64 / 1000 / 60} min";
        ViewModel.User = Environment.UserName;
        // TODO: This ist the Assembly version of the App, not the WinAppSdk version. Consider using Microsoft.WindowsAppSDK.Release instead.
        ViewModel.WinAppSdkVersion = typeof(App).Assembly.GetName().Version?.ToString() ?? "unknown";
        //ViewModel.WinAppSdkVersion = Microsoft.WindowsAppSDK.Version;
        // TODO: The following line is commented out because it may not provide the correct WinAppSdk version. Consider using Microsoft.WindowsAppSDK.Release instead.
        //public static Microsoft.WindowsAppSDK.Release Release => GetWinAppSdkVersion();
        //public static Microsoft.WindowsAppSDK.Release GetWinAppSdkVersion() => Microsoft.WindowsAppSDK.Release;


        // Calculated values: Split PATH environment variable
        ViewModel.PathCollection = [];
        string[]? pathEnvironment = Environment.GetEnvironmentVariable("PATH")?.Split(';');
        if ((pathEnvironment is not null) && (pathEnvironment.Length > 0))
        {
            foreach (string pathItem in pathEnvironment)
                ViewModel.PathCollection.Add(pathItem);
        }
    }
}
