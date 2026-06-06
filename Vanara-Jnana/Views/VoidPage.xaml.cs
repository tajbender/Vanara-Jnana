using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views;

public sealed partial class VoidPage : Page
{
    private VoidPageViewModel ViewModel { get; }
    public VoidPage()
    {
        InitializeComponent();
        // TODO: ViewModel = new VoidPageViewModel();


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
}
