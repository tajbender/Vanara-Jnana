using System;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Workbench.Pages.Workbench;

public sealed partial class WorkbenchPage : Page
{
    public WorkbenchPage()
    {
        InitializeComponent();

        //LaunchBar.PageRequested += OnPageRequested;
    }

    private void OnPageRequested(Type pageType)
    {
        try
        {
            var page = Activator.CreateInstance(pageType);

            // WorkbenchContent Replace current content with the new page
            WorkbenchContent.Children.Clear();
            WorkbenchContent.Children.Add(page as UIElement);
        }
        catch
        {
            Debug.WriteLine($"Failed to create page of type {pageType.FullName}");
            throw;
        }
    }
}