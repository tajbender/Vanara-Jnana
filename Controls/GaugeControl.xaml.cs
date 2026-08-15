using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Jnana.Controls;

public sealed partial class GaugeControl : UserControl
{
    public GaugeControl()
    {
        InitializeComponent();
        this.DataContext = new GaugeControlViewModel();
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
