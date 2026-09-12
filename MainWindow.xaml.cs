using Microsoft.UI.Xaml;
using Jnana.Core.Navigation;
using Jnana.Workbench.Pages.Workbench;

namespace Jnana;

public sealed partial class MainWindow : Window
{
    private readonly NavigationService _PrimaryNavigation;
    private readonly NavigationService _SecondaryNavigation;
    private readonly NavigationService _SidebarNavigation;

    public MainWindow()
    {
        InitializeComponent();

        _PrimaryNavigation = new NavigationService(MainWindowHost);
        _SecondaryNavigation = new NavigationService(SecondaryWindowHost);
        _SidebarNavigation = new NavigationService(SecondaryRightSidebarHost);



        //        _PrimaryNavigation.Navigate(typeof(WorkbenchPage));
        //        //_PrimaryNavigation.ShowPage(workbench);
        //        //MainGridHost.ShowPage(workbench);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Navigate to the initial page when the window is loaded
        _PrimaryNavigation.Navigate(typeof(WorkbenchPage));
    }
}
