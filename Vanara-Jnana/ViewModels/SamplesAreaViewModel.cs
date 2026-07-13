using CommunityToolkit.Mvvm.Input;
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

    private readonly List<SampleInfo> _samples = new()
    {
        new SampleInfo
        {
            Title = "Sample Restaurant at the and of the world",
            Description = "A hitchhiking example",
            Category = "Category A",
            RunCommand = new RelayCommand(() => { /* Execute Sample 1 */ }),
            CodeLink = "https://github.com/vanara/Sample1"
        },
        new SampleInfo
        {
            Title = "Sample 1",
            Description = "Description for Sample 1",
            Category = "Category A",
            RunCommand = new RelayCommand(() => { /* Execute Sample 1 */ }),
            CodeLink = "https://github.com/vanara/Sample1"
        },
        new SampleInfo
        {
            Title = "Sample 1",
            Description = "Description for Sample 1",
            Category = "Category B",
            RunCommand = new RelayCommand(() => { /* Execute Sample 1 */ }),
            CodeLink = "https://github.com/vanara/Sample1"
        },
        new SampleInfo
        {
            Title = "Sample 1",
            Description = "Description for Sample 1",
            Category = "Category B",
            RunCommand = new RelayCommand(() => { /* Execute Sample 1 */ }),
            CodeLink = "https://github.com/vanara/Sample1"
        },
        new SampleInfo
        {
            Title = "Sample 1",
            Description = "Description for Sample 1",
            Category = "Vanara Science Laboratories",
            RunCommand = new RelayCommand(() => { /* Execute Sample 1 */ }),
            CodeLink = "https://github.com/vanara/Sample1"
        },
    };

    public SamplesAreaViewModel()
    {
        _samples = _samples.ToList();
    }
}
