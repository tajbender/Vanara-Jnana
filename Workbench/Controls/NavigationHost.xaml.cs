using Microsoft.UI.Xaml.Controls;

namespace Jnana.Workbench.Controls;

public sealed partial class NavigationHost : UserControl
{
    // todo: public object Presenter { get; set; }
    // todo: public object SideBarPresenter { get; set; }

    public NavigationHost()
    {
        InitializeComponent();

        this.NavigationBreadcrumbBar.ItemsSource = new string[] { "Workbench" };
    }

    public void ShowPage(object pageInstance)
    {
        // TODO: this.Presenter.Content = pageInstance;
    }
}
