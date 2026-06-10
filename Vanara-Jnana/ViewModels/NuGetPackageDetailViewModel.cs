using CommunityToolkit.Mvvm.ComponentModel;
using Jnana.Services;
using Jnana.Helpers;
using Microsoft.UI.Xaml.Media.Imaging;
using NuGet.Versioning;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Jnana.ViewModels;

public sealed partial class NuGetPackageDetailViewModel : ObservableObject
{
    private readonly INuGetCatalogService _catalog;
    private PackageContent? _content;
    public string? Id => _content?.Id;
    public string? Version => _content?.Version?.ToNormalizedString();
    public string? Readme => _content?.Readme;

    private BitmapImage? _icon;
    public BitmapImage? Icon
    {
        get
        {
            if (_icon == null && _content?.IconBytes != null)
                _icon = ImageExtensions.ToBitmapImage(_content.IconBytes);
            return _icon;
        }
    }
    public NuGetPackageDetailViewModel(INuGetCatalogService catalog)
    {
        _catalog = catalog;
    }

    public async Task LoadAsync(string id, NuGetVersion version, CancellationToken token)
    {
        _content = await _catalog.DownloadPackageAsync(id, version, token);
        OnPropertyChanged(nameof(Id));
        OnPropertyChanged(nameof(Version));
        OnPropertyChanged(nameof(Readme));
        OnPropertyChanged(nameof(Icon));
    }

    public void Clear()
    {
        _content = null;
        _icon = null;
        OnPropertyChanged(nameof(Id));
        OnPropertyChanged(nameof(Version));
        OnPropertyChanged(nameof(Readme));
        OnPropertyChanged(nameof(Icon));
    }

    /*
                // Handle the selection of the package version info
                //await PackageDetailViewModel.LoadAsync(SelectedPackage.Id, NuGet.Versioning.NuGetVersion.Parse(SelectedPackage.Version), cancelToken).ContinueWith(t =>
                //{
                //    if (t.IsFaulted)
                //    {
                //        Debug.Print($"Failed to load package details: {t.Exception}");
                //        //logger?.LogError(t.Exception, "Failed to load package details");
                //    }
                //}, cancelToken);
                //
                //await PackageDetailViewModel.LoadAsync(SelectedPackage.Id, NuGet.Versioning.NuGetVersion.Parse(SelectedPackage.Version), CancellationToken.None);
     
     */
}
