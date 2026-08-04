using Jnana.Workbench.Controls;
using System.Threading.Tasks;

namespace Jnana.Core.Services;

public sealed class NuGetPreLoadService : INuGetPreLoadService
{
    private readonly NuGetTreeViewModel _treeViewModel;

    public NuGetPreLoadService(NuGetTreeViewModel treeViewModel)
    {
        _treeViewModel = treeViewModel;
    }

    public async Task PreLoadAsync(string projectPath)
    {
        await _treeViewModel.LoadAsync(projectPath);
    }
}
