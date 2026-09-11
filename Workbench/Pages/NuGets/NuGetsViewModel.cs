using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Core.Services;

namespace Jnana.Workbench.Pages.NuGets;

public partial class NuGetsViewModel(INuGetCatalogService catalogService) : ObservableObject
{
    private readonly INuGetCatalogService _catalogService = catalogService;

    [ObservableProperty] private string searchQuery = string.Empty;

    [ObservableProperty] private NuGetPackageInfo? selectedPackage;

    public ObservableCollection<NuGetPackageInfo> Packages { get; } = [];

    [RelayCommand]
    public async Task LoadAsync()
    {
        Packages.Clear();
        // TODO:            var results = await _catalogService.SearchAsync(searchQuery);
        // TODO:            foreach (var pkg in results)
        // TODO:                Packages.Add(pkg);
    }

    [RelayCommand]
    public void NavigateToPackage(NuGetPackageInfo package)
    {
        selectedPackage = package;

        // Workbench-Morphing:
        // NavigationService.MorphTo("nuget://" + package.Id);
    }
}