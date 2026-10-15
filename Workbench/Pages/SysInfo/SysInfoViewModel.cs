using Microsoft.UI.Xaml;
using Microsoft.WindowsAppSDK;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Version = Microsoft.WindowsAppSDK.Runtime.Version;

namespace Jnana.Workbench.Pages.SysInfo;

public partial class SysInfoViewModel : INotifyPropertyChanged
{
    private readonly CpuInfoProvider _cpu = new();
    private readonly GpuInfoProvider _gpu = new();
    private readonly RamInfoProvider _ram = new();
    private readonly DispatcherTimer _timer;

    // Environment


    // GetEnvironmentVariable
    public string EnvironmentPathVariable = "";
    public ObservableCollection<string> PathCollection;
    private double _cpuUsage;
    private string _gpuName;
    private double _gpuVram;
    private double _ramTotal;
    private double _ramUsed;

    public SysInfoViewModel()
    {
        this.OSVersion = Environment.OSVersion.ToString();
        this.MachineName = Environment.MachineName;
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

        this._timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        this._timer.Tick += this.Update;
        this._timer.Start();
    }

    public double CpuUsage
    {
        get => this._cpuUsage;
        private set
        {
            this._cpuUsage = value;
            this.OnPropertyChanged();
        }
    }

    public double RamUsed
    {
        get => this._ramUsed;
        private set
        {
            this._ramUsed = value;
            this.OnPropertyChanged();
        }
    }

    public double RamTotal
    {
        get => this._ramTotal;
        private set
        {
            this._ramTotal = value;
            this.OnPropertyChanged();
        }
    }

    public string GpuName
    {
        get => this._gpuName;
        private set
        {
            this._gpuName = value;
            this.OnPropertyChanged();
        }
    }

    public double GpuVram
    {
        get => this._gpuVram;
        private set
        {
            this._gpuVram = value;
            this.OnPropertyChanged();
        }
    }

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
    public string User { get; set; } = "";

    // Diagnostics
    public string Uptime { get; set; } = "";
    public int ProcessId { get; set; }
    public string ProcessName { get; set; } = "";

    public string SdkVersion => $"{Release.Major}.{Release.Minor}.{Release.Patch}";
    public string SdkChannel => Release.Channel;

    public string RuntimeVersion => Version.DotQuadString;
    //    public string RuntimePublisher => Microsoft.WindowsAppSDK.Identity.Publisher;
    //    public string FrameworkPackage => Microsoft.WindowsAppSDK.Packages.Framework.PackageFamilyName;
    //    public string MainPackage => Microsoft.WindowsAppSDK.Packages.Main.PackageFamilyName;
    //    public string DdlmX64 => Microsoft.WindowsAppSDK.Packages.DDLM.X64.PackageFamilyName;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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