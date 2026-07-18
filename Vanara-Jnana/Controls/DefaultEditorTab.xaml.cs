using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views.Tabs;

internal class DefaultEditorTabViewModel
{
    public string Title { get; set; } = "[DefaultEditorTab]";
}



public sealed partial class DefaultEditorTab : Page
{
    private object ViewModel { get; set; } = new DefaultEditorTabViewModel();

    private DefaultEditorTabViewModel ViewModelTyped => (DefaultEditorTabViewModel)ViewModel;

    public DefaultEditorTab()
    {
        //InitializeComponent();
        this.DataContext = ViewModel;
    }
}
