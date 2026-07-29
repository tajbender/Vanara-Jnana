using CommunityToolkit.Mvvm.Input;
using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views;

public sealed partial class WorkbenchVoidPage : Page
{
    private WorkbenchVoidViewModel ViewModel { get; }
    private NuGetsAreaViewModel NuGetsViewModel => ViewModel.NuGetsViewModel;
    private GitHubAreaViewModel GitHubViewModel => ViewModel.GitHubViewModel;
    private SamplesAreaViewModel SamplesViewModel => ViewModel.SamplesViewModel;
    private VanaraScienceLaboratoriesViewModel VanaraScienceLaboratoriesViewModel => ViewModel.VanaraScienceLaboratoriesViewModel;


    public WorkbenchVoidPage(NuGetsAreaViewModel nuGetsViewModel)
    {
        InitializeComponent();

        ViewModel = new WorkbenchVoidViewModel()
        {
            NuGetsViewModel = nuGetsViewModel,
            GitHubViewModel = new GitHubAreaViewModel(),
            SamplesViewModel = new SamplesAreaViewModel(),
            VanaraScienceLaboratoriesViewModel = new VanaraScienceLaboratoriesViewModel()
        };

        //            nuGetsViewModel,
        //            new GitHubAreaViewModel(),
        //            new SamplesAreaViewModel(),
        //            new VanaraScienceLaboratoriesViewModel());
    }
}
