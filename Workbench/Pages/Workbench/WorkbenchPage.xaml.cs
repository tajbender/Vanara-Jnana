using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace Jnana.Workbench.Pages.Workbench;

public sealed partial class WorkbenchPage : Page
{
    public string TitleText { get; set; } = "Vanara Jñāna";
    public string SubtitleText { get; set; } = "Workbench";
    public bool IsBackButtonVisible { get; set; } = true;
    public bool IsBackButtonEnabled { get; set; } = false;

    public WorkbenchPage()
    {
        InitializeComponent();
        LaunchBar.PageRequested += OnPageRequested;
    }

    private void OnPageRequested(Type pageType)
    {
        try
        {
            // Minimal: direkte Transformation
            var page = Activator.CreateInstance(pageType);

            // WorkbenchContent wird ersetzt
            WorkbenchContent.Children.Clear();
            WorkbenchContent.Children.Add(item: page as UIElement);
        }
        catch
        {
            Debug.WriteLine($"Failed to create page of type {pageType.FullName}");
        }
    }
}
