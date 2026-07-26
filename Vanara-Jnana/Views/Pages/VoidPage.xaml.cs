using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views;

public sealed partial class VoidPage : Page
{
    private VoidPageViewModel ViewModel { get; }
    private NuGetsAreaViewModel NuGetsViewModel => ViewModel.NuGetsViewModel;
    private GitHubAreaViewModel GitHubViewModel => ViewModel.GitHubViewModel;
    private SamplesAreaViewModel SamplesViewModel => ViewModel.SamplesViewModel;
    private VanaraScienceLaboratoriesViewModel VanaraScienceLaboratoriesViewModel => ViewModel.VanaraScienceLaboratoriesViewModel;


    public VoidPage(NuGetsAreaViewModel nuGetsViewModel)
    {
        InitializeComponent();

        // TODO: Consider using dependency injection to provide these view models,
        // especially if they need to maintain state or interact with services
        ViewModel = new VoidPageViewModel(
            nuGetsViewModel,
            new GitHubAreaViewModel(),
            new SamplesAreaViewModel(),
            new VanaraScienceLaboratoriesViewModel());
    }
}
