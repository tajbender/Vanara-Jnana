using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace Jnana.Workbench.Pages.Workbench;

public sealed partial class WorkbenchPage : Page
{
    public WorkbenchPage()
    {
        this.InitializeComponent();

        //LaunchBar.PageRequested += OnPageRequested;
    }

    private void OnPageRequested(Type pageType)
    {
        try
        {
            // Minimal: direkte Transformation
            var page = Activator.CreateInstance(pageType);

            // WorkbenchContent wird ersetzt
            this.WorkbenchContent.Children.Clear();
            this.WorkbenchContent.Children.Add(item: page as UIElement);
        }
        catch
        {
            Debug.WriteLine($"Failed to create page of type {pageType.FullName}");
            throw;
        }
    }
}
