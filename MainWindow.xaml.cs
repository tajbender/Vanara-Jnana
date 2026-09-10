using Jnana.Core.Navigation;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;

namespace Jnana;

/// INFO: TitleBar
///   - https://github.com/CommunityToolkit/Labs-Windows/discussions/454 
///   - https://learn.microsoft.com/en-us/dotnet/api/communitytoolkit.winui.ui.titlebarextensions?view=win-comm-toolkit-dotnet-7.0
/// see also:
///   TitleBarExtensions Class - `CommunityToolkit.WinUI.UI`

public sealed partial class MainWindow : Window
{
    private MicaController _micaController;
    private SystemBackdropConfiguration _configuration;

    public NavigationService NavigationService { get; }

    /// <summary>
    /// Public properties for the title, subtitle, and back button visibility of the main window.
    /// </summary>
    public string TitleText { get; set; } = "Vanara Jñāna";
    public string SubtitleText { get; set; } = "Workbench";
    public bool IsBackButtonVisible { get; set; } = true;
    public bool IsBackButtonEnabled { get; set; } = false;

    public MainWindow(MicaController micaController, SystemBackdropConfiguration configuration)
    {
        this._micaController = micaController;
        this._configuration = configuration;
        this.InitializeComponent();
        this.TrySetMicaBackdrop();

        this.NavigationService = new NavigationService();
        // TODO: Restore Navigation handling when NavigationService is implemented
        //_navigationService.OnPageNavigated += (sender, e) => NavigationHost.ShowPage(e.PageInstance);

        var workbench = new WorkbenchPage();
        this.NavigationHost.ShowPage(workbench);
        this.NavigationService.Navigate(typeof(WorkbenchPage));
    }

    private void TrySetMicaBackdrop()
    {
        this._configuration = new SystemBackdropConfiguration
        {
            IsInputActive = true,
            Theme = SystemBackdropTheme.Default
        };

//        this._micaController = new MicaController();
        // Todo: this fails:
        //   _micaController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
//        this._micaController.SetSystemBackdropConfiguration(this._configuration);
    }
}
