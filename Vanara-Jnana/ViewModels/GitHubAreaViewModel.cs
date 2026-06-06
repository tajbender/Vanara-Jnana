using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Vanara.NuGet;
using System.Collections.ObjectModel;
using System.Security.Policy;

namespace Jnana.ViewModels;

public partial class GitHubAreaViewModel : ObservableObject
{
    private readonly Url gitHubRepoUrl = new("https://github.com/vanara/Vanara");
    private readonly Url gitHubIssuesUrl = new("https://github.com/vanara/Vanara/issues");
    private readonly Url gitHubPullRequestsUrl = new("https://github.com/vanara/Vanara/pulls");

    public Url GitHubRepoUrl => gitHubRepoUrl;
    public Url GitHubIssuesUrl => gitHubIssuesUrl; 
    public Url GitHubPullRequestsUrl => gitHubPullRequestsUrl;
}
