using System;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Workbench.Pages.SysInfo;

public sealed partial class SysInfoPage : Page
{
    public SysInfoViewModel ViewModel { get; } = new();

    public SysInfoPage()
    {
        this.InitializeComponent();
        this.DataContext = this.ViewModel;
        this.LoadData();
    }

    private void LoadData()
    {
        this.ViewModel.CPU = "TODO: bind CPU Info.";
        this.ViewModel.GPU = "TODO: bind GPU Info.";
        this.ViewModel.HandleCount = 0; // TODO: bind to an PerformanceCounter
        this.ViewModel.MachineName = Environment.MachineName;
        this.ViewModel.OSVersion = Environment.OSVersion.VersionString;
        this.ViewModel.EnvironmentPathVariable = Environment.GetEnvironmentVariable("PATH") ?? "";
        this.ViewModel.ProcessName = Environment.ProcessPath ?? "unknown";
        this.ViewModel.ProcessId = Environment.ProcessId;
        this.ViewModel.RAM = "TODO: bind RAM Info.";
        this.ViewModel.ThreadCount = Environment.ProcessorCount;
        this.ViewModel.Uptime = $"{Environment.TickCount64 / 1000 / 60} min";
        this.ViewModel.User = Environment.UserName;
        // TODO: This ist the Assembly version of the App, not the WinAppSdk version. Consider using Microsoft.WindowsAppSDK.Release instead.
        this.ViewModel.WinAppSdkVersion = typeof(App).Assembly.GetName().Version?.ToString() ?? "unknown";
        //ViewModel.WinAppSdkVersion = Microsoft.WindowsAppSDK.Version;
        // TODO: The following line is commented out because it may not provide the correct WinAppSdk version. Consider using Microsoft.WindowsAppSDK.Release instead.
        //public static Microsoft.WindowsAppSDK.Release Release => GetWinAppSdkVersion();
        //public static Microsoft.WindowsAppSDK.Release GetWinAppSdkVersion() => Microsoft.WindowsAppSDK.Release;


        // Calculated values: Split PATH environment variable
        this.ViewModel.PathCollection = [];
        var pathEnvironment = Environment.GetEnvironmentVariable("PATH")?.Split(';');
        if ((pathEnvironment is not null) && (pathEnvironment.Length > 0))
        {
            foreach (var pathItem in pathEnvironment)
                this.ViewModel.PathCollection.Add(pathItem);
        }
    }
}
