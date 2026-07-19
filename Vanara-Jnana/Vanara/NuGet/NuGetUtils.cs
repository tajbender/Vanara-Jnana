using Microsoft.Windows.ApplicationModel.DynamicDependency;
using System.Runtime.CompilerServices;


namespace Jnana.Vanara.NuGet;

//internal static class NuGetUtils
//{
//    private static readonly SourceCacheContext cacheContext = new();
//    private static readonly SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
//
//    public static async Task DownloadPackagesAsync(IEnumerable<PackageIdentity> packageIds, string downloadDirectory, ILogger? logger, CancellationToken cancellationToken)
//    {
//        foreach (PackageIdentity packageIdentity in packageIds)
//        {
//            logger?.LogInformation($"Downloading package: {packageIdentity}");
//            DownloadResource downloadResource = await repository.GetResourceAsync<DownloadResource>(cancellationToken);
//            DownloadResourceResult result = await downloadResource.GetDownloadResourceResultAsync(packageIdentity, new PackageDownloadContext(new SourceCacheContext()), downloadDirectory, logger, cancellationToken);
//            if (result.Status == DownloadResourceResultStatus.Available)
//            {
//                // Save the .nupkg file to the download directory
//                string destinationPath = Path.Combine(downloadDirectory, $"{packageIdentity.Id}.{packageIdentity.Version}.nupkg");
//                using FileStream fileStream = File.Create(destinationPath);
//                await result.PackageStream.CopyToAsync(fileStream, cancellationToken);
//                fileStream.Close();
//            }
//        }
//    }
//
//    public static async Task ExtractAssembliesFromPacakgesAsync(string packagesDirectory, string dotnetVersion, string extractionDirectory, ILogger? logger, CancellationToken cancellationToken)
//    {
//        var targetFramework = NuGetFramework.Parse(dotnetVersion);
//        IFrameworkNameProvider frameworkNameProvider = DefaultFrameworkNameProvider.Instance;
//        var reducer = new FrameworkReducer(frameworkNameProvider, DefaultCompatibilityProvider.Instance);
//        Directory.CreateDirectory(extractionDirectory);
//        foreach (var packageFile in Directory.EnumerateFiles(packagesDirectory, "*.nupkg"))
//        {
//            logger?.LogInformation($"Processing package: {packageFile}");
//            using var packageReader = new PackageArchiveReader(packageFile);
//            IEnumerable<FrameworkSpecificGroup> libItems = await packageReader.GetLibItemsAsync(cancellationToken);
//            NuGetFramework? bestMatch = reducer.GetNearest(targetFramework, libItems.Select(x => x.TargetFramework));
//            foreach (var assemblyPath in libItems.FirstOrDefault(i => i.TargetFramework.Equals(bestMatch))?.Items.Where(i => i.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) || i.EndsWith(".nuspec", StringComparison.OrdinalIgnoreCase)) ?? [])
//            {
//                logger?.LogInformation($"Extracting assembly: {assemblyPath} from package: {packageFile}");
//                using Stream assemblyStream = packageReader.GetStream(assemblyPath);
//                using FileStream fileStream = File.Create(Path.Combine(extractionDirectory, Path.GetFileName(assemblyPath)));
//                await assemblyStream.CopyToAsync(fileStream, cancellationToken);
//            }
//        }
//    }
//
//    public static async IAsyncEnumerable<IPackageSearchMetadata> LoadNuGetPackageListAsync(string prefix, ILogger? logger, [EnumeratorCancellation] CancellationToken cancellationToken)
//    {
//        // Get the search resource from the repository
//        PackageSearchResource searchResource = await repository.GetResourceAsync<PackageSearchResource>(cancellationToken);
//
//        // Set search filters (e.g., include pre-release versions)
//        SearchFilter searchFilter = new(false, SearchFilterType.IsLatestVersion);
//
//        // Execute the search (take the top 10 results)
//        int skip = 0;
//        const int maxTake = 150, chunkSize = 30;
//        while (skip < maxTake)
//        {
//            bool found = false;
//            foreach (IPackageSearchMetadata? m in await searchResource.SearchAsync(prefix, searchFilter, skip: skip, take: chunkSize, logger, cancellationToken))
//            {
//                found = true;
//                yield return m;
//            }
//            if (!found)
//                break;
//            skip += chunkSize;
//        }
//    }
//
//    public static async Task<string[]> LoadPackageVersionsAsync(IPackageSearchMetadata metadata, ILogger? logger, CancellationToken cancellationToken) =>
//        [.. metadata.GetVersionsAsync().Result.OrderByDescending(v => v.Version).Select(v => v.Version.ToString()!)];
//
//    public static async Task ResolveDependenciesAsync(PackageIdentity package, NuGetFramework framework, HashSet<SourcePackageDependencyInfo> allPackages, DependencyInfoResource? depResource, ILogger? logger, CancellationToken cancellationToken)
//    {
//        if (allPackages.Contains(package)) return;
//
//        depResource ??= await repository.GetResourceAsync<DependencyInfoResource>(cancellationToken);
//        SourcePackageDependencyInfo dependencyInfo = await depResource.ResolvePackage(package, framework, cacheContext, logger, cancellationToken);
//        if (cancellationToken.IsCancellationRequested) return;
//        if (dependencyInfo != null)
//        {
//            allPackages.Add(dependencyInfo);
//            foreach (PackageDependency dependency in dependencyInfo.Dependencies)
//            {
//                if (cancellationToken.IsCancellationRequested) return;
//                logger?.LogInformation($"Resolving dependency: {dependency.Id} {dependency.VersionRange}");
//                var depIdentity = new PackageIdentity(dependency.Id, dependency.VersionRange.MinVersion);
//                await ResolveDependenciesAsync(depIdentity, framework, allPackages, depResource, logger, cancellationToken);
//            }
//        }
//    }



