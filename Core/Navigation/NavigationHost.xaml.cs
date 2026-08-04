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
        Presenter.Content = pageInstance;
    }
}
