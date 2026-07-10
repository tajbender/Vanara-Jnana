using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;

namespace Jnana.Services;

public class DockingService
{
    /// <summary>Represents the position where a window can be docked.</summary>
    public enum DockPosition
    {
        Left,
        Right,
        Bottom
    }

    private readonly Window _mainWindow;
    private readonly AppWindow _mainAppWindow;

    public DockingService(Window mainWindow)
    {
        _mainWindow = mainWindow;
        _mainAppWindow = GetAppWindow(mainWindow);
    }

    private AppWindow GetAppWindow(Window window)
    {
        var hwnd = WindowNative.GetWindowHandle(window);
        var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
        return AppWindow.GetFromWindowId(windowId);
    }

    // ------------------------------------------------------------
    //  PUBLIC API
    // ------------------------------------------------------------

    public AppWindow CreateDockPanel(string name, DockPosition position, int size = 400)
    {
        var appWindow = AppWindow.Create();
        appWindow.Title = name;

        // Presenter: CompactOverlay = Docking-Look
        var presenter = CompactOverlayPresenter.Create();
        appWindow.SetPresenter(presenter);

        DockTo(appWindow, position, size);

        return appWindow;
    }

    public void DockTo(AppWindow window, DockPosition position, int size)
    {
        var displayArea = DisplayArea.GetFromWindowId(_mainAppWindow.Id, DisplayAreaFallback.Primary);
        var workArea = displayArea.WorkArea;

        RectInt32 rect;

        switch (position)
        {
            case DockPosition.Right:
                rect = new RectInt32(
                    workArea.X + workArea.Width - size,
                    workArea.Y,
                    size,
                    workArea.Height);
                break;

            case DockPosition.Left:
                rect = new RectInt32(
                    workArea.X,
                    workArea.Y,
                    size,
                    workArea.Height);
                break;

            case DockPosition.Bottom:
                rect = new RectInt32(
                    workArea.X,
                    workArea.Y + workArea.Height - size,
                    workArea.Width,
                    size);
                break;

            default:
                rect = new RectInt32(
                    workArea.X,
                    workArea.Y,
                    size,
                    workArea.Height);
                break;
        }

        window.MoveAndResize(rect);
    }
}