//public partial class NuGetPackages : Form
//{
//    const string framework = "net8.0";
//    private const string Prefix = "Vanara";
//    readonly List<IPackageSearchMetadata> packages = [];
//    static readonly ILogger logger = NullLogger.Instance; // TODO: Replace with actual logger if needed
//    static readonly CancellationToken cancellationToken = CancellationToken.None; // TODO: Replace with actual cancellation token if needed
//
//    public NuGetPackages()
//    {
//        InitializeComponent();
//    }
//
//    private void NuGetPackages_Load(object sender, EventArgs e)
//    {
//        listBox1.Format += (s, args) => args.Value = args.ListItem is IPackageSearchMetadata r ? r.Title : args.ListItem?.ToString() ?? string.Empty;
//        Task.Factory.StartNew(async () =>
//        {
//            await foreach (var package in NuGetUtils.LoadNuGetPackageListAsync(Prefix, logger, cancellationToken))
//                if (package.Identity.Id.StartsWith(Prefix + '.', StringComparison.OrdinalIgnoreCase))
//                    packages.Add(package);
//            listBox1.Invoke(() => { listBox1.Items.Clear(); listBox1.DataSource = packages; });
//        }, cancellationToken);
//    }
//
//    protected override void OnFormClosing(FormClosingEventArgs e)
//    {
//        try { Directory.Delete(Path.Combine(Path.GetTempPath(), "NuGetDownloads"), true); } catch { }
//        base.OnFormClosing(e);
//    }
//
//    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        listBox2.Items.Clear();
//        if (listBox1.SelectedItem is IPackageSearchMetadata metadata)
//            Task.Factory.StartNew(() =>
//            {
//                string[] items = NuGetUtils.LoadPackageVersionsAsync(metadata, logger, cancellationToken).Result;
//                listBox2.Invoke(() => listBox2.Items.AddRange(items));
//            }, cancellationToken);
//    }
//
//    private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        listBox3.Items.Clear();
//        if (listBox1.SelectedItem is IPackageSearchMetadata metadata && listBox2.SelectedItem is string ver)
//        {
//            listBox3.Items.Add("Getting dependencies...");
//            Task.Factory.StartNew(async () =>
//            {
//                HashSet<SourcePackageDependencyInfo> dependencies = [];
//                await NuGetUtils.ResolveDependenciesAsync(metadata.Identity, NuGetFramework.Parse(framework), dependencies, null, logger, cancellationToken);
//                listBox3.Invoke(() => listBox3.Items[0] = "Downloading packages...");
//                string downloadDir = Path.Combine(Path.GetTempPath(), "NuGetDownloads");
//                await NuGetUtils.DownloadPackagesAsync(dependencies.Select(dep => new PackageIdentity(dep.Id, dep.Version)).Where(id => id.Id.StartsWith(Prefix + '.', StringComparison.OrdinalIgnoreCase)), downloadDir, logger, cancellationToken);
//                listBox3.Invoke(() => listBox3.Items[0] = "Extracting files...");
//                string extractDir = Path.Combine(downloadDir, "extracted");
//                await NuGetUtils.ExtractAssembliesFromPacakgesAsync(downloadDir, framework, extractDir, logger, cancellationToken);
//                listBox3.Invoke(() => { listBox3.Items.Clear(); listBox3.Items.AddRange(Array.ConvertAll(Directory.GetFiles(extractDir), Path.GetFileName)!); });
//            }, cancellationToken);
//        }
//    }
//}
