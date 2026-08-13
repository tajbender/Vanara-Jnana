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

public sealed partial class FloatingStatusBar : UserControl, IStatusService
{
    private readonly DispatcherTimer _hideTimer = new() { Interval = TimeSpan.FromSeconds(3) };

    //public static FloatingStatusBar Instance { get; private set; }

    private string _cpuUsage = "calculating...";
    private string _netUsage = "calculating...";
    private string _ramUsage = "calculating...";
    private string _diskUsage = "calculating...";

    public new double Opacity
    {
        get => ((UserControl)this).Opacity;
        set => ((UserControl)this).Opacity = value;
    }

    public string CPUUsage
    {
        get => this._cpuUsage;
        set => this._cpuUsage = value;
    }

    public string NetUsage
    {
        get => this._netUsage;
        set => this._netUsage = value;
    }

    public string RAMUsage
    {
        get => this._ramUsage;
        set => this._ramUsage = value;
    }

    public string DiskUsage
    {
        get => this._diskUsage;
        set => this._diskUsage = value;
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

    public void OnAreaMenuTapped(object sender, RoutedEventArgs e)
    {

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
