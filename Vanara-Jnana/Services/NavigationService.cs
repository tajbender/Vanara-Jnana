using CommunityToolkit.Mvvm.ComponentModel;
using Jnana.Views;
using Jnana.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using static Vanara_Jnana.exe.Models.Contracts.INavigationService;

namespace Jnana.Services;

//public static class AreaExtensions
//{
//    public static Type GetPageType(this NavigationArea area) =>
//        area switch
//        {
//            NavigationArea.Settings => typeof(SettingsPage),
//            NavigationArea.Void => typeof(VoidPage),
//            //NavigationArea.SysInfo => typeof(SysInfoPage),
//            NavigationArea.Utilities => typeof(UtilitiesPage),
//            //NavigationArea.Shell => typeof(ShellPage),
//            _ => throw new NotImplementedException()
//        };
//}


public partial class NavigationService : ObservableObject /* TODO: INavigationService */
{

    private readonly Frame _frame;
    private readonly Dictionary<NavigationArea, Type> _areaPageMap = new()
    { 
            //{ NavigationArea.Disassembler, typeof(DisassemblerPage)  },
            //{ NavigationArea.GitHub, typeof(GitHubPage) },
            //{ NavigationArea.NuGets, typeof(NuGetsPage) },
            //{ NavigationArea.Samples, typeof(SamplesPage) },
            { NavigationArea.Settings, typeof(SettingsPage) },
            { NavigationArea.Utilities, typeof(UtilitiesPage) },
            { NavigationArea.Void, typeof(VoidPage) },
    };

    public NavigationService(Frame frame)
    {
        _frame = frame ?? throw new ArgumentNullException(nameof(frame));
    }

    public void NavigateTo(NavigationArea area)
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

public sealed class NavigationStack
{
    private readonly Stack<NavigationState> _back = new();
    private readonly Stack<NavigationState> _forward = new();

    public NavigationState Current { get; private set; }

    public void Navigate(NavigationState state) { }
    public bool CanGoBack => _back.Count > 0;
    public bool CanGoForward => _forward.Count > 0;
    public NavigationState GoBack() { return _back.Pop(); }
    public NavigationState GoForward() { _back.Push(Current); return Current; }
}

//    public void Navigate(NavigationArea area)
//    {
//        if (_frame.Content is not IAreaPage current || current.NavigationArea != area)
//        {
////            if (_frame.Content is IAreaPage curr)
////                _historyBack.Push(curr.NavigationArea);
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
//            _historyForward.Push(curr.NavigationArea);
//
//        _frame.Navigate(prev.GetPageType());
//        AreaChanged?.Invoke(this, prev);

//    public void GoForward()
//    {
//        if (!CanGoForward) return;
//
//        var next = _historyForward.Pop();
//        if (_frame.Content is IAreaPage curr)
//            _historyBack.Push(curr.NavigationArea);
//
//        _frame.Navigate(next.GetPageType());
//        AreaChanged?.Invoke(this, next); }
