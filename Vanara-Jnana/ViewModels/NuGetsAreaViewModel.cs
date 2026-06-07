using ClassicSamplesBrowser.Vanara.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Jnana.ViewModels;

public partial class NuGetsAreaViewModel : ObservableObject
{
    // TODO: Use `NuGetPackageInfo` here:
    public ObservableCollection<String> Packages { get; } = new ObservableCollection<String>();

    public ICommand RefreshCommand { get; }

    public NuGetsAreaViewModel()
    {
        RefreshCommand = new RelayCommand(LoadPackages);
        Packages = new ObservableCollection<String>() { "Vanara.PInvoke.User32", "Vanara.PInvoke.Kernel32", "Vanara.PInvoke.Gdi32" };
    }

    // TODO: 06-08-26: Added this stub... Minor fix, and it should work. The main issue is that the NuGet API is a bit complex, and I need to figure out how to use it properly.
    //public static async IAsyncEnumerable<IPackageSearchMetadata> LoadLatestPackagesAsync(
    //    string prefix, ILogger? logger, [EnumeratorCancellation] CancellationToken cancellationToken)
    //{
    //    PackageSearchResource searchResource =
    //        await repository.GetResourceAsync<PackageSearchResource>(cancellationToken);
    //
    //    SearchFilter filter = new(includePrerelease: false);
    //
    //    IEnumerable<IPackageSearchMetadata> results =
    //        await searchResource.SearchAsync(prefix, filter, skip: 0, take: 200, logger, cancellationToken);
    //
    //    foreach (var pkg in results)
    //    {
    //        // pkg.Versions ist eine IAsyncEnumerable<VersionInfo>
    //        var latest = await pkg.GetVersionsAsync();
    //
    //        var stable = latest
    //            .Where(v => !v.Version.IsPrerelease)
    //            .OrderBy(v => v.Version)
    //            .LastOrDefault();
    //
    //        if (stable != null)
    //            yield return pkg;
    //    }
    //}


    internal async Task<IElementInfo?> LoadVanaraAssemblyTreeAsync()
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

    private void LoadPackages()
    {
        // TODO: API call
    }

    //    private readonly AssemblyLoaderService _loader;
    //
    //    public NuGetViewModel(AssemblyLoaderService loader)
    //    {
    //        _loader = loader;
    //
    //        Packages = new ObservableCollection<NuGetPackageInfo>();
    //        Versions = new ObservableCollection<string>();
    //    }
    //
    //    // -----------------------------
    //    // Collections
    //    // -----------------------------
    //    public ObservableCollection<NuGetPackageInfo> Packages { get; }
    //    public ObservableCollection<string> Versions { get; }
    //
    //    // -----------------------------
    //    // Selected Items
    //    // -----------------------------
    //    [ObservableProperty]
    //    private NuGetPackageInfo? selectedPackage;
    //
    //    partial void OnSelectedPackageChanged(NuGetPackageInfo? value)
    //    {
    //        if (value != null)
    //            LoadVersionsCommand.Execute(value);
    //    }
    //
    //    [ObservableProperty]
    //    private string? selectedVersion;
    //
    //    partial void OnSelectedVersionChanged(string? value)
    //    {
    //        if (value != null)
    //            LoadAssemblyTreeCommand.Execute(value);
    //    }
    //
    //    // -----------------------------
    //    // Reflection Root
    //    // -----------------------------
    //    [ObservableProperty]
    //    private IElementInfo? rootElement;
    //
    //    // -----------------------------
    //    // Loading Flags
    //    // -----------------------------
    //    [ObservableProperty] private bool isLoadingPackages;
    //    [ObservableProperty] private bool isLoadingVersions;
    //    [ObservableProperty] private bool isLoadingAssemblies;
    //
    //    // -----------------------------
    //    // Commands
    //    // -----------------------------
    //    [RelayCommand]
    //    private async Task LoadPackagesAsync()
    //    {
    //        try
    //        {
    //            IsLoadingPackages = true;
    //            Packages.Clear();
    //
    //            var pkgs = await _nuget.GetVanaraPackagesAsync();
    //
    //            foreach (var pkg in pkgs)
    //                Packages.Add(pkg);
    //        }
    //        finally
    //        {
    //            IsLoadingPackages = false;
    //        }
    //    }
    //
    //    [RelayCommand]
    //    private async Task LoadVersionsAsync(NuGetPackageInfo package)
    //    {
    //        try
    //        {
    //            IsLoadingVersions = true;
    //            Versions.Clear();
    //
    //            var versions = await _nuget.GetPackageVersionsAsync(package.Id);
    //
    //            foreach (var v in versions)
    //                Versions.Add(v);
    //        }
    //        finally
    //        {
    //            IsLoadingVersions = false;
    //        }
    //    }
    //
    //    [RelayCommand]
    //    private async Task LoadAssemblyTreeAsync(string version)
    //    {
    //        try
    //        {
    //            IsLoadingAssemblies = true;
    //
    //            var assemblies = await _nuget.DownloadAndExtractAssembliesAsync(
    //                SelectedPackage!.Id,
    //                version
    //            );
    //
    //            RootElement = await _loader.LoadFromAssembliesAsync(assemblies);
    //        }
    //        finally
    //        {
    //            IsLoadingAssemblies = false;
    //        }
    //    }
}
