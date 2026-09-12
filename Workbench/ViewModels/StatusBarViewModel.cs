using CommunityToolkit.Mvvm.ComponentModel;

namespace Jnana.Workbench.ViewModels;

public partial class StatusBarViewModel : ObservableObject
{
    private readonly TelemetryViewModel _telemetryViewModel;
    private string _cpu;
    private string _disk;
    private string _net;
    private string _ram;

    public string NetUsage { get => _net; set => SetProperty(ref _net, value); }

    public string DiskUsage { get => _disk; set => SetProperty(ref _disk, value); }

    public string CpuUsage { get => _cpu; set => SetProperty(ref _cpu, value); }

    public string RamUsage { get => _ram; set => SetProperty(ref _ram, value); }

    public StatusBarViewModel()
    {
        //TODO: _telemetryViewModel = App.GetService<TelemetryViewModel>(); // Uncomment this line and remove the next line when DI is set up
        _telemetryViewModel = new TelemetryViewModel(); // Temporary for testing, replace with DI in production

        // Bindings
        CpuUsage = $"{CpuUsage}%";
        RamUsage = $"{RamUsage}%";
        NetUsage = $"{NetUsage}%";
        DiskUsage = $"{DiskUsage}%";

        //telemetryViewModel.PropertyChanged += (s, e) =>
        //{
        //    switch (e.PropertyName)
        //    {
        //        case nameof(telemetryViewModel.CPU):
        //            CPUUsage = $"{telemetryViewModel.CPU}%";
        //            break;
        //        case nameof(telemetryViewModel.RAM):
        //            RAMUsage = $"{telemetryViewModel.RAM}%";
        //            break;
        //        case nameof(telemetryViewModel.Network):
        //            NetUsage = $"{telemetryViewModel.Network}%";
        //            break;
        //        case nameof(telemetryViewModel.Disk):
        //            DiskUsage = $"{telemetryViewModel.Disk}%";
        //            break;
        //        default:
        //            break;
        //    }
        //};
    }
}
