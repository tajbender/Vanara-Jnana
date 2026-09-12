using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Jnana.Core.Services;

namespace Jnana.ViewModels;

public class VanaraReleaseViewModel : INotifyPropertyChanged
{
    public VanaraReleaseViewModel()
    {
        LoadFailed += (s, e) =>
        {
            /* Handle load failure */
        };
        PropertyChanged += static (s, e) => { };
        _ = LoadAsync();
    }

    public ObservableCollection<ReleaseInfo> Releases { get; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    public event EventHandler LoadFailed;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }


    public async Task LoadAsync()
    {
        try
        {
            var items = await GitHubApi.GetLatestReleasesAsync();
            Releases.Clear();
            foreach (var r in items)
                Releases.Add(r);

            OnPropertyChanged(nameof(Releases));
        }
        catch
        {
            LoadFailed?.Invoke(this, EventArgs.Empty);
        }
    }
}

public class ReleaseInfo
{
    public string Name { get; set; }

    public string Body { get; set; }

    public DateTime PublishedAt { get; set; }
}