using Microsoft.UI.Xaml;

namespace Jnana.Controls.Helpers;

/**
 * //
 * <Grid x:Name="ResizeOverlay"
 *     // Background="{ThemeResource AcrylicBackgroundFillColorDefaultBrush}"
 *     // Opacity="0"
 *     // Visibility="Collapsed"
 * /
 * /
 * <Image Source="Assets/AppIcon.png"
 *     // Width="96" Height="96"
 *     // HorizontalAlignment="Center"
 *     // VerticalAlignment="Center"
 *     // Opacity="0.9"
 * *
 */

// 
// INFO: MicaWindowResizeHook.cs
// Use AppWindow.SetIcon() for default Symbol.
// Use MicaController or DesktopAcrylicController for the background.
//    Trigger over Window.Activated + SizeChanged for smooth fade-in/out.
// 
public class MicaWindowResizeHook
{
//    public static AddCompositor(Window window)
//    {
//        var compositor = window.Compositor;
//        var visual = ElementCompositionPreview.GetElementVisual(ResizeOverlay);
//        var blur = compositor.CreateGaussianBlurEffect();
//        // … evtl. Effektgraph aufbauen und animieren
//    }

    private async void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
    {
        //ResizeOverlay.Visibility = Visibility.Visible;
        //ResizeOverlay.Opacity = 1;
        //
        //await Task.Delay(300); // Dauer der Animation
        //ResizeOverlay.Opacity = 0;
        //ResizeOverlay.Visibility = Visibility.Collapsed;
    }
}