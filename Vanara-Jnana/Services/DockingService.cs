using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using Windows.Graphics;
using WinRT.Interop;

namespace Jnana.Services;

/// <summary>
/// Provides functionality to create and manage dockable windows in a WinUI 3 application.
/// </summary>
public class DockingService
{
    /// <summary>Represents the position where a window can be docked.</summary>
    public enum DockPosition
    {
        TopOfScreen,
        LeftOfScreen,
        RightOfScreen,
        BottomOfScreen,
        FreeFloating,
        AbsoluteFloating,
        Fullscreen,
        //MainWindow,
    }

    private readonly Window _mainWindow;
    private readonly AppWindow _mainAppWindow;

    public DockingService(Window ownerWindow)
    {
        _mainWindow = ownerWindow;
        _mainAppWindow = GetAppWindowFromWindowInstance(ownerWindow);
    }

    /// <summary>
    ///  Gets the AppWindow instance associated with the specified Window instance.
    ///  <br/>
    ///  - TODO. Move to Vanara.WinUI3.Interop and make it an extension method for Window.<br/>
    ///  <br/>
    ///  - TODO: Consider caching the AppWindow instance to avoid repeated calls to GetAppWindowFromWindowInstance.<br/>
    ///  - TODO: Consider adding error handling for cases where the Window instance is not valid or the AppWindow cannot be retrieved.<br/>
    ///  - TODO: Consider adding logging to track when the AppWindow is retrieved and any issues that occur.<br/>
    ///  - TODO: Consider adding unit tests to verify the behavior of GetAppWindowFromWindowInstance with different Window instances and scenarios.<br/>
    /// </summary>
    /// <param name="window"></param>
    /// <returns></returns>
    private static AppWindow GetAppWindowFromWindowInstance(Window window)
    {
        ArgumentNullException.ThrowIfNull(window);

        try
        {
            var hwnd = WindowNative.GetWindowHandle(window);
            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            return AppWindow.GetFromWindowId(windowId);
        }
        catch
        {
            Debug.Fail($"GetAppWindowFromWindowInstance() Failed to get AppWindow from {window}.");
            throw;
        }
    }

    // ------------------------------------------------------------
    //  PUBLIC API
    // ------------------------------------------------------------

    public AppWindow CreateDockPanel(string name, DockPosition position, int size = 320)
    {
        try
        {
            var appWindow = AppWindow.Create();
            appWindow.Title = name;

            // Presenter: CompactOverlay = Docking-Look
            var presenter = CompactOverlayPresenter.Create();
            appWindow.SetPresenter(presenter);

            DockTo(appWindow, position, size);

            return appWindow;
        }
        catch (Exception ex)
        {
            Debug.Fail($"CreateDockPanel Failed to create dock panel '{name}' Position {position} Size {size}: {ex.Message}");
            throw;
        }
    }

    public void DockTo(AppWindow window, DockPosition position, int size)
    {
        try
        {
            var displayArea = DisplayArea.GetFromWindowId(_mainAppWindow.Id, DisplayAreaFallback.Primary);
            var moveAndResizeRect = new RectInt32();
            var workArea = displayArea.WorkArea;

            switch (position)
            {
                case DockPosition.RightOfScreen:
                    moveAndResizeRect = new RectInt32(
                        workArea.X + workArea.Width - size,
                        workArea.Y,
                        size,
                        workArea.Height);
                    break;

                case DockPosition.LeftOfScreen:
                    moveAndResizeRect = new RectInt32(
                        workArea.X,
                        workArea.Y,
                        size,
                        workArea.Height);
                    break;

                case DockPosition.BottomOfScreen:
                    moveAndResizeRect = new RectInt32(
                        workArea.X,
                        workArea.Y + workArea.Height - size,
                        workArea.Width,
                        size);
                    break;
                case DockPosition.TopOfScreen:
                    moveAndResizeRect = new RectInt32(
                        workArea.X,
                        workArea.Y,
                        workArea.Width,
                        size);
                    break;
                case DockPosition.FreeFloating:
                    break;
                case DockPosition.AbsoluteFloating:
                    break;
                case DockPosition.Fullscreen:
                    break;
                default:
                    moveAndResizeRect = new RectInt32(
                        workArea.X,
                        workArea.Y,
                        size,
                        workArea.Height);
                    break;
            }

            window.MoveAndResize(moveAndResizeRect);
        }
        catch (Exception ex)
        {
            Debug.Fail($"Failed to dock window: {ex.Message}");
            throw;
        }
    }
}
