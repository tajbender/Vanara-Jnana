using System.Diagnostics;
using ClassicSamplesBrowser.Views;
using Microsoft.UI.Xaml.Controls;
using Vanara.PInvoke;

namespace ClassicSamplesBrowser.Services;

public static class NavigationService
{
    private static Frame _frame;

    public static void Initialize(Frame frame)
    {
        _frame = frame;
    }
    public static void Navigate(Shell32.IShellFolder shellFolder)
    {
        TryNavigate(shellFolder);
    }
    public static void NavigateToStart()
    {
        TryNavigate("Start");
    }
    public static void NavigateHome()
    {
        TryNavigate("Home");
    }
    public static void TryNavigate(object target)
    {
        try
        {
            var pageType = target switch
            {
                "Start" => typeof(StartPage),
                "API" => typeof(ApiExplorerPage),
                "Samples" => typeof(SamplesPage),
                _ => null
            };

            _frame.Navigate(pageType ?? null);
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.ToString());
            throw;
        }
    }
    public static void NavigateBack() { }
    public static void Forward() { }
}
