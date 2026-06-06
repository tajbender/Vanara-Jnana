using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Vanara.NuGet;
using System.Collections.ObjectModel;
using System.Security.Policy;

namespace Jnana.ViewModels;

public partial class GitHubAreaViewModel : ObservableObject
{
    private Url GitHubRepoUrl { get; } = new("https://github.com/vanara/Vanara");
    private Url GitHubIssuesUrl { get; } = new("https://github.com/vanara/Vanara/issues");
    private Url GitHubPullRequestsUrl { get; } = new("https://github.com/vanara/Vanara/pulls");
}
