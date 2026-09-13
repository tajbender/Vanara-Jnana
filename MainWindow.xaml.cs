using Jnana.Core.Navigation;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;

namespace Jnana;

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
    var navHost = new NavigationHost();
    navHost.ShowPage(workbench);
    _navigation_service?.Navigate(typeof(WorkbenchPage));
}

private void InitializeComponent()
{
    // XAML-generated InitializeComponent stub for build-time.
}

    /// <summary>
    ///     Public properties for the title, subtitle, and back button visibility of the main window.
    /// </summary>
    public string TitleText { get; set; } = "Vanara Jñāna";

    public string SubtitleText { get; set; } = "Workbench";
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