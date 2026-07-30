using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using Vanara.WinUI.Extensions.Helpers;

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

        this.UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        Debug.Fail($"App.UnhandledException: {e.Message}\n{e.Exception}");
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

        return _mainWindow;
    }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        try
        {
            _mainWindow = this.GetOrCreateMainWindow(allowInitialCreation: true);
            _mainWindow?.Activate();

            // TODO INFO WARN: This doesn't work because the window is not yet activated, so the XamlRoot is null. We need to wait until the window is activated before we can show a message box.
            //            var result = await MessageBox.ShowAsync(
            //                "This program cannot be run in DOS mode – Initialization failed.",
            //                "Jnana Workbench OS",
            //                MessageBoxType.Error,
            //                _mainWindow.Content.XamlRoot);

            // = //new Windows.Graphics.SizeInt32(1200, 800);
            //_Window.AppWindow.Size...
            // TODO: AppWindowTitleBar.SetIcon("Assets/VanaraMonkey.png");
            // TODO: AppWindowTitleBar.SetDragRegion(new Rect(0, 0, 100, 32));
            // TODO: CoreWebView2Environment.CreateAsync(null, "C:\\temp\\wv2logs", null);
        }
        catch
        {
            Debug.Fail("App.OnLaunched(): Failed to initialize the application.");

            // TODO INFO WARN: This doesn't work because the window is not yet activated, so the XamlRoot is null. We need to wait until the window is activated before we can show a message box.

            var fallbackWindow = new Window();
            var fallBackGrid = new TextBox();
            fallbackWindow.Content = fallBackGrid; // Guaranteed XamlRoot
            // fallbackWindow.Activate();

            var result = await MessageBox.ShowAsync(
                "This program cannot be run in DOS mode – Initialization failed.",
                "Jnana Workbench OS",
                MessageBoxType.Error,
                fallBackGrid.XamlRoot);
        }
    }
}
