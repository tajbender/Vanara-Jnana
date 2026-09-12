using System.Threading.Tasks;
using Jnana.Workbench.Controls;

namespace Jnana.Core.Services;

public sealed class AppServiceHost
{
    public AppServiceHost()
    {
        // Core
        var coreGraph = new NuGetDependencyGraphService();

        // Decorators
        var cachedGraph = new NuGetDependencyGraphCache(coreGraph);

        DependencyGraphService = cachedGraph;

        // ViewModels
        NuGetTreeViewModel = new NuGetTreeViewModel(DependencyGraphService);

        // Preload
        PreLoadService = new NuGetPreLoadService(NuGetTreeViewModel);
    }

    // Core ServiceHost
    public INuGetDependencyGraphService DependencyGraphService { get; }

    public NuGetTreeViewModel NuGetTreeViewModel { get; }

    public INuGetPreLoadService PreLoadService { get; }

    public async Task InitializeAsync(string projectPath)
    {
        await PreLoadService.PreLoadAsync(projectPath);
    }
}