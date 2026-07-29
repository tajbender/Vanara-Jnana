using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;   // INFO: `ContentPresenter` here

namespace Jnana.ViewModels;

public partial class WorkbenchVoidViewModel : ObservableObject
{
    public required NuGetsAreaViewModel NuGetsViewModel { get; init;  }
    public required GitHubAreaViewModel GitHubViewModel { get; init; }
    public required SamplesAreaViewModel SamplesViewModel { get; init; }
    public required VanaraScienceLaboratoriesViewModel VanaraScienceLaboratoriesViewModel { get; init; }

    public RelayCommand NavigateGitHubCommand;
    public RelayCommand NavigateNuGetCommand;
    public RelayCommand NavigateSamplesCommand;
    public RelayCommand NavigateSettingsCommand;
    public RelayCommand NavigateSysInfoCommand;
    public RelayCommand NavigateToolsCommand;
    public RelayCommand NavigateVanaraScienceLaboratoriesCommand;


    public WorkbenchVoidViewModel()
    {
        NuGetsViewModel = new NuGetsAreaViewModel();
        GitHubViewModel = new GitHubAreaViewModel();
        SamplesViewModel = new SamplesAreaViewModel();
        VanaraScienceLaboratoriesViewModel = new VanaraScienceLaboratoriesViewModel();
    }
}
