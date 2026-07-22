using System.ComponentModel;
using System.Text.Json;
using Vanara_Jnana.exe.Models.Contracts;

namespace Jnana.Services;

public class WindowSettings : INotifyPropertyChanged
{
    private double _width;
    private double _height;
    private double _x;
    private double _y;

    public double Width
    {
        get => _width;
        set { _width = value; OnPropertyChanged(nameof(Width)); }
    }

    public double Height
    {
        get => _height;
        set { _height = value; OnPropertyChanged(nameof(Height)); }
    }

    public double X
    {
        get => _x;
        set { _x = value; OnPropertyChanged(nameof(X)); }
    }

    public double Y
    {
        get => _y;
        set { _y = value; OnPropertyChanged(nameof(Y)); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}


public class SettingsService : ISettingsSerializer
{
    private readonly WindowSettings _windowSettings;

    public SettingsService()
    {
        _windowSettings = new WindowSettings();
        SettingsObjects = new object[] { _windowSettings };
    }

    public bool IsDirty { get; private set; }

    public object[] SettingsObjects { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public void Load()
    {
        // JSON aus AppData lesen
        var path = GetSettingsPath();
        if (!File.Exists(path))
            return;

        var json = File.ReadAllText(path);
        var dto = JsonSerializer.Deserialize<WindowSettings>(json);

        _windowSettings.Width = dto.Width;
        _windowSettings.Height = dto.Height;
        _windowSettings.X = dto.X;
        _windowSettings.Y = dto.Y;

        IsDirty = false;
        OnPropertyChanged(nameof(IsDirty));
    }

    public void Save()
    {
        var path = GetSettingsPath();
        var json = JsonSerializer.Serialize(_windowSettings);
        File.WriteAllText(path, json);

        IsDirty = false;
        OnPropertyChanged(nameof(IsDirty));
    }

    private string GetSettingsPath()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Vanara_Jnana"
        );
        Directory.CreateDirectory(folder);
        return Path.Combine(folder, "settings.json");
    }
}
