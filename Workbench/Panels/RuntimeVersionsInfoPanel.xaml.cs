using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Reflection;
using Windows.ApplicationModel;

namespace Jnana.Workbench.Panels;

public sealed partial class RuntimeVersionsInfoPanel : UserControl
{
    public static readonly DependencyProperty AppVersionInfoProperty =
        DependencyProperty.Register(
            nameof(AppVersionInfo),
            typeof(string),
            typeof(RuntimeVersionsInfoPanel),
            new PropertyMetadata(string.Empty));

    private string AppVersionInfo
    {
        get => (string)GetValue(AppVersionInfoProperty);
        set => SetValue(AppVersionInfoProperty, value);
    }

    public static readonly DependencyProperty VanaraCoreVersionInfoProperty =
        DependencyProperty.Register(
            nameof(VanaraCoreVersionInfo),
            typeof(string),
            typeof(RuntimeVersionsInfoPanel),
            new PropertyMetadata(string.Empty));

    private string VanaraCoreVersionInfo
    {
        get => (string)GetValue(VanaraCoreVersionInfoProperty);
        set => SetValue(VanaraCoreVersionInfoProperty, value);
    }

    public RuntimeVersionsInfoPanel()
    {
        this.InitializeComponent();

        this.AppVersionInfo = GetAppVersion();
        this.VanaraCoreVersionInfo = GetVanaraCoreVersion();
    }

    /// <summary>
    /// Gets the application's package version as a dotted string in Major.Minor.Build.Revision format.
    /// </summary>
    /// <remarks>Values are taken from Windows.ApplicationModel.Package.Current.Id.Version and concatenated with
    /// dots.</remarks>
    /// <returns>A string containing the package version formatted as 'Major.Minor.Build.Revision'.</returns>
    public static string GetAppVersion()
    {
        PackageVersion version = Package.Current.Id.Version;

        return
            $"{version.Major}." +
            $"{version.Minor}." +
            $"{version.Build}." +
            $"{version.Revision}";
    }

    /// <summary>
    /// Gets the Vanara.PInvoke.Kernel32 assembly version.
    /// </summary>
    /// <remarks>Obtains Version from the assembly's AssemblyName and converts it to a string; returns
    /// "Unknown" when Version is null.</remarks>
    /// <returns>The assembly version as a string, or "Unknown" if the version cannot be determined.</returns>
    public static string GetVanaraCoreVersion()
    {
        Version? version =
            typeof(Vanara.PInvoke.Kernel32)
                .Assembly
                .GetName()
                .Version;

        return version?.ToString() ?? "Unknown";
    }
}
