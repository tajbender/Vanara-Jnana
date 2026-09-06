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

namespace Jnana.Workbench.Controls;

public sealed partial class QuickLaunchBar : UserControl
{
    /// <summary>
    /// Gets or sets the URI of the user's avatar image.
    /// </summary>
    public string UserAvatarImageUri
    {
        get => (string)this.GetValue(UserAvatarImageUriProperty);
        set => this.SetValue(UserAvatarImageUriProperty, value);
    }
    public static readonly DependencyProperty UserAvatarImageUriProperty =
        DependencyProperty.Register(
            nameof(UserAvatarImageUriProperty),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the user's display name.
    /// </summary>
    public string UserDisplayName
    {
        get => (string)this.GetValue(UserDisplayNameProperty);
        set => this.SetValue(UserDisplayNameProperty, value);
    }
    public static readonly DependencyProperty UserDisplayNameProperty =
        DependencyProperty.Register(
            nameof(UserDisplayName),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the GitHub status.
    /// </summary>
    public string GitHubStatus
    {
        get => (string)this.GetValue(GitHubStatusProperty);
        set => this.SetValue(GitHubStatusProperty, value);
    }
    public static readonly DependencyProperty GitHubStatusProperty =
        DependencyProperty.Register(
            nameof(GitHubStatus),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata(string.Empty));

    public event Action<Type>? PageRequested;

    public QuickLaunchBar()
    {
        this.InitializeComponent();

        UserAvatarImageUri= "ms-appx:///Assets/Images/DefaultAvatar.png";
        this.UserDisplayName = Environment.UserName;
        this.GitHubStatus = "GitHub Status: Offline";
    }

    private void OnGitHubClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(GitHubPage));
    private void OnNuGetsClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(NuGetsPage));
    private void OnSamplesClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(SamplesPage));
    private void OnSysInfoClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(SysInfoPage));
    private void OnToolsAndUtilitiesClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(ToolsAndUtilitiesPage));
    private void OnWorkbenchClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(WorkbenchPage));
    private void OnSettingsClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(SettingsPage));

    private void RaisePageRequested(Type pageType)
        => PageRequested?.Invoke(pageType);

    private void StackPanel_HorizontalSnapPointsChanged(object sender, object e)
    {
        // TODO: React to Layout Updates or new SnapPoints
        Debug.WriteLine("Horizontal snap points recalculated: {0}: {1}", sender.ToString(), e.ToString());
        //(sender as StackPanel)?.HorizontalSnapPoints.Count ?? 0);
    }
}
