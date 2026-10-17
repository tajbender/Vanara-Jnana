using Jnana.Core.Navigation;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Workbench.Controls;

public sealed partial class NavigationHost : UserControl
{
    // todo: public object Presenter { get; set; }
    // todo: public object SideBarPresenter { get; set; }

    public NavigationHost()
    {
        this.InitializeComponent();

        this.BreadcrumbBar.ItemsSource = new string[] { "Workbench" };
    }

    public void ShowPage<TPage>(TPage pageInstance) where TPage : class, INavigationAware
    {
        this.PageContentPresenter.Content = pageInstance;

        //        this.ShowPage(typeof(TPage), pageInstance);
        // TODO: this.Presenter.Content = pageInstance;
    }
}
