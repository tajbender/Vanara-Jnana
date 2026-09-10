using Jnana.Core.Navigation;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;

namespace Jnana;

/// INFO: TitleBar
/// - https://github.com/CommunityToolkit/Labs-Windows/discussions/454 
/// -
/// https://learn.microsoft.com/en-us/dotnet/api/communitytoolkit.winui.ui.titlebarextensions?view=win-comm-toolkit-dotnet-7.0
/// see also:
/// TitleBarExtensions Class - `CommunityToolkit.WinUI.UI`
public sealed partial class MainWindow : Window
{
    public SystemBackdropConfiguration SysBackdropConfiguration;

    public MainWindow(MicaController micaController, SystemBackdropConfiguration configuration)
    {
        Controller = micaController;
        SysBackdropConfiguration = configuration;
        InitializeComponent();
        TrySetMicaBackdrop();

        NavigationService = new NavigationService();
        // TODO: Restore Navigation handling when NavigationService is implemented
        //_navigationService.OnPageNavigated += (sender, e) => NavigationHost.ShowPage(e.PageInstance);

        var workbench = new WorkbenchPage();
        NavigationHost.ShowPage(workbench);
        NavigationService.Navigate(typeof(WorkbenchPage));
    }

    public MicaController Controller { get; }

    public NavigationService NavigationService { get; }

    /// <summary>
    ///     Public properties for the title, subtitle, and back button visibility of the main window.
    /// </summary>
    public string TitleText { get; set; } = "Vanara Jñāna";

    public string SubtitleText { get; set; } = "Workbench";
    public bool IsBackButtonVisible { get; set; } = true;
    public bool IsBackButtonEnabled { get; set; } = false;

    private void TrySetMicaBackdrop()
    {
        SysBackdropConfiguration = new SystemBackdropConfiguration
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