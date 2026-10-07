using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Jnana.Workbench.ViewModels;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class TelemetryViewModel()
{
    private float cpu;
    private float gpu;
    private float ram;

    public float Cpu { get => this.cpu; set => this.cpu = value; }
    public float Gpu { get => this.gpu; set => this.gpu = value; }
    public float Ram { get => this.ram; set => this.ram = value; }

    /// <summary>
    /// Gets or sets the property changed event handler.
    /// </summary>
    public Action<object, object> PropertyChanged { get; internal set; }

    /// <summary>
    /// Sets the property value and raises the PropertyChanged event if the value has changed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <param name="propertyName"></param>
    private void SetProperty<T>(ref T field, T value, string propertyName)
    {
        if (!Equals(field, value))
        {
            field = value;
            PropertyChanged?.Invoke(this, new { PropertyName = propertyName });
        }
    }

    private string GetDebuggerDisplay()
    {
        try
        {
            StringBuilder sb = new();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                sb.AppendLine($"{prop.Name}: {prop.GetValue(this)}");
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving debugger display: {ex.Message}");
            return $"{this.GetType().Name}  Error retrieving debugger display: {ex.Message}";
        }
    }
}
