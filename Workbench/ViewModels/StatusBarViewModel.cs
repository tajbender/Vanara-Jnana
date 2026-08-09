using CommunityToolkit.Mvvm.ComponentModel;

namespace Jnana.Workbench.ViewModels;

public partial class StatusBarViewModel : ObservableObject
{
    private readonly TelemetryViewModel _telemetry;

    public StatusBarViewModel(TelemetryViewModel telemetry)
    {
        _telemetry = telemetry;
        // _telemetry = App.GetService<TelemetryViewModel>();



        // Bindings
        CPUUsage = $"{_telemetry.CPU}%";
        RAMUsage = $"{_telemetry.RAM}%";
        NetUsage = $"{_telemetry.Network}%";
        DiskUsage = $"{_telemetry.Disk}%";

        //_telemetry.PropertyChanged += (s, e) =>
        //{
        //    switch (e.PropertyName)
        //    {
        //        case nameof(_telemetry.CPU):
        //            CPUUsage = $"{_telemetry.CPU}%";
        //            break;
        //        case nameof(_telemetry.RAM):
        //            RAMUsage = $"{_telemetry.RAM}%";
        //            break;
        //        case nameof(_telemetry.Network):
        //            NetUsage = $"{_telemetry.Network}%";
        //            break;
        //        case nameof(_telemetry.Disk):
        //            DiskUsage = $"{_telemetry.Disk}%";
        //            break;
        //        default:
        //            break;
        //    }
        //};
    }

    private string _cpu;
    public string CPUUsage { get => _cpu; set => SetProperty(ref _cpu, value); }

    private string _ram;
    public string RAMUsage { get => _ram; set => SetProperty(ref _ram, value); }

    private string _net;
    public string NetUsage { get => _net; set => SetProperty(ref _net, value); }

    private string _disk;
    public string DiskUsage { get => _disk; set => SetProperty(ref _disk, value); }
}
