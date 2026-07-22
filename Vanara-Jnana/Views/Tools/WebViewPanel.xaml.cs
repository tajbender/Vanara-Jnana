using Microsoft.UI.Xaml.Controls;
using Vanara_Jnana.exe.ViewModels.Tools;

namespace Vanara_Jnana.exe.Views.Tools;

public sealed partial class WebViewPanel : Page
{
    public WebViewPanelViewModel ViewModel { get; } = new();

    public WebViewPanel()
    {
        InitializeComponent();
        DataContext = ViewModel;

        Loaded += async (_, __) =>
        {
            await Browser.EnsureCoreWebView2Async();

            Browser.CoreWebView2.Settings.AreDevToolsEnabled = true;
            Browser.CoreWebView2.Settings.IsZoomControlEnabled = false;
            Browser.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true;
        };
    }

    public void Navigate(string url)
    {
        ViewModel.Navigate(url);
    }
}
