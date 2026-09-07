using Jnana.Workbench.Pages.GitHub;
using Jnana.Workbench.Pages.NuGets;
using Jnana.Workbench.Pages.Samples;
using Jnana.Workbench.Pages.SysInfo;
using Jnana.Workbench.Pages.Workbench;
using Jnana.Workbench.Pages;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System;

namespace Jnana.Workbench.Controls;

public sealed partial class QuickLaunchBar : UserControl
{
    /// <summary>
    /// Gets or sets the GitHub user status.
    /// </summary>
    public string GitHubUserStatus
    {
        get => (string)this.GetValue(GitHubUserStatusProperty);
        set => this.SetValue(GitHubUserStatusProperty, value);
    }
    public static readonly DependencyProperty GitHubUserStatusProperty =
        DependencyProperty.Register(
            nameof(GitHubUserStatus),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata("GitHub status: checking..."));

    /// <summary>
    /// Gets or sets the network status.
    /// </summary>
    public string NetworkStatus
    {
        get => (string)this.GetValue(NetworkStatusDependencyProperty);
        set => this.SetValue(NetworkStatusDependencyProperty, value);
    }
    public static readonly DependencyProperty NetworkStatusDependencyProperty =
        DependencyProperty.Register(
            nameof(NetworkStatus),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata("Network status: pending..."));

    public string Orientation
    {
        get => (string)this.GetValue(OrientationDependencyProperty);
        set => this.SetValue(OrientationDependencyProperty, value);
    }

    public static readonly DependencyProperty OrientationDependencyProperty =
        DependencyProperty.Register(
            nameof(Orientation),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata("Vertical"));


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
            new PropertyMetadata("ms-appx:///Assets/Images/DefaultAvatar.png"));

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

    public event Action<object, Type, RoutedEventArgs>? PageRequested;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuickLaunchBar"/> class. This is the `Vanara Jñāna Workbench` `Quick Launch Bar control`,
    /// which provides quick access to various pages and displays user and network status information.
    /// </summary>
    public QuickLaunchBar()
    {
        this.InitializeComponent();
        _ = this.InitializeStatusAsync();
    }

    private async Task InitializeStatusAsync()
    {
        this.UserDisplayName = $"Windows User: `{Environment.UserName}`";
        this.NetworkStatus = "Network status pending...";
        this.GitHubUserStatus = "Checking GitHub User...";

        // Network status
        var networkOk = await CheckNetworkAsync();
        this.NetworkStatus = networkOk ? "Network status: Online" : "Network status: Offline";

        // GitHub user status
        var githubOk = await CheckGitHubAsync();
        this.GitHubUserStatus = githubOk ? "GitHub status: Online" : "GitHub status: Offline";
    }

    private async Task<bool> CheckNetworkAsync()
    {
        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync("8.8.8.8", 2000);
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            Debug.Fail("Failed to check network connectivity.");
            return false;
        }
    }

    private async Task<bool> CheckGitHubAsync()
    {
        try
        {
            using var client = new HttpClient();
            //client.Timeout = TimeSpan.FromSeconds(3);
            var response = await client.GetAsync("https://api.github.com/");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            Debug.Fail("Failed to check GitHub connectivity.");
            return false;
        }
    }

    /// <summary>
    /// Handles the click event for the GitHub button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnGitHubBrowserClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(sender, typeof(GitHubPage), e);
    /// <summary>
    /// Handles the click event for the NuGets button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnNuGetsClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(sender, typeof(NuGetsPage), e);
    /// <summary>
    /// Handles the click event for the Samples button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSamplesClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(sender, typeof(SamplesPage), e);
    /// <summary>
    /// Handles the click event for the System Information button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSysInfoClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(sender, typeof(SysInfoPage), e);
    /// <summary>
    /// Handles the click event for the Tools and Utilities button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnToolsAndUtilitiesClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(sender, typeof(ToolsAndUtilitiesPage), e);
    /// <summary>
    /// Handles the click event for the Workbench button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnWorkbenchClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(sender, typeof(WorkbenchPage), e);
    /// <summary>
    /// Handles the click event for the Settings button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSettingsClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(sender, typeof(SettingsPage), e);

    /// <summary>
    /// Raises the PageRequested event with the specified page type.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="pageType"></param>
    /// <param name="args"></param>
    private void RaisePageRequested(object sender, Type pageType, RoutedEventArgs args)
        => PageRequested?.Invoke(sender, pageType, args);

    private void StackPanel_HorizontalSnapPointsChanged(object sender, object e)
    {
        // TODO: React to Layout Updates or new SnapPoints
        Debug.WriteLine("Horizontal snap points recalculated: {0}: {1}", sender.ToString(), e.ToString());
        //(sender as StackPanel)?.HorizontalSnapPoints.Count ?? 0);
    }
}
