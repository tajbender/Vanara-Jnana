using System;

namespace Jnana.Workbench.ViewModels;

public class TelemetryViewModel
{
    private float _cpu;
    private float _gpu;
    private float _ram;

    public Action<object, object> PropertyChanged { get; internal set; }
}
