using Jnana.Workbench.Pages;
using Jnana.Workbench.Pages.GitHub;
using Jnana.Workbench.Pages.NuGets;
using Jnana.Workbench.Pages.Samples;
using Jnana.Workbench.Pages.SysInfo;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace Jnana.Workbench.Components;

public sealed partial class QuickLaunchBar : UserControl
{
    public QuickLaunchBar()
    {
        InitializeComponent();
    }


    private void OnDisassemblyClick(object sender, RoutedEventArgs e)
        => RaisePageRequested(typeof(DisassemblyPage));

    private void OnGitHubClick(object sender, RoutedEventArgs e)
        => RaisePageRequested(typeof(GitHubPage));

    private void OnNuGetsClick(object sender, RoutedEventArgs e)
        => RaisePageRequested(typeof(NuGetsPage));

    private void OnSamplesClick(object sender, RoutedEventArgs e)
        => RaisePageRequested(typeof(SamplesPage));

    private void OnToolsAndUtilitiesClick(object sender, RoutedEventArgs e)
        => RaisePageRequested(typeof(ToolsAndUtilitiesPage));

    private void OnSysInfoClick(object sender, RoutedEventArgs e)
        => RaisePageRequested(typeof(SysInfoPage));

    private void OnToolsClick(object sender, RoutedEventArgs e)
        => RaisePageRequested(typeof(ToolsAndUtilitiesPage));

    private void OnWorkbenchClick(object sender, RoutedEventArgs e)
        => RaisePageRequested(typeof(WorkbenchPage));

    public event Action<Type>? PageRequested;

    private void RaisePageRequested(Type pageType)
        => PageRequested?.Invoke(pageType);

    private void StackPanel_HorizontalSnapPointsChanged(object sender, object e)
    {
        // Reaktion auf Layoutänderungen oder neue SnapPoints
        Debug.WriteLine("Horizontal snap points recalculated: {0}: {1}",
            sender.ToString(), e.ToString());
        //(sender as StackPanel)?.HorizontalSnapPoints.Count ?? 0);
    }
}
