using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

namespace Jnana.Workbench.Controls;

public interface IStatusService
{
    void Show(string message, StatusKind kind = StatusKind.Info);
}

public enum StatusKind
{
    Info,
    Success,
    Warning,
    Error,
    NuGet,
    GitHub,
    Assembly
}

public sealed partial class FloatingStatusBar : UserControl
{
    private readonly DispatcherTimer _hideTimer = new() { Interval = TimeSpan.FromSeconds(3) };

    //public static FloatingStatusBar Instance { get; private set; }

    public float Opacity
    {
        get => this.Opacity;
        set => this.Opacity = value;
    }

    public string CPUUsage
    {
        get => CPUUsage;
        set => CPUUsage = value;
    }

    public string NetUsage
    {
        get => NetUsage;
        set => NetUsage = value;
    }

    public string RAMUsage
    {
        get => RAMUsage;
        set => RAMUsage = value;
    }

    public string DiskUsage
    {
        get => DiskUsage;
        set => DiskUsage = value;
    }

    public void OnAreaMenuTapped(object sender, RoutedEventArgs e)
    {

    }


    public FloatingStatusBar()
    {
        InitializeComponent();
        _hideTimer.Tick += (_, __) => Hide();

        // OnAreaMenuTapped
    }

    public void Show(string message, StatusKind kind)
    {
        // TODO: Implement the logic to display the status bar with the given message and kind.
        //            MessageElement.Text = message;
        //            IconElement.Glyph = GetGlyph(kind);
        //            Root.Background = GetBackground(kind);
        //
        //            Root.Opacity = 1;
        //            Root.Translation = new System.Numerics.Vector3(0, 0, 0);
        //
        //            _hideTimer.Stop();
        //            _hideTimer.Start();
    }

    public void Hide()
    {
        // TODO: Implement the logic to hide the status bar.
        //            Root.Opacity = 0;
        //            Root.Translation = new System.Numerics.Vector3(0, 20, 0);
    }

    private string GetGlyph(StatusKind kind) => kind switch
    {
        StatusKind.Success => "\uE73E",
        StatusKind.Warning => "\uE7BA",
        StatusKind.Error => "\uE783",
        StatusKind.NuGet => "\uEBDC",
        StatusKind.GitHub => "\uE8B8",
        StatusKind.Assembly => "\uEC2E",
        _ => "\uE946"
    };

    private Brush GetBackground(StatusKind kind) => kind switch
    {
        StatusKind.Error => new SolidColorBrush(Color.FromArgb(255, 180, 40, 40)),
        StatusKind.Success => new SolidColorBrush(Color.FromArgb(255, 40, 160, 80)),
        StatusKind.Warning => new SolidColorBrush(Color.FromArgb(255, 200, 160, 40)),
        _ => (Brush)Application.Current.Resources["AcrylicBackgroundFillColorDefaultBrush"]
    };
}
