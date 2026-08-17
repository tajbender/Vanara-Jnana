using Microsoft.UI.Xaml;
using Microsoft.WindowsAppSDK;
using Microsoft.WindowsAppSDK.Runtime;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static Vanara.PInvoke.AdvApi32.INSTALLSPEC;

namespace Jnana.Workbench.Pages.SysInfo;

public partial class SysInfoViewModel : INotifyPropertyChanged
{
    private readonly DispatcherTimer _timer;
    private readonly CpuInfoProvider _cpu = new();
    private readonly GpuInfoProvider _gpu = new();
    private readonly RamInfoProvider _ram = new();
    private double _cpuUsage;
    private double _gpuVram;
    private double _ramTotal;
    private double _ramUsed;
    private string _gpuName;

    public double CpuUsage
    {
        get => _cpuUsage;
        private set { _cpuUsage = value; OnPropertyChanged(nameof(CpuUsage)); }
    }

    public double RamUsed
    {
        get => _ramUsed;
        private set { _ramUsed = value; OnPropertyChanged(nameof(RamUsed)); }
    }

    public double RamTotal
    {
        get => _ramTotal;
        private set { _ramTotal = value; OnPropertyChanged(nameof(RamTotal)); }
    }

    public string GpuName
    {
        get => _gpuName;
        private set { _gpuName = value; OnPropertyChanged(nameof(GpuName)); }
    }

    public double GpuVram
    {
        get => _gpuVram;
        private set { _gpuVram = value; OnPropertyChanged(nameof(GpuVram)); }
    }

    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

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


    // GetEnvironmentVariable
    public string EnvironmentPathVariable = "";
    public ObservableCollection<string> PathCollection;
    public string User { get; set; } = "";

    // Diagnostics
    public string Uptime { get; set; } = "";
    public int ProcessId { get; set; }
    public string ProcessName { get; set; } = "";

    public string SdkVersion => $"{Release.Major}.{Release.Minor}.{Release.Patch}";
    public string SdkChannel => Release.Channel;
    public string RuntimeVersion => Microsoft.WindowsAppSDK.Runtime.Version.DotQuadString;
    //    public string RuntimePublisher => Microsoft.WindowsAppSDK.Identity.Publisher;
    //    public string FrameworkPackage => Microsoft.WindowsAppSDK.Packages.Framework.PackageFamilyName;
    //    public string MainPackage => Microsoft.WindowsAppSDK.Packages.Main.PackageFamilyName;
    //    public string DdlmX64 => Microsoft.WindowsAppSDK.Packages.DDLM.X64.PackageFamilyName;

    public event PropertyChangedEventHandler? PropertyChanged;

    public SysInfoViewModel()
    {
        OSVersion = Environment.OSVersion.ToString();
        MachineName = Environment.MachineName;
        this.WinAppSdkVersion = typeof(App).Assembly.GetName().Version?.ToString() ?? "unknown";

        // WinAppSdkVersion = Microsoft.WindowsAppSDK.Version;
        // 
        // Microsoft.WindowsAppSDK.Runtime
        // Microsoft.WindowsAppSDK

        //public string SdkVersion => $"{Release.Major}.{Release.Minor}.{Release.Patch}";
        //public string SdkChannel => Release.Channel;
        //public string RuntimeVersion => Version.DotQuadString;
        //public string RuntimePublisher => Identity.Publisher;
        //public string FrameworkPackage => Packages.Framework.PackageFamilyName;
        //public string MainPackage => Packages.Main.PackageFamilyName;
        //public string DdlmX64 => Packages.DDLM.X64.PackageFamilyName;

        //        var gpu = _gpu.GetGpuInfo();
        //        GpuName = gpu.name;
        //        GpuVram = gpu.vramGb;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += Update;
        _timer.Start();
    }

    private void Update(object sender, object e)
    {
//        CpuUsage = _cpu.GetCpuUsage();
//
//        var ram = _ram.GetRamInfo();
//        RamTotal = ram.totalGb;
//        RamUsed = ram.usedGb;
    }
}
