using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Jnana.Workbench.Pages;
using Jnana.Workbench.Pages.GitHub;
using Jnana.Workbench.Pages.NuGets;
using Jnana.Workbench.Pages.Samples;
using Jnana.Workbench.Pages.SysInfo;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Workbench.Controls;

public sealed partial class QuickLaunchBar : UserControl
{
    private const string DefaultAvatarImageUri = "ms-appx:///Assets/Images/DefaultAvatar.png";

    public static readonly DependencyProperty GitHubNetworkStatusProperty =
        DependencyProperty.Register(
            nameof(GitHubNetworkStatus),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata("GitHub status: No Network."));

    public static readonly DependencyProperty NetworkStatusDependencyProperty =
        DependencyProperty.Register(
            nameof(NetworkStatus),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata("Network status: pending..."));

    public static readonly DependencyProperty OrientationDependencyProperty =
        DependencyProperty.Register(
            nameof(Orientation),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata("Vertical"));

    public static readonly DependencyProperty UserAvatarImageUriProperty =
        DependencyProperty.Register(
            nameof(UserAvatarImageUriProperty),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata("ms-appx:///Assets/Images/DefaultAvatar.png"));

    public static readonly DependencyProperty UserDisplayNameProperty =
        DependencyProperty.Register(
            nameof(UserDisplayName),
            typeof(string),
            typeof(QuickLaunchBar),
            new PropertyMetadata(string.Empty));

    private readonly string[] GitHubBrowserItemSource = ["dummy entry", "another entry"];

    /// <summary>
    ///     Initializes a new instance of the <see cref="QuickLaunchBar" /> class. This is the `Vanara Jñāna Workbench` `Quick
    ///     Launch Bar control`,
    ///     which provides quick access to various pages and displays user and network status information.
    /// </summary>
    public QuickLaunchBar()
    {
        InitializeComponent();

        // Start the asynchronous initialization of status information
        _ = InitializeStatusAsync();
    }

    //private string GitHubNetworkStatus

    /// <summary>
    ///     Gets or sets the GitHub user status.
    /// </summary>
    public string GitHubNetworkStatus
    {
        get => (string)GetValue(GitHubNetworkStatusProperty);
        set => SetValue(GitHubNetworkStatusProperty, value);
    }

    /// <summary>
    ///     Gets or sets the network status.
    /// </summary>
    public string NetworkStatus
    {
        get => (string)GetValue(NetworkStatusDependencyProperty);
        set => SetValue(NetworkStatusDependencyProperty, value);
    }

    public string Orientation
    {
        get => (string)GetValue(OrientationDependencyProperty);
        set => SetValue(OrientationDependencyProperty, value);
    }


    /// <summary>
    ///     Gets or sets the URI of the user's avatar image.
    /// </summary>
    public string UserAvatarImageUri
    {
        get => (string)GetValue(UserAvatarImageUriProperty);
        set => SetValue(UserAvatarImageUriProperty, value);
    }

    /// <summary>
    ///     Gets or sets the user's display name.
    /// </summary>
    public string UserDisplayName
    {
        get => (string)GetValue(UserDisplayNameProperty);
        set => SetValue(UserDisplayNameProperty, value);
    }

    public event Action<object, Type, RoutedEventArgs>? PageRequested;

    /// <summary>
    ///     Asynchronously initializes the status information for the QuickLaunchBar, including user display name, network
    ///     status, and GitHub status.
    /// </summary>
    /// <returns></returns>
    private async Task InitializeStatusAsync()
    {
        UserDisplayName = $"Local user: `{Environment.UserName}`";
        NetworkStatus = "Network status pending...";
        GitHubNetworkStatus = "GitHub status pending...";

        // Network status
        var networkOk = await CheckNetworkAsync();
        NetworkStatus = networkOk ? "Network status: Online" : "Network status: Offline";

        // GitHub status
        if (networkOk)
        {
            var githubOk = await CheckGitHubAsync();
            GitHubNetworkStatus = githubOk ? "GitHub status: Online" : "GitHub status: Unreachable";
        }
    }

    private static async Task<bool> CheckNetworkAsync()
    {
        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync("8.8.8.8", 2000);
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }


    private static async Task<bool> CheckGitHubAsync()
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
    ///     Handles the click event for the GitHub button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnGitHubBrowserClick(object sender, RoutedEventArgs e)
    {
        RaisePageRequested(sender, typeof(GitHubPage), e);
    }

    /// <summary>
    ///     Handles the click event for the NuGets button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnNuGetsClick(object sender, RoutedEventArgs e)
    {
        RaisePageRequested(sender, typeof(NuGetsPage), e);
    }

    /// <summary>
    ///     Handles the click event for the Samples button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSamplesClick(object sender, RoutedEventArgs e)
    {
        RaisePageRequested(sender, typeof(SamplesPage), e);
    }

    /// <summary>
    ///     Handles the click event for the System Information button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSysInfoClick(object sender, RoutedEventArgs e)
    {
        RaisePageRequested(sender, typeof(SysInfoPage), e);
    }

    /// <summary>
    ///     Handles the click event for the Tools and Utilities button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnToolsAndUtilitiesClick(object sender, RoutedEventArgs e)
    {
        RaisePageRequested(sender, typeof(ToolsAndUtilitiesPage), e);
    }

    /// <summary>
    ///     Handles the click event for the Workbench button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnWorkbenchClick(object sender, RoutedEventArgs e)
    {
        RaisePageRequested(sender, typeof(WorkbenchPage), e);
    }

    /// <summary>
    ///     Handles the click event for the Settings button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSettingsClick(object sender, RoutedEventArgs e)
    {
        RaisePageRequested(sender, typeof(SettingsPage), e);
    }

    /// <summary>
    ///     Raises the PageRequested event with the specified page type.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="pageType"></param>
    /// <param name="args"></param>
    private void RaisePageRequested(object sender, Type pageType, RoutedEventArgs args)
    {
        PageRequested?.Invoke(sender, pageType, args);
    }

    private void StackPanel_HorizontalSnapPointsChanged(object sender, object e)
    {
        // TODO: React to Layout Updates or new SnapPoints
        Debug.WriteLine("Horizontal snap points recalculated: {0}: {1}", sender, e);
        //(sender as StackPanel)?.HorizontalSnapPoints.Count ?? 0);
    }
}