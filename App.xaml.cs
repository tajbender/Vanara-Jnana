using Jnana.Core.Services;
using Microsoft.UI.Xaml;

namespace Jnana;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private Window? _window;

    public static AppServiceHost Services { get; private set; } = new AppServiceHost();

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();
        _window.Activate();

        // navigate initially or from command line arguments
        // TODO: await Services.InitializeAsync(@"C:\Dev\MyProject\MyProject.csproj");
    }
}
