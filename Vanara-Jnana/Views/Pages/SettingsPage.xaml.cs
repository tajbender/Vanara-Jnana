using Microsoft.UI.Xaml.Controls;

namespace Jnana.Views;

public sealed partial class SettingsPage : Page
{
    private ViewModels.SettingsAreaViewModel ViewModel { get; } = new ViewModels.SettingsAreaViewModel();
    private ViewModels.SysInfoIdentityViewModel SysInfoIdentityViewModel { get; } = new ViewModels.SysInfoIdentityViewModel();



    public SettingsPage()
    {
        InitializeComponent();
    }
}
