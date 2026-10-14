using CommunityToolkit.Mvvm.ComponentModel;

namespace Jnana.Workbench.ViewModels;

public partial class StatusBarViewModel : ObservableObject
{
    private readonly TelemetryViewModel _telemetry;

    private string _cpu;

    private string _disk;

    private string _net;

    private string _ram;

    public StatusBarViewModel()
    {
        //TODO:        _telemetry = App.GetService<TelemetryViewModel>();
        this._telemetry = new TelemetryViewModel(); // Temporary for testing, replace with DI in production

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
    }

    public string CPUUsage
    {
        get => this._cpu;
        set => this.SetProperty(ref this._cpu, value);
    }

    public string RAMUsage
    {
        get => this._ram;
        set => this.SetProperty(ref this._ram, value);
    }

    public string NetUsage
    {
        get => this._net;
        set => this.SetProperty(ref this._net, value);
    }

    public string DiskUsage
    {
        get => this._disk;
        set => this.SetProperty(ref this._disk, value);
    }
}