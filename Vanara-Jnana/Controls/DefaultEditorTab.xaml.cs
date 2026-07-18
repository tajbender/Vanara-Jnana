using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views.Tabs;

public sealed partial class DefaultEditorTab : Page
{
    private object ViewModel { get; set; } = new DefaultEditorTabViewModel();

    public DefaultEditorTab()
    {
        InitializeComponent();
        this.ViewModel = new DefaultEditorTabViewModel();
        this.DataContext = ViewModel;
    }
}

internal class DefaultEditorTabViewModel
{
    public string Title { get; set; } = "Default Editor Tab";
}
