using Jnana.Core.Navigation;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Diagnostics;

namespace Jnana;

/// <summary>
/// 
///         <!-- INFO: TitleBar
///               - https://github.com/CommunityToolkit/Labs-Windows/discussions/454 
///               - https://learn.microsoft.com/en-us/dotnet/api/communitytoolkit.winui.ui.titlebarextensions?view=win-comm-toolkit-dotnet-7.0
///        see also:
///               TitleBarExtensions Class - `CommunityToolkit.WinUI.UI v7.0.3` -->
/// 
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly NavigationService _navigationService;
    private SystemBackdropConfiguration _configuration;
    private MicaController _micaController;
    public string TitleText { get; set; } = "Vanara jñāna";
    public string SubtitleText { get; set; } = "Workbench";
    public bool IsBackButtonVisible { get; set; } = true;
    public bool IsBackButtonEnabled { get; set; } = false;

    public MainWindow(MicaController micaController, SystemBackdropConfiguration configuration)
    {
        this._micaController = micaController;
        this._configuration = configuration;
        this.InitializeComponent();
        this.TrySetMicaBackdrop();

        // TODO: Set correct title bar colors based on the current theme and set drag area
        this.ExtendsContentIntoTitleBar = true;
        this.SetTitleBar(this.AppTitleBar);

        this._navigationService = new NavigationService();
        // TODO: Restore Navigation handling when NavigationService is implemented
        //_navigationService.OnPageNavigated += (sender, e) => NavigationHost.ShowPage(e.PageInstance);

        var workbench = new WorkbenchPage();
        //TODO: NavigationHost<WorkbenchPage>.ShowPage(workbench);
        this._navigationService.Navigate(typeof(WorkbenchPage));
    }
    
    private void QuickLaunchBar_PageRequested(object arg1, Type arg2, RoutedEventArgs arg3)
    {
        Debug.WriteLine($"QuickLaunchBar_PageRequested: {arg2.Name}");
    }

    private void TrySetMicaBackdrop()
    {
        this._configuration = new SystemBackdropConfiguration
        {
            IsInputActive = true,
            Theme = SystemBackdropTheme.Default
        };

        //        this._micaController = new MicaController();
        // Todo: this fails:
        //   _micaController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
        //        this._micaController.SetSystemBackdropConfiguration(this._configuration);
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Minimize window using AppWindow API
    }

    /// <summary>
    /// Toggle the hosting window between full-screen and default presentation modes.
    /// </summary>
    /// <remarks>Obtains the native window handle, retrieves the AppWindow for the window, and switches its
    /// Presenter between FullScreen and Default.</remarks>
    /// <param name="sender">The source of the click event.</param>
    /// <param name="e">Event arguments for the routed click event.</param>
    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        if (appWindow.Presenter.Kind == AppWindowPresenterKind.FullScreen)
            appWindow.SetPresenter(AppWindowPresenterKind.Default);
        else
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
    }

    /// <summary>
    /// Closes the window.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The routed event data.</param>
    private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
}
