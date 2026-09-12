using System;
using Windows.Foundation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Jnana.Controls;

public class GaugeControlViewModel
{
    private double _minimum = 0;
    private double _maximum = 100;
    private double _percent;
    private double _value;

    public GaugeControlViewModel(double value)
    {
        Value = value;
    }

    public Point ArcEndPoint => CalculateArcPoint(Percent * 1.8 - 90);
    public bool IsLargeArc => Percent > 50;

    public double Minimum
    {
        get => _minimum;
        set => _minimum = value;
    }

    public double Maximum
    {
        get => _maximum;
        set => _maximum = value;
    }

    public double NeedleAngle => Percent * 1.8 - 90; // 0% = -90° (left), 100% = +90° (right)

    public double Percent
    {
        get => _percent;
        set => _percent = value;
    } // 0–100

    public double Value
    {
        get => _value;
        set => _value = value;
    }

    private static Point CalculateArcPoint(double v)
    {
        return new Point(100, 100);
    }
}


public sealed partial class GaugeControl : UserControl
{
    public PathGeometry ProgressArc => CalculateProgressArc();

    public GaugeControlViewModel ViewModel;

    public GaugeControl()
    {
        //InitializeComponent();
        //DataContext = new GaugeControlViewModel();
        ViewModel = (GaugeControlViewModel)DataContext;
    }

    private PathGeometry CalculateProgressArc()
    {
        if (ViewModel is not GaugeControlViewModel vm)
            return new PathGeometry();
        double startAngle = -90; // Start at the top
        double endAngle = vm.NeedleAngle; // End angle based on the percentage
        // Convert angles to radians
        double startRadians = startAngle * (Math.PI / 180);
        double endRadians = endAngle * (Math.PI / 180);
        // Calculate the start and end points of the arc
        double radius = 100; // Assuming a radius of 100 for the gauge
        Point startPoint = new Point(
            100 + radius * Math.Cos(startRadians),
            100 + radius * Math.Sin(startRadians)
        );
        Point endPoint = new Point(
            100 + radius * Math.Cos(endRadians),
            100 + radius * Math.Sin(endRadians)
        );
        bool isLargeArc = vm.IsLargeArc;
        // Create the arc segment
        ArcSegment arcSegment = new ArcSegment
        {
            Point = endPoint,
            Size = new Size(radius, radius),
            IsLargeArc = isLargeArc,
            SweepDirection = SweepDirection.Clockwise
        };
        // Create the path figure
        PathFigure pathFigure = new PathFigure
        {
            StartPoint = startPoint,
            Segments = { arcSegment }
        };
        // Create the path geometry
        PathGeometry pathGeometry = new PathGeometry();
        pathGeometry.Figures.Add(pathFigure);

        return pathGeometry;
    }
}
