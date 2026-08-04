using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Jnana.Workbench.Pages.Workbench;

public sealed partial class WorkbenchPage : Page
{
    public WorkbenchPage()
    {
        InitializeComponent();
        LaunchBar.PageRequested += OnPageRequested;
    }

    private void OnPageRequested(Type pageType)
    {
        // Minimal: direkte Transformation
        var page = Activator.CreateInstance(pageType);

        // WorkbenchContent wird ersetzt
        WorkbenchContent.Children.Clear();
        WorkbenchContent.Children.Add((UIElement)page);
    }
}
