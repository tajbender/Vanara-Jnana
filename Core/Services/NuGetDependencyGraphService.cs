using Jnana.Workbench.Controls;
using NuGet.Common;
using NuGet.ProjectModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jnana.Core.Services;

public sealed class NuGetDependencyGraphService : INuGetDependencyGraphService
{
    public async Task<DependencyGraphResult> GetDependencyGraphAsync(string projectPath)
    {
        var dgFile = Path.Combine(Path.GetDirectoryName(projectPath)!, "project.assets.json");

        if (!File.Exists(dgFile))
            throw new FileNotFoundException("project.assets.json not found", dgFile);

        var lockFile = LockFileUtilities.GetLockFile(dgFile, NullLogger.Instance);

        var topLevel = new List<PackageInfo>();
        var transitive = new List<PackageInfo>();

        foreach (var library in lockFile.Libraries)
        {
            if (library.Type != "package")
                continue;

            var info = new PackageInfo(library.Name, library.Version.ToNormalizedString());

            // TODO:            if (library.IsDirectReference)
            if (true) // Placeholder for actual check for direct reference
                topLevel.Add(info);
            else
                transitive.Add(info);
        }

        return new DependencyGraphResult(topLevel, transitive);
    }
}
