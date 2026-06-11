using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views;

public sealed partial class VoidPage : Page
{
    private VoidPageViewModel ViewModel { get; }

    public VoidPage()
    {
        InitializeComponent();

        ViewModel = new VoidPageViewModel(      // TODO: Consider using dependency injection to provide these view models, especially if they need to maintain state or interact with services
            new NuGetsAreaViewModel(),
            new GitHubAreaViewModel(),
            new SamplesAreaViewModel());

    }
}
