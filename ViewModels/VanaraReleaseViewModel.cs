using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Jnana.Core.Services;

namespace Jnana.ViewModels;

public class VanaraReleaseViewModel : INotifyPropertyChanged
{
    private readonly ObservableCollection<ReleaseInfo> _releases = [];

    public ObservableCollection<ReleaseInfo> Releases => _releases;

    public event EventHandler LoadFailed;
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public VanaraReleaseViewModel()
    {
        LoadFailed += (s, e) => { /* Handle load failure */ };
        PropertyChanged += static (s, e) => { };
        _ = this.LoadAsync();
    }


    public async Task LoadAsync()
    {
        try
        {
            var items = await GitHubApi.GetLatestReleasesAsync();
            this.Releases.Clear();
            foreach (var r in items)
                this.Releases.Add(r);

            this.OnPropertyChanged(nameof(this.Releases));
        }
        catch
        {
            LoadFailed?.Invoke(this, EventArgs.Empty);
        }
    }
}

public class ReleaseInfo
{
    private string _name;
    private string _body;
    private DateTime _publishedAt;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string Body
    {
        get => _body;
        set => _body = value;
    }

    public DateTime PublishedAt
    {
        get => _publishedAt;
        set => _publishedAt = value;
    }
}
