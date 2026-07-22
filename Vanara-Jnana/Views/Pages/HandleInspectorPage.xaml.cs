using Microsoft.UI.Xaml.Controls;
using Vanara_Jnana.exe.ViewModels;

namespace Vanara_Jnana.exe.Views.Pages;

public sealed partial class HandleInspectorPage : Page
{
    public HandleInspectorViewModel ViewModel { get; } = new();

    public HandleInspectorPage()
    {
        InitializeComponent();
        DataContext = ViewModel;
    }
}
