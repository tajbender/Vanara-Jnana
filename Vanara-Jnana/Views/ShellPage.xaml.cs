using ClassicSamplesBrowser.ViewModels;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System;
using Windows.Foundation.Collections;
using Windows.Foundation;

namespace ClassicSamplesBrowser.Views;

public sealed partial class ShellPage : Page
{
    static readonly CancellationToken CancellationToken = CancellationToken.None;
    private NuGetViewModel NuGetVM { get; }
    private GitHubViewModel GitHubVM { get; }
    private SamplesViewModel SamplesVM { get; }
    public ShellPage()
    {
        InitializeComponent();
        NuGetVM = new NuGetViewModel();
        GitHubVM = new GitHubViewModel();
        SamplesVM = new SamplesViewModel();
    }
}
