using System;

namespace Jnana.Workbench.ViewModels;

public class TelemetryViewModel
{
    public string CPU { get; set; }
    public string RAM { get; set; }
    public string Network { get; set; }
    public string Disk { get; set; }

    //    private float cpu;
    //    private float gpu;
    //    private float ram;

    public Action<object, object> PropertyChanged { get; internal set; }
}
