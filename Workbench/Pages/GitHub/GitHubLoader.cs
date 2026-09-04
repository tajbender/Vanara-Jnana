namespace Jnana.Workbench.Pages.GitHub;

/// <summary>
/// This class is responsible for loading data from GitHub using the GitHub API.
/// </summary>
public class GitHubLoader
{
    public static string GetRepositoryInfo(string owner, string repo)
    {
        return $"Repository: {owner}/{repo}";
    }
    public static string GetIssueInfo(string owner, string repo, int issueNumber)
    {
        return $"Issue #{issueNumber} in {owner}/{repo}";
    }
    public static string GetPullRequestInfo(string owner, string repo, int prNumber)
    {
        return $"Pull Request #{prNumber} in {owner}/{repo}";
    }
    public static string GetCommitInfo(string owner, string repo, string commitSha)
    {
        return $"Commit {commitSha} in {owner}/{repo}";
    }
    public static string GetBranchInfo(string owner, string repo, string branchName)
    {
        return $"Branch {branchName} in {owner}/{repo}";
    }
    public static string GetTagInfo(string owner, string repo, string tagName)
    {
        return $"Tag {tagName} in {owner}/{repo}";
    }
    public static string GetReleaseInfo(string owner, string repo, string releaseTag)
    {
        return $"Release {releaseTag} in {owner}/{repo}";
    }
    public static string GetContributorInfo(string owner, string repo, string contributorLogin)
    {
        return $"Contributor {contributorLogin} in {owner}/{repo}";
    }
}
