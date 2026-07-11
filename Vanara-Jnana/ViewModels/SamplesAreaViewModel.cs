using System.Windows.Input;

namespace Jnana.ViewModels;

public class SamplesAreaViewModel
{
    public sealed class SampleInfo
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string Category { get; init; }
        public ICommand RunCommand { get; init; }
        public string CodeLink { get; init; }
    }
}
