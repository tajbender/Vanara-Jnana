using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ClassicSamplesBrowser.Vanara.Controls;

public sealed partial class FloatingStatusBar : UserControl
{
    private readonly DispatcherTimer _hideTimer = new() { Interval = TimeSpan.FromSeconds(3) };

    public FloatingStatusBar()
    {
        //InitializeComponent();
        _hideTimer.Tick += (_, __) => Hide();
    }

    public void Show(string message, string icon = "\uE946")
    {
        MessageElement.Text = message;
        IconElement.Glyph = icon;

        ControlRoot.Opacity = 1;
        ControlRoot.Translation = new System.Numerics.Vector3(0, 0, 0);

        _hideTimer.Stop();
        _hideTimer.Start();
    }

    public void Hide()
    {
        ControlRoot.Opacity = 0;
        ControlRoot.Translation = new System.Numerics.Vector3(0, 20, 0);
    }
}
