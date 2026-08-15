using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Core.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Jnana.Workbench.NuGet
{
    public partial class NuGetsViewModel : ObservableObject
    {
        private readonly INuGetCatalogService _catalogService;

        [ObservableProperty]
        private string searchQuery = string.Empty;

        [ObservableProperty]
        private NuGetPackageInfo? selectedPackage;

        public ObservableCollection<NuGetPackageInfo> Packages { get; } = new();

        public NuGetsViewModel(INuGetCatalogService catalogService)
        {
            _catalogService = catalogService;
        }

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
}
