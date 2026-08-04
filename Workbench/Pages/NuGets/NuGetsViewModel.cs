using System.Collections.ObjectModel;

namespace Jnana.Workbench.Pages.NuGets;

public class NuGetsViewModel
{
    public ObservableCollection<NuGetPackageModel> Packages { get; } = [];

    // TODO: Add search logic
    // TODO: Add load logic
}
