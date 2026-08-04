using System.Threading.Tasks;

namespace Jnana.Core.Services;

public interface INuGetPreLoadService
{
    Task PreLoadAsync(string projectPath);
}
