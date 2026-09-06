using Jnana.Workbench.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jnana.Core.Services;

public sealed class NuGetDependencyGraphCache : INuGetDependencyGraphService
{
    private readonly INuGetDependencyGraphService _inner;
    private readonly Dictionary<string, DependencyGraphResult> _cache = [];

    public NuGetDependencyGraphCache(INuGetDependencyGraphService inner)
    {
        this._inner = inner;
    }

    public async Task<DependencyGraphResult> GetDependencyGraphAsync(string projectPath)
    {
        // cache hit
        if (this._cache.TryGetValue(projectPath, out var cached))
            return cached;

        try
        {
            var result = await this._inner.GetDependencyGraphAsync(projectPath);

            this._cache[projectPath] = result;

            return result;
        }
        catch
        {
            // in case of an error, return the cached value if available
            if (this._cache.TryGetValue(projectPath, out var fallback))
                return fallback;

            // → otherwise, rethrow the exception
            throw;
        }
    }
}
