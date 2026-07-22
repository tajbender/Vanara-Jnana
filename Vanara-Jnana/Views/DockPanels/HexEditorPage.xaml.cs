using Microsoft.UI.Xaml.Controls;
using Vanara_Jnana.exe.ViewModels;

namespace Vanara_Jnana.exe.Views.DockPanels;

public sealed partial class HexEditorPage : Page
{
    public HexEditorViewModel ViewModel { get; } = new();

    public HexEditorPage()
    {
        InitializeComponent();
        DataContext = ViewModel;

        // Load a default file for demonstration purpose
        // // Change this to a valid file path on your system
        OpenFile("C:\\Windows\\System32\\notepad.exe"); 
    }

    public void OpenFile(string path)
    {
        ViewModel.LoadFile(path);
    }
}
