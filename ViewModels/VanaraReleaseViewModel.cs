using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Jnana.Core.Services;

namespace Jnana.ViewModels;

public class ReleaseInfo
{
    public string Name { get; set; }
    public string Body { get; set; }
    public DateTime PublishedAt { get; set; }
}

public partial class VanaraReleaseViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ReleaseInfo> Releases { get; } = [];
    public event PropertyChangedEventHandler PropertyChanged;

    public event EventHandler LoadFailed;

    public VanaraReleaseViewModel()
    {
        LoadFailed += (s, e) =>
        {
            /* Handle load failure */
        };
        PropertyChanged += static (s, e) => { };
        _ = this.LoadAsync();
    }

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public async Task LoadAsync()
    {
        try
        {
            List<ReleaseInfo> items = await GitHubApi.GetLatestReleasesAsync();
            this.Releases.Clear();
            foreach (ReleaseInfo r in items)
                this.Releases.Add(r);

            this.OnPropertyChanged(nameof(this.Releases));
        }
        catch
        {
            LoadFailed?.Invoke(this, EventArgs.Empty);
        }
    }
}
