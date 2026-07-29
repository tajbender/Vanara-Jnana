using Microsoft.UI.Xaml;
using System.ComponentModel;
using System.Diagnostics;
using static Vanara.PInvoke.Kernel32;

namespace Vanara_Jnana.Models;

public class SysInfoViewModel : INotifyPropertyChanged
{
    private readonly DispatcherTimer _timer;
    private readonly PerformanceCounter _cpuCounter;
    private readonly PerformanceCounter _netCounter;

    /// <summary>Gets the CPU usage as a percentage of total CPU capacity.</summary>
    public float CpuUsagePercent { get; private set; }
    /// <summary>Gets the disk usage as a percentage of total disk space.</summary>
    public float DiskUsagePercent { get; private set; }
    /// <summary>Gets the RAM usage as a percentage of total physical memory.</summary>
    public float MemoryUsagePercent { get; private set; }
    /// <summary>Gets the network usage in KB/s.</summary>
    public float NetworkUsage { get; private set; }
    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    public SysInfoViewModel()
    {
        _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        _netCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", GetPrimaryNetworkInterface());

        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += (_, _) => UpdateMetrics();
        _timer.Start();
    }

    private static float GetMemoryUsage()
    {
        var status = new MEMORYSTATUSEX { dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX)) };
        if (GlobalMemoryStatusEx(ref status))
        {
            return status.dwMemoryLoad;
        }

        // Something went wrong, log the error
        var lastError = Marshal.GetLastWin32Error();
        Debug.Fail($"GlobalMemoryStatusEx failed with error code: {lastError}");

        return 0;
    }

    private static float GetDiskUsage()
    {
        var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.IsReady);
        return (float)(drive != null ? 100.0 * (1 - ((double)drive.AvailableFreeSpace / drive.TotalSize)) : 0);
    }

    public static string GetPrimaryNetworkInterface()
    {
        var category = new PerformanceCounterCategory("Network Interface");

        return category.GetInstanceNames()
                       .FirstOrDefault(name => !name.Contains("Loopback") && !name.Contains("isatap")) ?? "Ethernet";
    }

    private void UpdateMetrics()
    {
        CpuUsagePercent = (float)Math.Round(_cpuCounter.NextValue(), 1);
        DiskUsagePercent = (float)Math.Round(GetDiskUsage(), 1);
        MemoryUsagePercent = (float)Math.Round(GetMemoryUsage(), 1);
        NetworkUsage = (float)Math.Round(_netCounter.NextValue() / 1024, 1);

        OnPropertyChanged(nameof(CpuUsagePercent));
        OnPropertyChanged(nameof(MemoryUsagePercent));
        OnPropertyChanged(nameof(DiskUsagePercent));
        OnPropertyChanged(nameof(NetworkUsage));
    }

    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

}
