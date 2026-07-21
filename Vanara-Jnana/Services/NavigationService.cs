using CommunityToolkit.Mvvm.ComponentModel;
using Jnana.Views;
using Jnana.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using static Vanara_Jnana.exe.Models.Contracts.INavigationService;

namespace Jnana.Services;


public static class AreaExtensions
{
    public static Type GetPageType(this Area area) =>
        area switch
        {
            Area.Settings => typeof(SettingsPage),
            Area.Void => typeof(VoidPage),
            //Area.SysInfo => typeof(SysInfoPage),
            Area.Utilities => typeof(UtilitiesPage),
            //Area.Shell => typeof(ShellPage),
            _ => throw new NotImplementedException()
        };
}


public partial class NavigationService : ObservableObject /* TODO: INavigationService */
{

    public enum Area
    {
        Settings,
        Void,
        SysInfo,
        Utilities,
        Shell,
        Workbench,
        Disassembler,
    }

    public interface IAreaPage
    {
        Area Area { get; }
    }




    private readonly Frame _frame;
    private readonly Dictionary<Area, Type> _areaPageMap;

    public NavigationService(Frame frame)
    {
        _frame = frame ?? throw new ArgumentNullException(nameof(frame));

        _areaPageMap = new()
        {
            //{ Area.Disassembler, typeof(DisassemblerPage)  },
            //{ Area.GitHub, typeof(GitHubPage) },
            //{ Area.NuGets, typeof(NuGetsPage) },
            //{ Area.Samples, typeof(SamplesPage) },
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

//    public void Navigate(Area area)
//    {
//        if (_frame.Content is not IAreaPage current || current.Area != area)
//        {
////            if (_frame.Content is IAreaPage curr)
////                _historyBack.Push(curr.Area);
////
////            _historyForward.Clear();
////
////            _frame.Navigate(area.GetPageType());
////            AreaChanged?.Invoke(this, area);
//        }
//    }

//    public bool CanGoBack => _historyBack.Count > 0;
//    public bool CanGoForward => _historyForward.Count > 0;

//    public void GoBack()
//    {
//        if (!CanGoBack) return;
//
//        var prev = _historyBack.Pop();
//        if (_frame.Content is IAreaPage curr)
//            _historyForward.Push(curr.Area);
//
//        _frame.Navigate(prev.GetPageType());
//        AreaChanged?.Invoke(this, prev);

//    public void GoForward()
//    {
//        if (!CanGoForward) return;
//
//        var next = _historyForward.Pop();
//        if (_frame.Content is IAreaPage curr)
//            _historyBack.Push(curr.Area);
//
//        _frame.Navigate(next.GetPageType());
//        AreaChanged?.Invoke(this, next);
}
