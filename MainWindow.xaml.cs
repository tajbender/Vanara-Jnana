using Jnana.Core.Navigation;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;

namespace Jnana;

/// <summary>
/// 
///         <!-- INFO: TitleBar
///               - https://github.com/CommunityToolkit/Labs-Windows/discussions/454 
///               - https://learn.microsoft.com/en-us/dotnet/api/communitytoolkit.winui.ui.titlebarextensions?view=win-comm-toolkit-dotnet-7.0
///        see also:
///               TitleBarExtensions Class - `CommunityToolkit.WinUI.UI v7.0.3` -->
/// 
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly NavigationService _navigationService;
    private SystemBackdropConfiguration _configuration;
    private MicaController _micaController;

    public MainWindow(MicaController micaController, SystemBackdropConfiguration configuration)
    {
        _micaController = micaController;
        _configuration = configuration;
        InitializeComponent();
        TrySetMicaBackdrop();

        _navigationService = new NavigationService();
        // TODO: Restore Navigation handling when NavigationService is implemented
        //_navigationService.OnPageNavigated += (sender, e) => NavigationHost.ShowPage(e.PageInstance);

        var workbench = new WorkbenchPage();
        //TODO: NavigationHost<WorkbenchPage>.ShowPage(workbench);
        _navigationService.Navigate(typeof(WorkbenchPage));
    }

    /// <summary>
    ///     Public properties for the title, subtitle, and back button visibility of the main window.
    /// </summary>
    public string TitleText { get; set; } = "jñāna";
    public string SubtitleText { get; set; } = "vanara jñāna";
    public bool IsBackButtonVisible { get; set; } = true;
    public bool IsBackButtonEnabled { get; set; } = false;

    private void TrySetMicaBackdrop()
    {
        _configuration = new SystemBackdropConfiguration
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