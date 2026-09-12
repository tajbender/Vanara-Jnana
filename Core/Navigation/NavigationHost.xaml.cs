using System.Diagnostics;
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
    private void TabView_AddButtonClick(TabView sender, object args)
    {
        Debug.WriteLine("TabView_AddButtonClick()");
    }

    private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        Debug.WriteLine("TabView_TabCloseRequested()");
    }
}