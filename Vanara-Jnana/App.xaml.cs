using Microsoft.UI.Xaml;
//using LaunchActivatedEventArgs = Windows.ApplicationModel.Activation.LaunchActivatedEventArgs;

namespace ClassicSamplesBrowser;

public partial class App : Application
{
    private MainWindow? _mainWindow;

    public App()
    {
        InitializeComponent();
        //AppWindowTitleBar.SetIcon("Assets/VanaraMonkey.png");
    }

    private MainWindow? GetOrCreateMainWindow(bool allowInitialCreation = false)
    {
        if (_mainWindow == null && allowInitialCreation)
        {
            _mainWindow = new MainWindow
            {
                ExtendsContentIntoTitleBar = true
            };
        }

        // = //new Windows.Graphics.SizeInt32(1200, 800);
        //_Window.AppWindow.Size...

        return _mainWindow;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        GetOrCreateMainWindow(true)?.Activate();
    }
}
