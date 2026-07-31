using CommunityToolkit.Mvvm.Input;
using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views;

public sealed partial class WorkbenchVoidPage : Page
{
    private WorkbenchVoidViewModel ViewModel { get; }

    public WorkbenchVoidPage(NuGetsAreaViewModel nuGetsViewModel)
    {
        InitializeComponent();

        ViewModel = new WorkbenchVoidViewModel();

        //            nuGetsViewModel,
        //            new GitHubAreaViewModel(),
        //            new SamplesAreaViewModel(),
        //            new VanaraScienceLaboratoriesViewModel());
    }
}
