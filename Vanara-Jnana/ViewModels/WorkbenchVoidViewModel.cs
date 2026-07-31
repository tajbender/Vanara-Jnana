using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;   // INFO: `ContentPresenter` here

namespace Jnana.ViewModels;

public partial class WorkbenchVoidViewModel : ObservableObject
{

    public RelayCommand NavigateGitHubCommand;
    public RelayCommand NavigateNuGetCommand;
    public RelayCommand NavigateSamplesCommand;
    public RelayCommand NavigateSettingsCommand;
    public RelayCommand NavigateSysInfoCommand;
    public RelayCommand NavigateToolsCommand;
    public RelayCommand NavigateVanaraScienceLaboratoriesCommand;


    public WorkbenchVoidViewModel()
    {
    }
}
