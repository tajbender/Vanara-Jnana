using Microsoft.UI.Xaml.Controls;

namespace Jnana.Core.Navigation;

public sealed partial class NavigationHost : UserControl
{
    public NavigationHost()
    {
        InitializeComponent();
    }

    public void ShowPage(object pageInstance)
    {
        // TODO: this.Presenter.Content = pageInstance;
    }
    // todo: public object Presenter { get; set; }
    // todo: public object SideBarPresenter { get; set; }
}