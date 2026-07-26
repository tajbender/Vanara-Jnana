using Jnana.Services;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;
using Vanara_Jnana.exe.Models.Contracts;
using Vanara_Jnana.exe.Views.Tools;
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

    public WebViewPanelViewModel(string url = "https://www.example.com")
    {
        Url = url;
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

public sealed class WebViewProvider : INavigationProvider
{
    public bool CanHandle(string provider) => provider.Equals("webview", StringComparison.OrdinalIgnoreCase);

    public Task<NavigationNode> ResolveAsync(NamespaceAddress address)
    {
        return Task.FromResult(new NavigationNode
        {
            Title = address.Path,
            Icon = new SymbolIconSource { Symbol = Symbol.Globe },
            PageType = typeof(WebViewPanel),
            ViewModel = new WebViewPanelViewModel(),
            IsPanel = false
        });
    }
}
