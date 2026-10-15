using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Jnana.ViewModels;

namespace Jnana.Core.Services;

public static class GitHubApi
{
    private static readonly HttpClient httpClient = new();

    public static async Task<List<ReleaseInfo>> GetLatestReleasesAsync()
    {
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("ElectrifierWorkbench");

        // TODO: Consider to add "per_page=10" as optional parameter to limit the number of releases returned by the API.
        var url = "https://api.github.com/repos/dahall/Vanara/releases?per_page=10";
        var json = await httpClient.GetStringAsync(url);
        List<ReleaseInfo>? data = JsonSerializer.Deserialize<List<ReleaseInfo>>(json);

        return data ?? [];
    }
}
