using System;

namespace Jnana.Core.Navigation;

public class NavigationService : INavigationService
{
    public Type? CurrentPage { get; private set; }

    public void Navigate(Type pageType)
    {
        CurrentPage = pageType;
        // TODO: Implement navigation logic
    }

    public void Navigate<TPage>() where TPage : class
    {
        Navigate(typeof(TPage));
    }

    public void GoBack()
    {
        // TODO: Implement back navigation
    }
}
