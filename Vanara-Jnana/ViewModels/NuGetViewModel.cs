using ClassicSamplesBrowser.Vanara.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Vanara.NuGet;
using System.Collections.ObjectModel;

namespace Vanara.Jnana.ViewModels;

public partial class NuGetViewModel : ObservableObject
{
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
