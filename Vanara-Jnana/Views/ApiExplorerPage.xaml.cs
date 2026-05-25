using Microsoft.UI.Xaml.Controls;
using System;

namespace ClassicSamplesBrowser.Views;

public sealed partial class ApiExplorerPage : Page
{
    public Type TargetType { get; }

    public ApiExplorerPage(Type targetType)
    {
        this.InitializeComponent();
        TargetType = targetType;
    }
}
