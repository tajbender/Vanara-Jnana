using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Diagnostics;

namespace Jnana.Views.Tiles;

public sealed partial class WorkbenchTile : UserControl
{
    public WorkbenchTileViewModel ViewModel { get; } = new();

    public WorkbenchTile()
    {
        try
        {
            this.InitializeComponent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void Grid_OnTapped(object sender, TappedRoutedEventArgs e)
    {
        Debug.WriteLine("WorkbenchTile tapped");
        // TODO:        NavigationService.Navigate(typeof(WorkbenchPage));
    }
}

public class WorkbenchTileViewModel
{
    public string Title => "Workbench";
    public string Description => "Dein Entwicklungsarbeitsplatz";
}
