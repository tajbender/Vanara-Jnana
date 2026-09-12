using System;
using Windows.Foundation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Jnana.Controls;

public class GaugeControlViewModel
{
    public GaugeControlViewModel(double value)
    {
        Value = value;
    }

    public Point ArcEndPoint => CalculateArcPoint(Percent * 1.8 - 90);
    public bool IsLargeArc => Percent > 50;

    public double Minimum { get; set; }

    public double Maximum { get; set; } = 100;

    public double NeedleAngle => Percent * 1.8 - 90; // 0% = -90° (left), 100% = +90° (right)

    public double Percent { get; set; } // 0–100

    public double Value { get; set; }

    private static Point CalculateArcPoint(double v)
    {
        return new Point(100, 100);
    }
}

public sealed partial class GaugeControl : UserControl
{
    public GaugeControlViewModel ViewModel;

    public GaugeControl()
    {
        //InitializeComponent();
        //DataContext = new GaugeControlViewModel();
        ViewModel = (GaugeControlViewModel)DataContext;
    }

    public PathGeometry ProgressArc => CalculateProgressArc();

    private PathGeometry CalculateProgressArc()
    {
        if (ViewModel is not GaugeControlViewModel vm)
            return new PathGeometry();
        double startAngle = -90; // Start at the top
        var endAngle = vm.NeedleAngle; // End angle based on the percentage
        // Convert angles to radians
        var startRadians = startAngle * (Math.PI / 180);
        var endRadians = endAngle * (Math.PI / 180);
        // Calculate the start and end points of the arc
        double radius = 100; // Assuming a radius of 100 for the gauge
        var startPoint = new Point(
            100 + radius * Math.Cos(startRadians),
            100 + radius * Math.Sin(startRadians)
        );
        var endPoint = new Point(
            100 + radius * Math.Cos(endRadians),
            100 + radius * Math.Sin(endRadians)
        );
        var isLargeArc = vm.IsLargeArc;
        // Create the arc segment
        var arcSegment = new ArcSegment
        {
            Point = endPoint,
            Size = new Size(radius, radius),
            IsLargeArc = isLargeArc,
            SweepDirection = SweepDirection.Clockwise
        };
        // Create the path figure
        var pathFigure = new PathFigure
        {
            StartPoint = startPoint,
            Segments = { arcSegment }
        };
        // Create the path geometry
        var pathGeometry = new PathGeometry();
        pathGeometry.Figures.Add(pathFigure);

        return pathGeometry;
    }
}