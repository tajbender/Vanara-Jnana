using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
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
            try
            {
                await Browser.EnsureCoreWebView2Async();

                if (Browser.CoreWebView2 != null)
                {
                    Browser.CoreWebView2.Settings.AreDevToolsEnabled = true;
                    Browser.CoreWebView2.Settings.IsZoomControlEnabled = false;
                    Browser.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

                    Debug.WriteLine("WebView2 Runtime is available. Initializer completed.");
                }
                else
                {
                    Debug.WriteLine("WebView2 Runtime is not available. CoreWebView2 is null.");
                    throw new InvalidOperationException("WebView2 Runtime is not available. Please install it to use this feature.");
                }
            }
            catch (COMException ex)
            {
                Debug.WriteLine("WebView2 Runtime is missing: " + ex.Message);
                throw new InvalidOperationException("WebView2 Runtime is missing. Please install it to use this feature.", ex);
                // TODO: Optional: Fallback anzeigen oder eine Nachricht an den Benutzer, dass WebView2 nicht verfügbar ist.
                // MessageBox.Show("WebView2 Runtime is missing. Please install it to use this feature.", "WebView2 Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        };
    }

    public void Navigate(string url)
    {
        ViewModel.Navigate(url);
    }
}
