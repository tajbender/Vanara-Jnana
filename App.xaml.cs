using System;
using System.Diagnostics;
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

    public DispatcherTimer VerySlowTimer { get; }

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

        this.VerySlowTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(2.5)
        };
        this.VerySlowTimer.Start();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        try
        {
            // TODO: ServiceHost.InitializeAsync();
            var backdropConfiguration = new SystemBackdropConfiguration();
            this._window = new MainWindow(null, /* TODO */ backdropConfiguration);
            if (this._window != null)
            {
                this._window.Activate();
            }
            else
            {
                throw new InvalidOperationException("Failed to create the main window.");
            }
        }
        catch (Exception e)
        {
            var msgText = $"App.OnLaunched(): Exception while launching the application: {e.Message}";
            Console.WriteLine(msgText);
            Debug.Write(msgText);
        }
    }
}
