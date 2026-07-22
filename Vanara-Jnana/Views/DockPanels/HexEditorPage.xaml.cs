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

        // TODO: Consider using a more robust method to determine the current process's executable path,
        // especially in scenarios where the application might be running in a different context or environment.
        // TODO: Move this to App Class Context
        var filename = Environment.ProcessPath; //Process.GetCurrentProcess().MainModule.FileName;
        if(filename != null)
        {
            OpenFile(filename);
        }
    }

    public void OpenFile(string path)
    {
        ViewModel.LoadFile(path);
    }
}
