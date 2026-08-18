using Jnana.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jnana.Core.Services;

public static class GitHubApi
{
    private static readonly HttpClient http = new();

    public static async Task<List<ReleaseInfo>> GetLatestReleasesAsync()
    {
        var url = "https://api.github.com/repos/dahall/Vanara/releases?per_page=10";

        http.DefaultRequestHeaders.UserAgent.ParseAdd("ElectrifierWorkbench");

        var json = await http.GetStringAsync(url);
        var data = JsonSerializer.Deserialize<List<ReleaseInfo>>(json);

        return data;
    }
}
