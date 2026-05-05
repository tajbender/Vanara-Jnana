using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassicSamplesBrowser.Views;

namespace ClassicSamplesBrowser.Vanara.Services;

public class NavigationService(Frame defaultFrame)
{
    // The Frame control used for navigation
    protected Frame Frame = defaultFrame;

    public List<Frame> NavigationHistory { get; } = [];

    // TODO: Events: Navigated, Navigating, NavigationFailed, NavigationStopped

    // object = null; => Home!
    public bool Navigate(object navigationTarget, object? parameter = null, bool writeHistory = true)
    {
        try 
        {
            if (navigationTarget != null)
            {
                return parameter != null ?
                    Frame.Navigate(navigationTarget.GetType(), parameter) 
                    : Frame.Navigate(navigationTarget.GetType());
            }

            // TODO: search the web
        }
        catch (Exception ex)
        {
            // Handle navigation exceptions as needed.
            Debug.WriteLine($"Navigation error: {ex.Message}");
            return false;
        }

        if (writeHistory)
        {
            // Navigate to the target page and add it to the navigation history
        }

        return true;
    }

    public bool CanGoBack => Frame.CanGoBack;
    public bool CanGoForward => Frame.CanGoForward;

    public void GoBack()
    {
        if(CanGoBack)
        {
            Frame.GoBack();
        }
    }

    public void GoForward()
    {
        if(CanGoForward)
        {
            Frame.GoForward();
        }
    }
}
