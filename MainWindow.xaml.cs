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
    private readonly MicaController _controller;
    private readonly NavigationService _navigationService;
    private string _titleText = "Vanara Jñāna";
    private string _subtitleText = "Workbench";
    private bool _isBackButtonVisible = true;
    private bool _isBackButtonEnabled = false;

    public MainWindow(MicaController micaController, SystemBackdropConfiguration configuration)
    {
        _controller = micaController;
        SysBackdropConfiguration = configuration;
        InitializeComponent();
        TrySetMicaBackdrop();

        _navigationService = new NavigationService();
        // TODO: Restore Navigation handling when NavigationService is implemented
        //_navigationService.OnPageNavigated += (sender, e) => NavigationHost.ShowPage(e.PageInstance);

        var workbench = new WorkbenchPage();
        NavigationHost.ShowPage(workbench);
        NavigationService.Navigate(typeof(WorkbenchPage));
    }

    public MicaController Controller => _controller;

    public NavigationService NavigationService => _navigationService;

    /// <summary>
    ///     Public properties for the title, subtitle, and back button visibility of the main window.
    /// </summary>
    public string TitleText
    {
        get => _titleText;
        set => _titleText = value;
    }

    public string SubtitleText
    {
        get => _subtitleText;
        set => _subtitleText = value;
    }

    public bool IsBackButtonVisible
    {
        get => _isBackButtonVisible;
        set => _isBackButtonVisible = value;
    }

    public bool IsBackButtonEnabled
    {
        get => _isBackButtonEnabled;
        set => _isBackButtonEnabled = value;
    }

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