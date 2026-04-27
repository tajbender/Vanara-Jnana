using ClassicSamplesBrowser.Vanara.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassicSamplesBrowser.Vanara.NuGet;

namespace ClassicSamplesBrowser.Vanara.Services;

internal class AssemblyLoaderService
{
    public async Task<IElementInfo?> LoadVanaraAssemblyTreeAsync()
    {
        // Get Vanara packages from NuGet
//        var packages = await NuGetUtils.GetVanaraPackagesAsync();
//
//        // Select the latest version (simple for now)
//        var latest = packages.OrderByDescending(p => p.Version).FirstOrDefault();
//        if (latest == null)
//            return null;
//
//        // Download package and extract DLLs
//        var dllPaths = await NuGetUtils.DownloadAndExtractAssembliesAsync(latest);
//
//        // Create reflection tree
//        var root = AssemblyElements.CreateFromAssemblies(dllPaths);
//
//        return root;
        return null;
    }
}
