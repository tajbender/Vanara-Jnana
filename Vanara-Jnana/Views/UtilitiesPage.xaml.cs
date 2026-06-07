using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views;

public sealed partial class UtilitiesPage : Page
{
    public UtilitiesPage()
    {
        InitializeComponent();
        //Loaded += OnLoaded;
    }

    //private void OnLoaded(object sender, RoutedEventArgs e)
    //{
    //    if (!ToolManager.HasOpenTools)
    //    {
    //        ToolManager.Open<HexEditorTool>();
    //    }
    //}
}
