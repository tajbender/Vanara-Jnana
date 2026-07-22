using System.ComponentModel;

namespace Vanara_Jnana.exe.ViewModels.Tools;

public class WebViewPanelViewModel : INotifyPropertyChanged
{
    private string _url;

    public string Url
    {
        get => _url;
        set { _url = value; OnPropertyChanged(nameof(Url)); }
    }

    public void Navigate(string url)
    {
        Url = url;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
