using Jnana.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Jnana.Views;

public sealed partial class VoidPage : Page
{
    private VoidPageViewModel ViewModel { get; }
    public VoidPage()
    {
        InitializeComponent();
        // TODO: ViewModel = new VoidPageViewModel();

        GitHubWebView2.Loaded += GitHubPreview_Loaded;

        //        GitHubPreview.CoreWebView2Initialized += (_, __) =>
        //        {
        //            // TODO: Is this call ever reached?
        //            var wv = GitHubPreview.CoreWebView2;
        //
        //            // Disable context menus, dev tools, zoom, and status bar
        //            wv.Settings.AreDefaultContextMenusEnabled = false;
        //            wv.Settings.AreDevToolsEnabled = false;
        //            wv.Settings.IsZoomControlEnabled = false;
        //            wv.Settings.IsStatusBarEnabled = false;
        //
        //            // Disable built-in error page to prevent it from showing when loading fails
        //            //wv.Settings.IsBuiltInErrorPageEnabled = false;
        //
        //            // Inject CSS to hide Scrollbars and prevent scrolling (optional, can be adjusted as needed)
        //            //wv.AddScriptToExecuteOnDocumentCreatedAsync(@"
        //            //const style = document.createElement('style');
        //            //style.innerHTML = 
        //            //    `::-webkit-scrollbar { display: none; }
        //            //    body { overflow: hidden !important; }
        //            //`; document.head.appendChild(style); ");
        //        };
    }

    private async void GitHubPreview_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            await GitHubWebView2.EnsureCoreWebView2Async();
            GitHubWebView2.Source = new Uri("https://github.com/vanara");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("WebView2 init failed: " + ex);
        }
    }
}
