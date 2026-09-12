using Jnana.Core.Navigation;
using Jnana.Workbench.Pages.Workbench;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;

namespace Jnana;

public sealed partial class MainWindow : Window
{
    private MicaController _micaController;
    private SystemBackdropConfiguration _configuration;
    private readonly NavigationService _navigationService;

    /// <summary>
    /// Public properties for the title, subtitle, and back button visibility of the main window.
    /// </summary>
    public string TitleText { get; set; } = "Vanara Jñāna";
    public string SubtitleText { get; set; } = "Workbench";
    public bool IsBackButtonVisible { get; set; } = true;
    public bool IsBackButtonEnabled { get; set; } = false;

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
        NavigationHost.ShowPage(workbench);
        _navigationService.Navigate(typeof(WorkbenchPage));
    }

    private void TrySetMicaBackdrop()
    {
        _configuration = new SystemBackdropConfiguration
        {
            IsInputActive = true,
            Theme = SystemBackdropTheme.Default
        };

        _micaController = new MicaController();
        // Todo: this fails:
        //   _micaController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
        _micaController.SetSystemBackdropConfiguration(_configuration);
    }
}
