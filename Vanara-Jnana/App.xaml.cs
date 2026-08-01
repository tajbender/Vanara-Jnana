using Jnana.Services;
using Jnana.ViewModels;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using Vanara.WinUI.Extensions.Helpers;
using Vanara_Jnana.exe.Services.Navigation.Providers;

//using LaunchActivatedEventArgs = Windows.ApplicationModel.Activation.LaunchActivatedEventArgs;

namespace Jnana;

public partial class App : Application
{
    private MainWindow? _mainWindow;
    private bool extendsContentIntoTitleBar = false;
    private AppWindowTitleBar? _appTitleBar;

    /// <summary>Get the current instance of our <see cref="App"/>.
    /// <remarks><see cref="Application.Current"/> will always be the one single App-Instance
    /// ever created during lifetime. Casting to our <see cref="App"/> will always work.</remarks></summary>
    public static new App Current => (App)Application.Current;

    public NavigationService Navigation { get; } = new();
    public SettingsAreaViewModel Settings { get; } = new();
    public GitHubAreaViewModel GitHub { get; } = new();
    public NuGetsAreaViewModel NuGet { get; } = new();
    public SamplesAreaViewModel Samples { get; } = new();
    public WorkbenchVoidViewModel WorkbenchState { get; } = new();

    public VanaraScienceLaboratoriesViewModel VanaraScienceLaboratoriesViewModel { get; init; }

    public App()
    {
        InitializeComponent();

        this.UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Add `\n` line break to every exception for better logfile visibility
        Debug.Fail($"App.App_UnhandledException: {e.Message}.\n{e.Exception}");
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
        catch (Exception ex)
        {
            Debug.Fail($"App.OnLaunched(): Failed to initialize the application.\n{ex}");

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
