using System.Collections.ObjectModel;

namespace Jnana.Workbench.Pages.Samples;

internal class SamplesViewModel
{
    public ObservableCollection<SampleModel> Samples { get; } = [];

    // TODO: Add load logic
}
