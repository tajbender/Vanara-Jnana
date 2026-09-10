using System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace Jnana.Controls.Helpers;

/// <summary>
/// A value converter that converts a status string to a corresponding color brush.
/// </summary>
public class StatusColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var status = value?.ToString()?.ToLowerInvariant() ?? string.Empty;

        return status switch
        {
            "online" or "operational" => new SolidColorBrush(Colors.LimeGreen),
            "warning" or "degraded" => new SolidColorBrush(Colors.Gold),
            "error" or "outage" => new SolidColorBrush(Colors.IndianRed),
            "pending" or "unknown" => new SolidColorBrush(Colors.Gray),
            _ => new SolidColorBrush(Colors.LightGray)
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
        => throw new NotImplementedException();
}
