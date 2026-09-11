using CommunityToolkit.Mvvm.ComponentModel;

namespace Jnana.Workbench.ViewModels;

public partial class StatusBarViewModel : ObservableObject
{
    private readonly TelemetryViewModel _telemetry = new(); // Temporary for testing, replace with DI in production

    private string _cpu;

    private string _disk;

    private string _net;

    private string _ram;

    //TODO:        _telemetry = App.GetService<TelemetryViewModel>();
    // Bindings
    //CPUUsage = $"{_telemetry.CPU}%";
    //RAMUsage = $"{_telemetry.RAM}%";
    //NetUsage = $"{_telemetry.Network}%";
    //DiskUsage = $"{_telemetry.Disk}%";
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

    public string CPUUsage
    {
        get => _cpu;
        set => SetProperty(ref _cpu, value);
    }

    public string RAMUsage
    {
        get => _ram;
        set => SetProperty(ref _ram, value);
    }

    public string NetUsage
    {
        get => _net;
        set => SetProperty(ref _net, value);
    }

    public string DiskUsage
    {
        get => _disk;
        set => SetProperty(ref _disk, value);
    }
}