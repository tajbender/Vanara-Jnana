using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace Jnana.Views.Tiles;

public sealed partial class WorkbenchTile : UserControl
{
    public WorkbenchTile()
    {
        InitializeComponent();
    }

    public WorkbenchTileViewModel ViewModel { get; } = new();

    private void Grid_OnTapped(object sender, TappedRoutedEventArgs e)
    {
        // TODO:        NavigationService.Navigate(typeof(WorkbenchPage));
    }
}