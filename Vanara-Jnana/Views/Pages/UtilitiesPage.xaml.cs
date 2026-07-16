using Jnana.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views;

public sealed partial class UtilitiesPage : Page
{
    private UtilitiesAreaViewModel ViewModel { get; } = new UtilitiesAreaViewModel();
    public UtilitiesPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        //        if (!ToolManager.HasOpenTools)
        //        {
        //            ToolManager.Open<HexEditorTool>();
        //        }
    }
}
