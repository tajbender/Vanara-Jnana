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
// TODO:
//    public static void Navigate(Shell32.IShellFolder shellFolder)
//    {
//        TryNavigate(shellFolder);
//    }
    public static bool TryNavigate(object target, bool allowPageCreation = true)
    {
        try
        {
            var pageType = target switch
            {
                "Assemblies" => typeof(AssembliesPage),
                "GitHub" => typeof(GitHubPage),
                "NuGets" => typeof(NuGetsPage),
                "Samples" => typeof(SamplesPage),
                "Settings" => typeof(SettingsPage),
                "Start" => typeof(StartPage),
                "Utilities" => typeof(UtilitiesPage),
                _ => null
            };

            // Check if the page type is null and if page creation is allowed
            if (pageType == null && !allowPageCreation)
            {
                //LogWriter.PrintLine("Page type is null and page creation is not allowed.");
                return false;
            }

            // TODO: _frame.Navigate(pageType ?? new UtilitiesPage());
            return _frame.Navigate(pageType);
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
