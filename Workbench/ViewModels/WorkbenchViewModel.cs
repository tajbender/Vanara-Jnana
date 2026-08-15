using CommunityToolkit.Mvvm.ComponentModel;

namespace Jnana.Workbench.ViewModels;

public class WorkbenchViewModel : ObservableObject
{
    private bool _isLeftPaneActive;
    private bool _isRightPaneActive;
    private bool _isPaneButtonVisible;
    private bool _showBackButtonSetting;

    public bool IsLeftPaneActive
    {
        get => _isLeftPaneActive;
        set => this.SetProperty(ref this._isLeftPaneActive, value);
    }

    public bool IsRightPaneActive => !IsLeftPaneActive;
    public bool IsPaneButtonVisible
    {
        get => _isPaneButtonVisible;
        set => this.SetProperty(ref this._isPaneButtonVisible, value);
    }
    public bool ShowBackButtonSetting
    {
        get => _showBackButtonSetting;
        set => this.SetProperty(ref this._showBackButtonSetting, value);
    }
    public double LeftPaneOpacity => IsLeftPaneActive ? 1 : 0.2;
    public double RightPaneOpacity => IsRightPaneActive ? 1 : 0.2;

    public WorkbenchViewModel()
    {

    }

    private void UpdatePaneOpacities()
    {
        OnPropertyChanged(nameof(LeftPaneOpacity));
        OnPropertyChanged(nameof(RightPaneOpacity));
    }

    private void UpdatePaneButtonVisibility()
    {
        OnPropertyChanged(nameof(IsPaneButtonVisible));
    }

    private void UpdateBackButtonVisibility()
    {
        OnPropertyChanged(nameof(ShowBackButtonSetting));
    }

    private void UpdatePaneStates()
    {
        UpdatePaneOpacities();
        UpdatePaneButtonVisibility();
        UpdateBackButtonVisibility();
    }

//    <summary>
//        This method is called whenever a property changes.
//    </summary>
//    <param name="propertyName">The name of the property that changed.</param>

    private void OnPropertyChanged(string propertyName) =>
        base.OnPropertyChanged(propertyName);
}
