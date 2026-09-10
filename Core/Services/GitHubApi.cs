using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Jnana.ViewModels;

namespace Jnana.Core.Services;

public static class GitHubApi
{
    private static readonly HttpClient http = new();
    private static readonly string GitHubReleasesUrl = @"https://api.github.com/repos/dahall/Vanara/releases?per_page=10";

    // add connection timeout to the HttpClient
    // add a retry policy to the HttpClient
    // add a circuit breaker policy to the HttpClient

    public static async Task<List<ReleaseInfo>> GetLatestReleasesAsync()
    {
        try
        {
            http.DefaultRequestHeaders.UserAgent.ParseAdd("vanara-jnana");
            var json = await http.GetStringAsync(GitHubReleasesUrl);
            var releaseInfos = JsonSerializer.Deserialize<List<ReleaseInfo>>(json);

            return releaseInfos ?? new List<ReleaseInfo>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}