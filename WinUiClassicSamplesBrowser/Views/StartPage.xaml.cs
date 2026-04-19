using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace ClassicSamplesBrowser.Views;

/// <summary><completionlist cref="=StartPage"></completionlist>
/// StartPage is the main page that is shown when the app is launched
/// and serves as a navigation hub for the various samples in the app.
/// </summary>
public sealed partial class StartPage : Page
{
    public StartPage()
    {
        InitializeComponent();
        LoadVersions();
    }

    private async void LoadVersions()
    {
        // TODO: Replace with David's NuGet API helper
        var versions = new List<string>
            {
                "Latest Version: 5.0.4",
                "5.0.4",
                "5.0.3",
                "5.0.2",
                "5.0.1",
                "5.0.0",
            };
    }

    private void OpenExplorer_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(ApiExplorerPage));
    }

    private void OpenSamples_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(SamplesPage));
    }

    private void LoadAssemblies_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Hook into David's Assembly Loader
    }
}
