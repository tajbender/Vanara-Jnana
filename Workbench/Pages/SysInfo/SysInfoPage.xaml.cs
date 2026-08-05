using Microsoft.UI.Xaml.Controls;
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
        ViewModel.OSVersion = Environment.OSVersion.VersionString;
        ViewModel.WinAppSdkVersion = typeof(App).Assembly.GetName().Version?.ToString() ?? "unknown";
        ViewModel.MachineName = Environment.MachineName;
        ViewModel.CPU = "TODO: CPU Info";
        ViewModel.RAM = "TODO: RAM Info";
        ViewModel.GPU = "TODO: GPU Info";
        ViewModel.ThreadCount = Environment.ProcessorCount;
        ViewModel.HandleCount = 0; // TODO: bind to an PerformanceCounter
        ViewModel.Path = Environment.GetEnvironmentVariable("PATH") ?? "";
        ViewModel.User = Environment.UserName;
        ViewModel.Uptime = $"{Environment.TickCount64 / 1000 / 60} min";
        ViewModel.ProcessId = Environment.ProcessId;
    }
}
