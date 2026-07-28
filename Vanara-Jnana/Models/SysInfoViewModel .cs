using Microsoft.UI.Xaml;
using System.ComponentModel;
using System.Diagnostics;
using static Vanara.PInvoke.Kernel32;

namespace Vanara.Jnana.ViewModels
{
    public class SysInfoViewModel : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _netCounter;

        public double CpuUsage { get; private set; }
        public double DiskUsage { get; private set; }
        /// <summary>Gets the network usage in KB/s.</summary>
        public double NetworkUsage { get; private set; }
        /// <summary>Gets the RAM usage as a percentage of total physical memory.</summary>
        public double RamUsage { get; private set; }

        public SysInfoViewModel()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _netCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", GetPrimaryNetworkInterface());

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (_, _) => UpdateMetrics();
            _timer.Start();
        }

        private void UpdateMetrics()
        {
            CpuUsage = Math.Round(_cpuCounter.NextValue(), 1);
            RamUsage = Math.Round(GetRamUsage(), 1);
            DiskUsage = Math.Round(GetDiskUsage(), 1);
            NetworkUsage = Math.Round(_netCounter.NextValue() / 1024, 1);

            OnPropertyChanged(nameof(CpuUsage));
            OnPropertyChanged(nameof(RamUsage));
            OnPropertyChanged(nameof(DiskUsage));
            OnPropertyChanged(nameof(NetworkUsage));
        }

        private static double GetRamUsage()
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

        private static double GetDiskUsage()
        {
            var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.IsReady);
            return drive != null ? 100.0 * (1 - (double)drive.AvailableFreeSpace / drive.TotalSize) : 0;
        }

        public static string GetPrimaryNetworkInterface()
        {
            var category = new PerformanceCounterCategory("Network Interface");

            return category.GetInstanceNames()
                           .FirstOrDefault(name => !name.Contains("Loopback") && !name.Contains("isatap")) ?? "Ethernet";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
