using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.WindowsAppSDK;
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
    private string _osVersion = "";
    private string _winAppSdkVersion = "";
    private string _machineName = "";
    private string _cpu1 = "";
    private string _ram1 = "";
    private string _gpu1 = "";
    private int _threadCount;
    private int _handleCount;
    private string _user = "";
    private string _uptime = "";
    private int _processId;
    private string _processName = "";

    public SysInfoViewModel()
    {
        OSVersion = Environment.OSVersion.ToString();
        MachineName = Environment.MachineName;
        WinAppSdkVersion = typeof(App).Assembly.GetName().Version?.ToString() ?? "unknown";

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

    public double CpuUsage
    {
        get => _cpuUsage;
        private set
        {
            _cpuUsage = value;
            OnPropertyChanged();
        }
    }

    public double RamUsed
    {
        get => _ramUsed;
        private set
        {
            _ramUsed = value;
            OnPropertyChanged();
        }
    }

    public double RamTotal
    {
        get => _ramTotal;
        private set
        {
            _ramTotal = value;
            OnPropertyChanged();
        }
    }

    public string GpuName
    {
        get => _gpuName;
        private set
        {
            _gpuName = value;
            OnPropertyChanged();
        }
    }

    public double GpuVram
    {
        get => _gpuVram;
        private set
        {
            _gpuVram = value;
            OnPropertyChanged();
        }
    }

    // Identity
    public string OSVersion
    {
        get => _osVersion;
        set => _osVersion = value;
    }

    public string WinAppSdkVersion
    {
        get => _winAppSdkVersion;
        set => _winAppSdkVersion = value;
    }

    public string MachineName
    {
        get => _machineName;
        set => _machineName = value;
    }

    // Hardware
    public string CPU
    {
        get => _cpu1;
        set => _cpu1 = value;
    }

    public string RAM
    {
        get => _ram1;
        set => _ram1 = value;
    }

    public string GPU
    {
        get => _gpu1;
        set => _gpu1 = value;
    }

    // Runtime
    public int ThreadCount
    {
        get => _threadCount;
        set => _threadCount = value;
    }

    public int HandleCount
    {
        get => _handleCount;
        set => _handleCount = value;
    }

    public string User
    {
        get => _user;
        set => _user = value;
    }

    // Diagnostics
    public string Uptime
    {
        get => _uptime;
        set => _uptime = value;
    }

    public int ProcessId
    {
        get => _processId;
        set => _processId = value;
    }

    public string ProcessName
    {
        get => _processName;
        set => _processName = value;
    }

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