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
            new PropertyMetadata("GitHub Status: Offline"));

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

    public event Action<Type>? PageRequested;

    public QuickLaunchBar()
    {
        this.InitializeComponent();
        this.UserAvatarImageUri = "ms-appx:///Assets/Images/DefaultAvatar.png";
        this.UserDisplayName = $"Windows User: `{Environment.UserName}`";

        // Vanara Jñāna
    }

    private async Task InitializeStatusAsync()
    {
        NetworkStatus = "pending...";
        GitHubUserStatus = "checking...";

        // Netzwerk prüfen
        bool networkOk = await CheckNetworkAsync();
        NetworkStatus = networkOk ? "Online" : "Offline";

        // GitHub prüfen
        bool githubOk = await CheckGitHubAsync();
        GitHubUserStatus = githubOk ? "Online" : "Offline";
    }

    private async Task<bool> CheckNetworkAsync()
    {
        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync("8.8.8.8", 2000);
            return reply.Status == IPStatus.Success;
        }
        catch { return false; }
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
        catch { return false; }
    }

    /// <summary>
    /// Handles the click event for the GitHub button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnGitHubClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(GitHubPage));

    /// <summary>
    /// Handles the click event for the NuGets button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnNuGetsClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(NuGetsPage));

    /// <summary>
    /// Handles the click event for the Samples button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSamplesClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(SamplesPage));
    /// <summary>
    /// Handles the click event for the System Information button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSysInfoClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(SysInfoPage));
    /// <summary>
    /// Handles the click event for the Tools and Utilities button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnToolsAndUtilitiesClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(ToolsAndUtilitiesPage));

    /// <summary>
    /// Handles the click event for the Workbench button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnWorkbenchClick(object sender, RoutedEventArgs e)
        => this.RaisePageRequested(typeof(WorkbenchPage));

    /// <summary>
    /// Handles the click event for the Settings button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
