using System.ComponentModel;

namespace Vanara_Jnana.exe.Models.Contracts;

public interface ISettingsSerializer : INotifyPropertyChanged
{
    [ReadOnly(true)]
    public bool IsDirty { get; }

    [ReadOnly(true)]
    public object[] SettingsObjects { get; }
}
