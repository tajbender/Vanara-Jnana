using Jnana.Workbench.Controls;
using System.Threading.Tasks;

namespace Jnana.Core.Services;

public sealed class AppServiceHost
{
    // Core ServiceHost
    public INuGetDependencyGraphService DependencyGraphService { get; }
    public NuGetTreeViewModel NuGetTreeViewModel { get; }
    public INuGetPreLoadService PreLoadService { get; }

    public AppServiceHost()
    {
        // Core
        var coreGraph = new NuGetDependencyGraphService();

        // Decorators
        var cachedGraph = new NuGetDependencyGraphCache(coreGraph);

        this.DependencyGraphService = cachedGraph;

        // ViewModels
        this.NuGetTreeViewModel = new NuGetTreeViewModel(this.DependencyGraphService);

        // Preload
        this.PreLoadService = new NuGetPreLoadService(this.NuGetTreeViewModel);
    }

    public async Task InitializeAsync(string projectPath)
    {
        await this.PreLoadService.PreLoadAsync(projectPath);
    }
}
