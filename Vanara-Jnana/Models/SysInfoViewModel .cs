using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Vanara.Jnana.ViewModels
{
    public class SysInfoViewModel : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;
//        private readonly PerformanceCounter _cpuCounter;
//        private readonly PerformanceCounter _netCounter;

        public double CpuUsage { get; private set; }
        public double RamUsage { get; private set; }
        public double DiskUsage { get; private set; }
        public double NetworkUsage { get; private set; }

        public SysInfoViewModel()
        {
//            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
//            _netCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", GetPrimaryNetworkInterface());

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (_, _) => UpdateMetrics();
            _timer.Start();
        }

        private void UpdateMetrics()
        {
//            CpuUsage = Math.Round(_cpuCounter.NextValue(), 1);
            RamUsage = Math.Round(GetRamUsage(), 1);
            DiskUsage = Math.Round(GetDiskUsage(), 1);
//            NetworkUsage = Math.Round(_netCounter.NextValue() / 1024, 1); // KB/s

            OnPropertyChanged(nameof(CpuUsage));
            OnPropertyChanged(nameof(RamUsage));
            OnPropertyChanged(nameof(DiskUsage));
            OnPropertyChanged(nameof(NetworkUsage));
        }

        private static double GetRamUsage()
        {
            var info = GC.GetGCMemoryInfo();
            var total = info.TotalAvailableMemoryBytes / (1024 * 1024);
            var used = (GC.GetTotalMemory(false)) / (1024 * 1024);
            return used / total * 100;
        }

        private static double GetDiskUsage()
        {
            var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.IsReady);
            return drive != null ? 100.0 * (1 - (double)drive.AvailableFreeSpace / drive.TotalSize) : 0;
        }

        private static string GetPrimaryNetworkInterface()
        {
//            var category = new PerformanceCounterCategory("Network Interface");
//            return category.GetInstanceNames().FirstOrDefault(name => !name.Contains("Loopback"));
            return "Ethernet"; // TODO: Replace with actual logic to get the primary network interface
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
