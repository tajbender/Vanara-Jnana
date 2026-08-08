using System;

namespace Jnana.Workbench.ViewModels;

public class TelemetryViewModel
{
    private float cpu;
    private float gpu;
    private float ram;

    public Action<object, object> PropertyChanged { get; internal set; }
}
