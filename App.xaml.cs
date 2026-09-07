using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Jnana.Core.Services;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;

namespace Jnana;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private Window? _window;

    /// <summary>
    /// Gets the singleton instance of the AppServiceHost for the application.
    /// </summary>
    public static AppServiceHost ServiceHost { get; private set; } = new();

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        try
        {
            //ServiceHost.InitializeAsync(@"C:\Dev\MyProject\MyProject.csproj");
            var config = new SystemBackdropConfiguration();
            this._window = new MainWindow(null, /* TODO */ config);
            this._window.Activate();
        }
        catch (Exception e)
        {
            Debug.Fail($"Error occurred while launching the application: {e.Message}");
            Console.WriteLine(e);
        }
    }
}
