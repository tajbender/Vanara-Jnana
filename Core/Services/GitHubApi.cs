using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Jnana.ViewModels;
using Jnana.Workbench.Pages.GitHub;

namespace Jnana.Core.Services;

public static class GitHubApi
{
    private static readonly HttpClient httpClient = new();

    // add connection timeout to the HttpClient
    // add a retry policy to the HttpClient
    // add a circuit breaker policy to the HttpClient

    public static async Task<List<ReleaseInfo>> GetLatestReleasesAsync()
    {
        try
        {
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("vanara-jnana");
            var json = await httpClient.GetStringAsync(GitHubViewModel.GitHubReleasesUrl);
            var releaseInfos = JsonSerializer.Deserialize<List<ReleaseInfo>>(json);

            return releaseInfos ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}