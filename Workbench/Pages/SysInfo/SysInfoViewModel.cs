using Microsoft.WindowsAppSDK;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Jnana.Workbench.Pages.SysInfo;

public class SysInfoViewModel : INotifyPropertyChanged
{
    // Identity
    public string OSVersion { get; set; } = "";
    public string WinAppSdkVersion { get; set; } = "";
    public string MachineName { get; set; } = "";

    // Hardware
    public string CPU { get; set; } = "";
    public string RAM { get; set; } = "";
    public string GPU { get; set; } = "";

    // Runtime
    public int ThreadCount { get; set; }
    public int HandleCount { get; set; }

    // Environment
    public string Path { get; set; } = "";
    public string User { get; set; } = "";

    // Diagnostics
    public string Uptime { get; set; } = "";
    public int ProcessId { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
