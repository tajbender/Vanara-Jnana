using Microsoft.UI.Xaml;
using Jnana.Core.Navigation;
using Jnana.Workbench.Pages.Workbench;

namespace Jnana;

public sealed partial class MainWindow : Window
{
    private readonly NavigationService _navigation;

    /// <summary>
    /// Public properties for the title, subtitle, and back button visibility of the main window.
    /// </summary>
    public string TitleText { get; set; } = "Vanara Jñāna";
    public string SubtitleText { get; set; } = "Workbench";
    public bool IsBackButtonVisible { get; set; } = true;
    public bool IsBackButtonEnabled { get; set; } = false;

    public MainWindow()
    {
        InitializeComponent();

        _navigation = new NavigationService();
        // TODO: Restore Navigation handling when NavigationService is implemented
        //_navigation.OnPageNavigated += (sender, e) => NavigationHost.ShowPage(e.PageInstance);

        var workbench = new WorkbenchPage();
        NavigationHost.ShowPage(workbench);
        _navigation.Navigate(typeof(WorkbenchPage));
    }
}
