using CommunityToolkit.Mvvm.ComponentModel;
using Jnana.Models;
using Jnana.Views;
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
//public void Navigate(Area area) { CurrentArea = area; }
//    // TODO:
//    //    public static void Navigate(Shell32.IShellFolder shellFolder)
//    //    {
//    //        TryNavigate(shellFolder);
//    //    }
//    public bool TryNavigate(object target, bool allowPageCreation = true)
//    {
//        try
//        {
//            var pageType = target switch
//            {
//                "Assemblies" => typeof(AssembliesPage),
//                "GitHub" => typeof(GitHubPage),
//                "NuGets" => typeof(NuGetsPage),
//                "Samples" => typeof(SamplesPage),
//                "Settings" => typeof(SettingsPage),
//                "Start" => typeof(StartPage),
//                "Utilities" => typeof(UtilitiesPage),
//                _ => null
//            };
//
//            // Check if the page type is null and if page creation is allowed
//            if (pageType == null && !allowPageCreation)
//            {
//                //LogWriter.PrintLine("Page type is null and page creation is not allowed.");
//                return false;
//            }
//
//            // TODO: _frame.Navigate(pageType ?? new UtilitiesPage());
//            //return _frame.Navigate(pageType);
//            return true;
//        }
//        catch (Exception ex)
//        {
//            Debug.Fail(ex.ToString());
//            throw;
//        }
//    }
