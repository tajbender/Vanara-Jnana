using Jnana.Helpers;
using Jnana.Services;
using Jnana.Views;
using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System.Diagnostics;
using Vanara.PInvoke;
using Windows.Graphics;
using WinRT;
using WinRT.Interop;

namespace Jnana;

public sealed partial class MainWindow : Window
{
    private SystemBackdropConfiguration _backdropConfig;
    private RectInt32 _defaultBounds = new(320, 256, 1024, 768);
    private DockingService _docking;
    private MicaController _micaController;
    private WindowsSystemDispatcherQueueHelper _wsdqHelper;

    public MainWindow(DockingService? docking = null)
    {
        InitializeComponent();
        _docking = docking ??= new DockingService(this);

        SetWindowBounds(_defaultBounds);
        TrySetMicaBackdrop();
        CreateInspectorPanel(IsVisible: false);

        // Navigate to the ShellPage when the MainWindow is initialized
        _ = RootFrame.Navigate(typeof(ShellPage));

        // _navigationService = new NavigationService(RootFrame);
        // var initialSize = ApplicationData.Current.LocalSettings.Values["InitialWindowSize"] as string;
        // this.AppWindow.Size = _initialWindowSize;
        // AppWindow.Size = new Size() { Width = 800, Height = 600 };
        // TODO:  this.SetTitleBar(StartPage.DragRegion);

    }

    public void ShowSystemMenu() => ShowSystemMenu(windowHandleTargetObject: this, uFlags: 0x0000, bGetSystemMenuRevert: false);

    /// <summary>
    /// Shows the system menu of the specified window.
    /// TODO: Move to Vanara.WinUI3.Interop and make it an extension method for Window.
    /// </summary>
    /// <param name="windowHandleTargetObject"></param>
    /// <param name="uFlags">TODO</param>
    /// <param name="bGetSystemMenuRevert">The action to be taken. If this parameter is FALSE, GetSystemMenu returns a handle to the copy of the window menu currently in use.
    /// The copy is initially identical to the window menu, but it can be modified. If this parameter is TRUE, GetSystemMenu resets the window menu back to the default state.
    /// The previous window menu, if  any, is destroyed.</param>
    public static void ShowSystemMenu(object windowHandleTargetObject, uint uFlags = 0x0000, bool bGetSystemMenuRevert = false)
    {
        // TODO: Add support for right-clicking the title bar to show the system menu, and for showing the system menu at the cursor position instead of the top-left corner of the window
        // TODO: Handle exceptions that may occur when calling the Win32 API functions, such as if the window handle is invalid or if the system menu cannot be retrieved or displayed
        var hwnd = WindowNative.GetWindowHandle(windowHandleTargetObject);
        var menu = User32.GetSystemMenu(hwnd, bGetSystemMenuRevert);
        var point = new PointInt32(0, 0);
        User32.TrackPopupMenuFlags tpopMenuFlags = User32.TrackPopupMenuFlags.TPM_LEFTBUTTON;
        User32.TrackPopupMenu(menu, tpopMenuFlags, point.X, point.Y, 0, hwnd);
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args)
    {
        Debug.WriteLine($"MainWindow.Window_Activated( WindowWindowActivationState: {args.WindowActivationState} )");
        _backdropConfig.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
    }

    /// <summary>Tries to set the Mica backdrop for the window. This method checks if Mica is supported on the current system, 
    /// and if so, it initializes the necessary components to apply the Mica effect to the window's background.</summary>
    /// <returns>True if the Mica backdrop was successfully set. False otherwise.</returns>
    private bool TrySetMicaBackdrop()
    {
        if (!MicaController.IsSupported())
            return false;

        try
        {
            _wsdqHelper = new WindowsSystemDispatcherQueueHelper();
            _wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

            _backdropConfig = new SystemBackdropConfiguration
            {
                IsInputActive = true,
                Theme = SystemBackdropTheme.Default
            };

            _micaController = new MicaController
            {
                Kind = MicaKind.BaseAlt
            };

            _micaController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
            _micaController.SetSystemBackdropConfiguration(_backdropConfig);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MainWindow.TrySetMicaBackdrop Failed to set Mica backdrop: {ex.Message}");
            return false;
        }
    }

    /// <summary>Sets the window bounds to the specified rectangle.</summary>
    /// <param name="bounds">The desired bounds for the window.</param>
    public void SetWindowBounds(RectInt32 bounds)
    {
        try
        {
            var hwnd = WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            // TODO: 10-07-26 var appWindow = AppWindow.GetFromWindowId(windowId);
            // TODO: 10-07-26 appWindow.MoveAndResize(bounds);
            // TODO: OnNotifyPropertyChanged("WindowBounds"); Store to configuration file or settings for persistence across sessions.
        }
        catch
        {
            Debug.Fail($"MainWindow.SetWindowBounds Failed to set window bounds to {bounds}.");
            throw;
        }
    }

    /// <summary>
    /// Handles the event when the window icon is pressed.
    /// This method shows the system menu for the window when the icon is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnIconPressed(object sender, PointerRoutedEventArgs e)
    {
        // Show the system menu when the icon is pressed
        var ptrPointer = e.Pointer;

        ShowSystemMenu();
    }

    #region DockingService stuff
    private void CreateInspectorPanel(DockingService? docking = null, string? title = null, bool IsVisible = true)
    {
        docking ??= this._docking;
        Debug.Assert(docking != null, "DockingService is not initialized.");

        AppWindow inspectorDockPanel = docking.CreateDockPanel("Inspector", DockingService.DockPosition.RightOfScreen, 420);
        inspectorDockPanel.Title = title ?? "Inspector dock panel"; // TODO: i18n
        if (IsVisible)
        {
            inspectorDockPanel.Show();
        }

        // TODO: Show the Panel beside the main window, not as a child of the main window.
        // This will allow the panel to be moved independently of the main window and will
        // also allow it to be shown on a different monitor if desired.
        //
        // TODO: Optional: XAML-Content setzen
        //        var xamlRoot = inspector.XamlRoot;
        //        var frame = new Frame();
        //        frame.Navigate(typeof(Views.InspectorView));
        //        inspector.SetTitleBarVisibility(AppWindowTitleBarVisibility.Hidden);
    }
    #endregion DockingService stuff
}
