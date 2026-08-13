using Jnana.Views.Tiles;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;

namespace Jnana.Workbench.Pages.Workbench;

public sealed partial class WorkbenchPage : Page
{
    public ObservableCollection<UserControl> Tiles { get; } =
    [
        new WorkbenchTile(),
        new NuGetTile()
    ];

    public string TitleText { get; set; } = "Vanara jñāna";
    public string SubtitleText { get; set; } = "Workbench";
    public bool IsPaneButtonVisible { get; set; } = true;
    public bool ShowBackButtonSetting { get; set; } = true;

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
        WorkbenchContent.Children.Add(item: page as UIElement);
    }
}
