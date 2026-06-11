using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;   // INFO: `ContentPresenter` here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jnana.ViewModels;

public partial class VoidPageViewModel : ObservableObject
{
    public NuGetsAreaViewModel NuGetsViewModel { get; }
    public GitHubAreaViewModel GitHubViewModel { get; }
    public SamplesAreaViewModel SamplesViewModel { get; }

    public VoidPageViewModel(
        NuGetsAreaViewModel nuget,
        GitHubAreaViewModel github,
        SamplesAreaViewModel samples)
    {
        NuGetsViewModel = nuget;
        GitHubViewModel = github;
        SamplesViewModel = samples;
    }
}
