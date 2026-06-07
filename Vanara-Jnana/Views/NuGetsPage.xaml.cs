using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views;

public sealed partial class NuGetsPage : Page
{
    public NuGetsAreaViewModel ViewModel { get; } = new NuGetsAreaViewModel();
    public NuGetsPage()
    {
        InitializeComponent();
    }
}
