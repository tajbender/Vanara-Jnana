using System;

namespace Jnana.Core.Navigation;

public interface INavigationService
{
    Type? CurrentPage { get; }
    void Navigate(Type pageType);
    void Navigate<TPage>() where TPage : class;
    void GoBack();
}