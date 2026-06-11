using System.ComponentModel;

namespace Jnana.Models;

public interface ISettingsSerializer : INotifyPropertyChanged
{
    [ReadOnly(true)]
    public bool IsDirty { get; }

    [ReadOnly(true)]
    public object[] SettingsObjects { get; }
}
