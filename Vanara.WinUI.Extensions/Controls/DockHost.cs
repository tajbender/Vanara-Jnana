using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace Vanara.WinUI.Extensions;

public partial class DockHost : FrameworkElement
{
    public IReadOnlyList<DockPanel> Panels { get; }

    public void AddPanel(DockPanel panel) { }
    public void RemovePanel(DockPanel panel) { }

    public void DockTo(DockPanel panel, DockPosition position) { }
    public void Float(DockPanel panel) { }
    public void AutoHide(DockPanel panel) { }

    public DockLayout LoadLayout() { return new DockLayout(); }
    public void SaveLayout(DockLayout layout) { }
}

public class DockPanel : ContentControl
{
    public string Id { get; }
    public string Title { get; set; }
    public IconSource Icon { get; set; }

    public DockState State { get; set; }
    public DockPosition Position { get; set; }

    public void Activate() { }
    public void Close() { }
}

public enum DockState
{
    Docked,
    Floating,
    AutoHide,
    Hidden
}

public enum DockPosition
{
    Left,
    Right,
    Bottom,
    Top,
    Float
}

public class DockLayout
{
    public List<DockPanelLayout> Panels { get; set; }
}

public class DockPanelLayout
{
    public string Id { get; set; }
    public DockState State { get; set; }
    public DockPosition Position { get; set; }
    public Rect Bounds { get; set; }
}
