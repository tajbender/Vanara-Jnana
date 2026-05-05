using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassicSamplesBrowser.Views;

namespace ClassicSamplesBrowser.Vanara.Services;

public class NavigationService(Frame frame)
{
    protected Frame _frame = frame;

    public Frame? CurrentFrame => _frame;

    public void Navigate(Type pageType, object? parameter = null, bool writeHistory = true)
    {
        if (pageType != null)
        {
            _frame.Navigate(pageType, parameter);
            //bool navigated = parameter ? 
            //    _frame.Navigate(pageType, parameter)
            //    : _frame.Navigate(pageType);        
        }
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