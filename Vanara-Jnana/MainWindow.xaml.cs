using ClassicSamplesBrowser.Helpers;
using ClassicSamplesBrowser.Views;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System.Runtime.InteropServices;
using WinRT;

namespace ClassicSamplesBrowser;

public sealed partial class MainWindow : Window
{
    private WindowsSystemDispatcherQueueHelper _wsdqHelper;
    private MicaController _micaController;
    private SystemBackdropConfiguration _backdropConfig;

    //private Size _initialWindowSize = new Size() { Width = 800, Height = 600 };

    public MainWindow()
    {
        InitializeComponent();
        TrySetMicaBackdrop();

        //var initialSize = ApplicationData.Current.LocalSettings.Values["InitialWindowSize"] as string;
        //this.AppWindow.Size = _initialWindowSize;
        // AppWindow.Size = new Size() { Width = 800, Height = 600 };

        RootFrame.Navigate(typeof(StartPage));
    }

//    private void OnIconPressed(object sender, PointerRoutedEventArgs e)
//    {
//        // Show the system menu when the icon is pressed
//        var ptrPointer = e.Pointer;
//
//        ShowSystemMenu();
//    }
//
    public void ShowSystemMenu() => ShowSystemMenu(target: this, uFlags: 0x0000, bRevert: false);

    public static void ShowSystemMenu(object target, uint uFlags = 0x0000, bool bRevert = false)
    {
        // Get the window handle (HWND) of the current window using Vanara interop
        // TODO: Add support for right-clicking the title bar to show the system menu, and for showing the system menu at the cursor position instead of the top-left corner of the window
        // TODO: Handle exceptions that may occur when calling the Win32 API functions, such as if the window handle is invalid or if the system menu cannot be retrieved or displayed
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
        var menu = GetSystemMenu(hwnd, bRevert);

        var point = new Windows.Graphics.PointInt32(0, 0);
        //PInvoke.GetCursorPos(out point);

        TrackPopupMenu(menu, uFlags, point.X, point.Y, 0, hwnd, IntPtr.Zero);
        return;

        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        static extern bool TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y,
            int nReserved, IntPtr hWnd, IntPtr prcRect);
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args)
    {
        _backdropConfig.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
    }

    private bool TrySetMicaBackdrop()
    {
        if (!MicaController.IsSupported())
            return false;

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
}
