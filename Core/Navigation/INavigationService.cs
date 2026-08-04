using System;

namespace Jnana.Core.Navigation;

public interface INavigationService
{
    void Navigate(Type pageType);
    void Navigate<TPage>() where TPage : class;
    void GoBack();
    Type? CurrentPage { get; }
}
