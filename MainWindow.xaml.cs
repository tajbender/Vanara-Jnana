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

        var workbench = new WorkbenchPage();
        Host.ShowPage(workbench);
        _navigation.Navigate(typeof(WorkbenchPage));
    }
}
