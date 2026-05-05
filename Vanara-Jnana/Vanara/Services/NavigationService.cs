using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassicSamplesBrowser.Views;

namespace ClassicSamplesBrowser.Vanara.Services;

public class NavigationService(Frame frame)
{
    protected Frame _frame = frame;

    public Frame? CurrentFrame => _frame;

    // TODO: Events: Navigated, Navigating, NavigationFailed, NavigationStopped

    public bool Navigate(Type pageType, object? parameter = null, bool writeHistory = true)
    {
        try 
        {
            if (pageType != null)
            {
                return parameter != null ? 
                    _frame.Navigate(pageType, parameter) 
                    : _frame.Navigate(pageType);
            }
        }
        catch (Exception ex)
        {
            // Handle navigation exceptions as needed
            Debug.WriteLine($"Navigation error: {ex.Message}");
            return false;
        }

        if (writeHistory)
        {
        }

        return true;
    }

    public bool CanGoBack => _frame.CanGoBack;
    public bool CanGoForward => _frame.CanGoForward;

    public void GoBack()
    {
        if (_frame.CanGoBack)
            _frame.GoBack();
    }

    public void GoForward()
    {
        if (_frame.CanGoForward)
            _frame.GoForward();
    }
}