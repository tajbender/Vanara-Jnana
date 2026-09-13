using Windows.Foundation;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Controls;

public sealed partial class GaugeControl : UserControl
{
    public GaugeControl()
    {
        InitializeComponent();
        DataContext = new GaugeControlViewModel();
    }
}

public class GaugeControlViewModel
{
    public Point ArcEndPoint => CalculateArcPoint(Percent * 1.8 - 90);
    public bool IsLargeArc => Percent > 50;
    public double Minimum { get; set; } = 0;
    public double Maximum { get; set; } = 100;
    public double NeedleAngle => Percent * 1.8 - 90; // 0% = -90° (links), 100% = +90° (rechts)
    public double Percent { get; set; } // 0–100
    public double Value { get; set; }

    private Point CalculateArcPoint(double v)
    {
        return new Point(100, 100);
    }
}