using System.Collections.ObjectModel;

namespace Jnana.Workbench.Pages.Samples;

internal class SamplesViewModel
{
    private readonly ObservableCollection<SampleModel> _samples = [];

    public ObservableCollection<SampleModel> Samples => _samples;

    // TODO: Add load logic
}