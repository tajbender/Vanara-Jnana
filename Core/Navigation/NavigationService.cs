using Microsoft.UI.Xaml.Controls;
using System;

namespace Jnana.Core.Navigation;

public class NavigationService : INavigationService
{
    //private readonly Frame _frame;

    public Type? CurrentPage { get; private set; }

    /*  // private readonly Frame _frame;
        public NavigationService(Frame frame) => _frame = frame; */

    public void Navigate(Type pageType)
    {
        CurrentPage = pageType;
        // TODO: Implement navigation logic:
        //       public void Navigate(Type pageType) => _frame.Navigate(pageType);
    }

    public void Navigate<TPage>() where TPage : class
    {
        Navigate(typeof(TPage));
    }

    public void GoBack()
    {
        // TODO: Implement back navigation
        //       public void GoBack() { if (_frame.CanGoBack) _frame.GoBack(); }     
    }
}
