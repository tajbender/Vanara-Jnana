using Microsoft.UI.Xaml.Controls;
using System;

namespace Jnana.Core.Navigation;

public class NavigationService : INavigationService
{
    private readonly Frame _frame;
    private readonly NavigationHost _host;

    public Type? CurrentPage { get; private set; } = null;
    public NavigationHost SecondaryRightSidebarHost { get; }

    public event EventHandler Navigated;

    public NavigationService(NavigationHost host, Frame frame)
    {
        _host = host;
        _frame = frame;

        // pageInstance
        //_PrimaryNavigation = new NavigationService(MainGridHost);

    }

    public NavigationService(NavigationHost secondaryRightSidebarHost)
    {
        this.SecondaryRightSidebarHost = secondaryRightSidebarHost;
    }

    public void Navigate(Type pageType)
    {
        try
        {
            var page = Activator.CreateInstance(pageType);
            CurrentPage = pageType;

            // TODO: Implement navigation logic:
            //ShowPage(page);

            Navigated?.Invoke(this, EventArgs.Empty);
        }
        catch
        {
            CurrentPage = null;
        }
    }

    private void ShowPage(Frame page)
    {
        if (CurrentPage != null)
        {
            try
            {
                _frame.Navigate(page.GetType());
            }
            catch
            {
                // Handle navigation failure
            }
        }
    }

    public void Navigate<TPage>() where TPage : class
    {
        Navigate(typeof(TPage));

        Navigated?.Invoke(this, EventArgs.Empty);
    }

    // TODO: Implement back navigation
    public void GoBack()
    {
        if (CurrentPage != null)
        {
            if (_frame.CanGoBack)
                _frame.GoBack();
            Navigated?.Invoke(this, EventArgs.Empty);
        }
    }
}
