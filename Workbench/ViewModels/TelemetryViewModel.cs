using System;

namespace Jnana.Workbench.ViewModels;

public class TelemetryViewModel
{
    private float cpu;
    private float gpu;
    private float ram;
    private Action<object, object> _propertyChanged;

    public Action<object, object> PropertyChanged
    {
        get => _propertyChanged;
        internal set => _propertyChanged = value;
    }
}