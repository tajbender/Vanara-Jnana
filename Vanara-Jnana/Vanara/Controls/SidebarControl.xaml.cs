using ClassicSamplesBrowser.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace ClassicSamplesBrowser.Vanara.Controls;

public sealed partial class SidebarControl : UserControl
{
    public enum SidebarDockMode
    {
        Default, // Use the default docking behavior (Stick to Edge. Outer: Navigation. Inner: Content)
        Left,
        Right
    }
    public enum FloatMode
    {
        Default, // Use the default floating behavior, Use threshold to determine when to float (e.g., when the window is too narrow)
        Always,
        Never,
    }
    public SidebarDockMode ChildDockMode { get; set; } = SidebarDockMode.Default;
    public FloatMode ChildFloatMode { get; set; } = FloatMode.Default;
    public SidebarControl()
    {
        // TODO: Implement logic to determine docking and floating behavior based on ChildDockMode and ChildFloatMode properties.
        // TODO: Ensure all Resources are properly defined and accessible.
        InitializeComponent();
        Loaded += SidebarControl_Loaded;
    }
    private void SidebarControl_Loaded(object sender, RoutedEventArgs e)
    {
//        foreach (var child in SidebarPanel.Children)
//        {
//            if (child is ToggleButton btn)
//                btn.Click += (s, e2) => NavigationService.TryNavigate(btn);
//        }
    }
}
