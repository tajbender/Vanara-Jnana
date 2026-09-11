using System.Threading.Tasks;
using Jnana.Workbench.Controls;

namespace Jnana.Core.Services;

public sealed class NuGetPreLoadService(NuGetTreeViewModel treeViewModel) : INuGetPreLoadService
{
    public async Task PreLoadAsync(string projectPath)
    {
        await treeViewModel.LoadAsync(projectPath);
    }
}