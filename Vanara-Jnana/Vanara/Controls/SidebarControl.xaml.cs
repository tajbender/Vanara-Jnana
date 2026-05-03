using ClassicSamplesBrowser.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClassicSamplesBrowser.Vanara.Controls;

public sealed partial class SidebarControl : UserControl
{
    public SidebarControl()
    {
        InitializeComponent();
        Loaded += SidebarControl_Loaded;
    }

    private void SidebarControl_Loaded(object sender, RoutedEventArgs e)
    {
        foreach (var child in SidebarPanel.Children)
        {
            if (child is ToggleButton btn)
                btn.Click += (s, e2) => NavigationService.Navigate(btn.Tag.ToString());
        }
    }
}
