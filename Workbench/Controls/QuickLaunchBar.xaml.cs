using Jnana.Workbench.Pages;
using Jnana.Workbench.Pages.GitHub;
using Jnana.Workbench.Pages.NuGets;
using Jnana.Workbench.Pages.Samples;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Jnana.Workbench.Controls;

public sealed partial class QuickLaunchBar : UserControl
{
    public string GitHubNetworkStatus
    {
        get => (string)this.GetValue(GitHubNetworkStatusProperty);
        private set => this.SetValue(GitHubNetworkStatusProperty, value);
    }

    public string NetworkStatus
    {
        get => (string)this.GetValue(NetworkStatusDependencyProperty);
        private set => this.SetValue(NetworkStatusDependencyProperty, value);
    }

    public string Orientation
    {
        get => (string)this.GetValue(OrientationDependencyProperty);
        set => this.SetValue(OrientationDependencyProperty, value);
    }

    /// <summary>
    ///     Gets or sets the URI of the user's avatar image.
    /// </summary>
    public string UserAvatarImageUri
    {
        get => (string)this.GetValue(UserAvatarImageUriProperty);
        set => this.SetValue(UserAvatarImageUriProperty, value);
    }

    /// <summary>
    ///     Gets or sets the user's display name.
    /// </summary>
    public string UserDisplayName
    {
        get => (string)this.GetValue(UserDisplayNameProperty);
        set => this.SetValue(UserDisplayNameProperty, value);
    }

    /// <summary>
    ///   Event raised when a page is requested to be displayed.
    ///   The event provides the sender, and <see cref="Type"/> of the requested page, 
    ///   and the <see cref="RoutedEventArgs"/> associated with the event.
    /// </summary>
    public event Action<object, Type, RoutedEventArgs>? PageRequested;

    public QuickLaunchBar()
    {
        this.InitializeComponent();

        // Start the asynchronous initialization of status information
        _ = this.InitializeStatusAsync();
    }

    //private string GitHubNetworkStatus
    // <Page.Resources><ResourceDictionary Source = "ms-appx:///Microsoft.UI.Xaml/DensityStyles/Compact.xaml" />
    // <!--Dependency Properties for binding to the UI elements -->

    #region Dependency Properties
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

    #endregion Dependency Properties

    private async Task InitializeStatusAsync()
    {
        this.UserDisplayName = $"Local user: `{Environment.UserName}`";
        this.NetworkStatus = "Network status pending...";
        this.GitHubNetworkStatus = "GitHub status pending...";

        // Network status
        var networkOk = await CheckNetworkAsync();
        this.NetworkStatus = networkOk ? "Network status: Online" : "Network status: Offline";

        // GitHub status
        if (networkOk)
        {
            var githubOk = await CheckGitHubAsync();
            this.GitHubNetworkStatus = githubOk ? "GitHub status: Online" : "GitHub status: Unreachable";
        }
    }

    private static async Task<bool> CheckNetworkAsync()
    {
        try
        {
            using var ping = new Ping();
            PingReply reply = await ping.SendPingAsync("8.8.8.8", 2000);
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }


    private static async Task<bool> CheckGitHubAsync()
    {
        // TODO: Use a more robust method to check GitHub connectivity,
        // possibly using the GitHub API or a specific endpoint.
        try
        {
            using var client = new HttpClient();

            using var request = new HttpRequestMessage(HttpMethod.Head, "https://github.com/");

            using var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException e)
        {
            Debug.Fail("Failed to check GitHub connectivity: {0}", e.Message);
            return false;
        }
        catch
        {
            Debug.Fail("Failed to check GitHub connectivity.");
            return false;
        }
    }

    private void OnGitHubClick(object sender, RoutedEventArgs e)
    {
        this.RaisePageRequested(sender, typeof(GitHubPage), e);
    }

    private void OnGitHubIssuesClick(object sender, RoutedEventArgs e)
    {
        this.RaisePageRequested(sender, typeof(GitHubPage), e);
    }

    private void OnGitHubDiscussionsClick(object sender, RoutedEventArgs e)
    {
        this.RaisePageRequested(sender, typeof(GitHubPage), e);
    }

    private void OnNuGetsClick(object sender, RoutedEventArgs e)
    {
        this.RaisePageRequested(sender, typeof(NuGetsPage), e);
    }

    private void OnSamplesClick(object sender, RoutedEventArgs e)
    {
        this.RaisePageRequested(sender, typeof(SamplesPage), e);
    }

    private void OnScienceLabsClick(object sender, RoutedEventArgs e)
    {
        this.RaisePageRequested(sender, typeof(ScienceLabsPage), e);
    }

    private void OnToolsAndUtilitiesClick(object sender, RoutedEventArgs e)
    {
        this.RaisePageRequested(sender, typeof(ToolsAndUtilitiesPage), e);
    }

    private void OnWorkbenchClick(object sender, RoutedEventArgs e)
    {
        this.RaisePageRequested(sender, typeof(WorkbenchPage), e);
    }

    private void OnSettingsClick(object sender, RoutedEventArgs e)
    {
        this.RaisePageRequested(sender, typeof(SettingsPage), e);
    }

    private void RaisePageRequested(object sender, Type pageType, RoutedEventArgs args)
    {
        PageRequested?.Invoke(sender, pageType, args);
    }
}
