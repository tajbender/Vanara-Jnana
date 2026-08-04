using Jnana.Core.Navigation;
using Jnana.ViewModels;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace Jnana.Views.Tiles;

public sealed partial class WorkbenchTile : UserControl
{
    public WorkbenchTileViewModel ViewModel { get; } = new();

    public WorkbenchTile()
    {
        InitializeComponent();
    }

    private void Grid_OnTapped(object sender, TappedRoutedEventArgs e)
    {
// TODO:        NavigationService.Navigate(typeof(WorkbenchPage));
    }
}
