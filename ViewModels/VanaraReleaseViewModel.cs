using Jnana.Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Jnana.ViewModels;

public class VanaraReleaseViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ReleaseInfo> Releases { get; } = new();

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

    public event EventHandler LoadFailed;
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public class ReleaseInfo
{
    public string Name { get; set; }
    public string Body { get; set; }
    public DateTime PublishedAt { get; set; }
}
