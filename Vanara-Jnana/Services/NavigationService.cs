using ClassicSamplesBrowser.Views;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicSamplesBrowser.Services;

public static class NavigationService
{
    private static Frame _frame;

    public static void Init(Frame frame)
    {
        _frame = frame;
    }

    public static void Navigate(string tag)
    {
        Type page = tag switch
        {
            "Start" => typeof(StartPage),
            "API" => typeof(ApiExplorerPage),
            "Samples" => typeof(SamplesPage),
            _ => null
        };

        // TODO: Fix this. It should be possible to navigate to a page that is not in the switch statement, but it should be in the same assembly as the other pages.
        if (page != null)
            _frame.Navigate(page);
    }
}
