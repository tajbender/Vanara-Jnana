using System.ComponentModel;
using static Vanara.PInvoke.User32;

namespace Vanara_Jnana.exe.ViewModels.Tools;

public class WebViewPanelViewModel : INotifyPropertyChanged
{
    private string _url;
    private string _statusMessage;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string Url
    {
        get => _url;
        set { _url = value; OnPropertyChanged(nameof(Url)); }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(nameof(StatusMessage)); }
    }

    public WebViewPanelViewModel()
    {
        Url = "https://www.example.com";
        StatusMessage = "Ready";
    }

    public void Navigate(string url)
    {
        Url = url;
        StatusMessage = $"Navigating to {url}";
    }

    private void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public void HandleWebViewNavigationCompleted(bool isSuccess, string url)
    {
        if (isSuccess)
        {
            StatusMessage = $"Successfully navigated to {url}";
        }
        else
        {
            StatusMessage = $"Failed to navigate to {url}";
        }
    }
}
