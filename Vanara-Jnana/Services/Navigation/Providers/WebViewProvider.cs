using Jnana.Services;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanara_Jnana.exe.Models.Contracts;
using Vanara_Jnana.exe.Views.Tools;
using Vanara_Jnana.exe.ViewModels.Tools;
namespace Vanara_Jnana.exe.Services.Navigation.Providers;

public sealed class WebViewProvider : INavigationProvider
{
    public bool CanHandle(string provider) => provider.Equals("webview", StringComparison.OrdinalIgnoreCase);

    public Task<NavigationNode> ResolveAsync(NamespaceAddress address)
    {
        return Task.FromResult(new NavigationNode
        {
            Title = address.Path,
            Icon = new SymbolIconSource { Symbol = Symbol.World },
            PageType = typeof(WebViewPanel),
            ViewModel = new WebViewPanelViewModel(address.Path),
            IsPanel = false
        });
    }
}
