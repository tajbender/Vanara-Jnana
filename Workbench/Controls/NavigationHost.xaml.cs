using Jnana.Core.Navigation;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Workbench.Controls;

public sealed partial class NavigationHost : UserControl
{
    // todo: public object Presenter { get; set; }
    // todo: public object SideBarPresenter { get; set; }

    public NavigationHost()
    {
        InitializeComponent();

        this.BreadcrumbBar.ItemsSource = new string[] { "Vanara Jnana", "Workbench" };
    }

    public static void ShowPage<TPage>(TPage pageInstance) where TPage : class, INavigationAware
    {
        // TODO: this.Presenter.Content = pageInstance;
    }
}
