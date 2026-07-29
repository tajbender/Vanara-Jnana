using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System.Diagnostics;
//using LaunchActivatedEventArgs = Windows.ApplicationModel.Activation.LaunchActivatedEventArgs;

namespace Jnana;

public partial class App : Application
{
    public enum AppTheme
    {
        Light,
        Dark,
        System
    }

    private MainWindow? _mainWindow;
    private bool extendsContentIntoTitleBar = false;
    private AppWindowTitleBar? _appTitleBar;
    //private AppTheme _theme;
    //private bool _isDarkMode;
    //private bool _isLightMode;


    public App()
    {
        InitializeComponent();
        // TODO: AppWindowTitleBar.SetIcon("Assets/VanaraMonkey.png");
        // TODO: AppWindowTitleBar.SetDragRegion(new Rect(0, 0, 100, 32));
        // TODO: CoreWebView2Environment.CreateAsync(null, "C:\\temp\\wv2logs", null);
    }

    private MainWindow? GetOrCreateMainWindow(bool allowInitialCreation = false)
    {
        if (_mainWindow == null && allowInitialCreation)
        {
            _mainWindow = new MainWindow()
            {
                ExtendsContentIntoTitleBar = extendsContentIntoTitleBar
            };

            _appTitleBar = _mainWindow.AppWindow.TitleBar;
            //  _mainWindow.SetTitleBar(MyDragRegion);
            // titleBar.SetIcon("Assets/VanaraMonkey.png");

            Debug.WriteLine($"App.GetOrCreateMainWindow(): Success");
        }
        
        // = //new Windows.Graphics.SizeInt32(1200, 800);
        //_Window.AppWindow.Size...

        return _mainWindow;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        GetOrCreateMainWindow(allowInitialCreation: true)?.Activate();
    }
}
