using Microsoft.UI.Xaml;
using Jnana.Core.Navigation;
using Jnana.Workbench.Pages.Workbench;

namespace Jnana;

public sealed partial class MainWindow : Window
{
    private readonly NavigationService _navigation;

    public MainWindow()
    {
        InitializeComponent();

        _navigation = new NavigationService();

        // Minimal: Workbench direkt anzeigen
        var page = new WorkbenchPage();
        Host.ShowPage(page);

        // Optional: NavigationService merken
        _navigation.Navigate(typeof(WorkbenchPage));
    }
}
