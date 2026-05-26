using ClassicSamplesBrowser.Vanara.Reflection;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ClassicSamplesBrowser.Views;

public sealed partial class ApiExplorerPage : Page
{
    internal TypeInfo CurrentTypeInfo { get; }
    public string DisplayName => "ApiExplorerPage.DisplayName";
    internal IEnumerable<IElementInfo> Members { get; } = [];
    internal ApiExplorerPage(Type targetType) : this(TypeInfo.MakeType(type: targetType)) { }
    internal ApiExplorerPage(TypeInfo targetType)
    {
        this.InitializeComponent();
        CurrentTypeInfo = targetType;
        Members = targetType.Children;
    }
}
