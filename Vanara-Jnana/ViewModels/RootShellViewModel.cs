using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Maps;
using Jnana.Services;

namespace Jnana.ViewModels;

public class RootShellViewModel
{
    public NuGetService NuGet { get; }
//    public GitHubService GitHub { get; }
//    public ApiService Api { get; }
//
//    public NuGetListViewModel NuGetList { get; }
//    public ApiExplorerViewModel ApiExplorer { get; }
//
//    public ObservableCollection<WorkbenchTabViewModel> Tabs { get; }
}

public class NuGetService
{
//    public Task<IReadOnlyList<NuGetPackage>> SearchAsync(string query);
}

public class NuGetListViewModel
{
//    private readonly NuGetService _service;
//
//    public ObservableCollection<NuGetPackageViewModel> Packages { get; }
//
//    public async Task LoadAsync()
//    {
//        var result = await _service.SearchAsync("");
//        Packages.Clear();
//        foreach (var pkg in result)
//            Packages.Add(new NuGetPackageViewModel(pkg));
//    }
}
