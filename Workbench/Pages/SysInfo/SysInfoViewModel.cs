namespace Jnana.Workbench.Pages.SysInfo;

public class SysInfoViewModel
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
}
