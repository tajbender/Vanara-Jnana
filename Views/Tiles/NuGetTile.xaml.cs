using System;
using System.Diagnostics;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace Jnana.Views.Tiles;

public sealed partial class NuGetTile : UserControl
{
    protected object ViewModel { get; }

    public NuGetTile()
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
        Debug.WriteLine("NuGetTile tapped");
    }
}
