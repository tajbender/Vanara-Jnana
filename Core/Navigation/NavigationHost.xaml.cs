using Microsoft.UI.Xaml.Controls;

namespace Jnana.Core.Navigation;

public sealed partial class NavigationHost : UserControl
{
    public NavigationHost()
    {
        this.InitializeComponent();
    }

    public void ShowPage(object pageInstance)
    {
        this.Presenter.Content = pageInstance;
    }
}
