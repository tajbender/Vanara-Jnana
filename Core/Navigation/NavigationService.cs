using System;

namespace Jnana.Core.Navigation;

public class NavigationService : INavigationService
{
    //private readonly Frame _frame;

    public Type? CurrentPageType { get; private set; }

    /*  // private readonly Frame _frame;
        public NavigationService(Frame frame) => _frame = frame; */

    public void Navigate(Type pageType)
    {
        ArgumentNullException.ThrowIfNull(pageType);

        if (this.CurrentPageType == pageType)
        {
            return; // Already on the requested page
        }

        try
        {
            // TODO: Implement navigation logic:
            //       public void Navigate(Type pageType) => _frame.Navigate(pageType);
            this.CurrentPageType = pageType;
        }
        catch (Exception ex)
        {
            // TODO: Handle navigation errors
            throw new InvalidOperationException("Failed to navigate to the requested page.", ex);
        }
    }

    public void Navigate<TPage>() where TPage : class
    {
        this.Navigate(typeof(TPage));
    }

    public void GoBack()
    {
        // TODO: Implement back navigation
        //       public void GoBack() { if (_frame.CanGoBack) _frame.GoBack(); }     
    }
}