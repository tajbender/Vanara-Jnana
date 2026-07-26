using Jnana.Services;
using Jnana.ViewModels;
using Jnana.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using Vanara_Jnana.exe.Models.Contracts;

namespace Vanara_Jnana.exe.Services.Navigation.Providers;

public sealed class SettingsProvider : INavigationProvider
{
    public bool CanHandle(string provider) => provider.Equals("settings", StringComparison.OrdinalIgnoreCase);

    public Task<NavigationNode> ResolveAsync(NamespaceAddress address)
    {
        return Task.FromResult(new NavigationNode
        {
            Title = "Settings",
            Icon = new SymbolIconSource { Symbol = Symbol.Globe },
            PageType = typeof(SettingsPage),
            ViewModel = new SettingsAreaViewModel(),
            IsPanel = false
        });
    }
}
