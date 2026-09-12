using System;
using System.Diagnostics;
using Jnana.Core.Services;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;

namespace Jnana;

/// <summary>
///     Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private Window? _window;

    // TODO: These version strings should be automatically generated from the project file or assembly info. For now, they are hardcoded.
    public string Version = "1.269.14";       // TODO:Full version string with major, minor, and build numbers. Get them from the project file or assembly info.
    public string PackageVersion = "1.269";         // TODO:Full version string with major, minor, and build numbers. Get them from the project file or assembly info.
    private static AppServiceHost _serviceHost = new();

    /// <summary>
    ///     Initializes the singleton application object.  This is the first line of authored code
    ///     executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <summary>
    ///     Gets the singleton instance of the AppServiceHost for the application.
    /// </summary>
    public static AppServiceHost ServiceHost
    {
        get => _serviceHost;
        private set => _serviceHost = value;
    }

    /// <summary>
    ///     Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        try
        {
            //ServiceHost.InitializeAsync(@"C:\Dev\MyProject\MyProject.csproj");
            var config = new SystemBackdropConfiguration();
            _window = new MainWindow(null, /* TODO */ config);
            _window.Activate();
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error occurred while initializing the application: {e.Message}");
            Console.WriteLine(e);
        }
    }
}