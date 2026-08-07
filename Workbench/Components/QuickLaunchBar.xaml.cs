using Jnana.Workbench.Pages;
using Jnana.Workbench.Pages.GitHub;
using Jnana.Workbench.Pages.NuGets;
using Jnana.Workbench.Pages.Samples;
using Jnana.Workbench.Pages.SysInfo;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Jnana.Workbench.Components;

public sealed partial class QuickLaunchBar : UserControl
{
    public QuickLaunchBar()
    {
        InitializeComponent();
    }

    private void OnWorkbenchClick(object sender, RoutedEventArgs e)
        => RaisePageRequested(typeof(WorkbenchPage));

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

    public event Action<Type>? PageRequested;

    private void RaisePageRequested(Type pageType)
        => PageRequested?.Invoke(pageType);
}
