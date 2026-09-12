using System.Threading.Tasks;
using Jnana.Workbench.Controls;

namespace Jnana.Core.Services;

public sealed class AppServiceHost
{
    private readonly INuGetDependencyGraphService _dependencyGraphService;
    private readonly NuGetTreeViewModel _nuGetTreeViewModel;
    private readonly INuGetPreLoadService _preLoadService;

    public AppServiceHost()
    {
        // Core
        var coreGraph = new NuGetDependencyGraphService();

        // Decorators
        var cachedGraph = new NuGetDependencyGraphCache(coreGraph);

        _dependencyGraphService = cachedGraph;

        // ViewModels
        _nuGetTreeViewModel = new NuGetTreeViewModel(DependencyGraphService);

        // Preload
        _preLoadService = new NuGetPreLoadService(NuGetTreeViewModel);
    }

    // Core ServiceHost
    public INuGetDependencyGraphService DependencyGraphService => _dependencyGraphService;

    public NuGetTreeViewModel NuGetTreeViewModel => _nuGetTreeViewModel;

    public INuGetPreLoadService PreLoadService => _preLoadService;

    public async Task InitializeAsync(string projectPath)
    {
        await PreLoadService.PreLoadAsync(projectPath);
    }
}