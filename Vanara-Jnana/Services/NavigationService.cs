using CommunityToolkit.Mvvm.ComponentModel;
using Jnana.Models;
using Jnana.Views;
using Jnana.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using static Jnana.Models.INavigationService;

namespace Jnana.Services;

public partial class NavigationService : ObservableObject, INavigationService
{
    private readonly Frame _frame;
    private readonly Dictionary<Area, Type> _areaPageMap;

    public NavigationService(Frame frame)
    {
        _frame = frame ?? throw new ArgumentNullException(nameof(frame));

        _areaPageMap = new()
        {
            { Area.Disassembler, typeof(DisassemblerPage)  },
            { Area.GitHub, typeof(GitHubPage) },
            { Area.NuGets, typeof(NuGetsPage) },
            { Area.Samples, typeof(SamplesPage) },
            { Area.Settings, typeof(SettingsPage) },
            { Area.Utilities, typeof(UtilitiesPage) },
            { Area.Void, typeof(VoidPage) },
        };
    }

    public void NavigateTo(Area area)
    {
        try
        {
            if (_areaPageMap.TryGetValue(area, out var pageType))
            {
                Debug.Print($"Navigating to `{area}` page.");
                _frame.Navigate(pageType);
            }
            else
            {
                Debug.Print($"Failed to get page for `{area}` from PageMap.");
            }
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.ToString());
            throw;
        }
    }
}
