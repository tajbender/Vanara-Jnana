using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel;

namespace Jnana.Workbench.Panels;

public sealed partial class SystemStatusInfoPanel : UserControl
{
    public static readonly DependencyProperty AppPackageVersionInfoProperty =
        DependencyProperty.Register(
            nameof(AppPackageVersionInfo),
            typeof(string),
            typeof(SystemStatusInfoPanel),
            new PropertyMetadata(string.Empty));

    private string AppPackageVersionInfo
    {
        get => (string)GetValue(AppPackageVersionInfoProperty);
        set => SetValue(AppPackageVersionInfoProperty, value);
    }

    public static readonly DependencyProperty VanaraCoreVersionInfoProperty =
        DependencyProperty.Register(
            nameof(VanaraCoreVersionInfo),
            typeof(string),
            typeof(SystemStatusInfoPanel),
            new PropertyMetadata(string.Empty));

    private string VanaraCoreVersionInfo
    {
        get => (string)GetValue(VanaraCoreVersionInfoProperty);
        set => SetValue(VanaraCoreVersionInfoProperty, value);
    }

    public SystemStatusInfoPanel()
    {
        this.InitializeComponent();

        this.AppPackageVersionInfo = GetAppPackageVersion();
        this.VanaraCoreVersionInfo = GetVanaraCoreVersion();
    }

    /// <summary>
    /// Gets the application's package version as a dotted string in Major.Minor.Build.Revision format.
    /// </summary>
    /// <remarks>Values are taken from Windows.ApplicationModel.Package.Current.Id.Version and concatenated with
    /// dots.</remarks>
    /// <returns>A string containing the package version formatted as 'Major.Minor.Build.Revision'.</returns>
    public static string GetAppPackageVersion()
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
