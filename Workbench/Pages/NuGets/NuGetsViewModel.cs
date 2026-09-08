using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Core.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Jnana.Workbench.NuGet;

public partial class NuGetsViewModel : ObservableObject
{
    private readonly INuGetCatalogService _catalogService;

    [ObservableProperty]
    private string searchQuery = string.Empty;

    [ObservableProperty]
    private NuGetPackageInfo? selectedPackage;

    public ObservableCollection<NuGetPackageInfo> Packages { get; } = [];

    public NuGetsViewModel(INuGetCatalogService catalogService)
    {
        this._catalogService = catalogService;
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        this.Packages.Clear();
        // TODO:            var results = await _catalogService.SearchAsync(searchQuery);
        // TODO:            foreach (var pkg in results)
        // TODO:                Packages.Add(pkg);
    }

    [RelayCommand]
    public void NavigateToPackage(NuGetPackageInfo package)
    {
        this.selectedPackage = package;

        // Workbench-Morphing:
        // NavigationService.MorphTo("nuget://" + package.Id);
    }
}