using Jnana.Views.Tiles;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;

namespace Jnana.Workbench.Pages.Workbench;

public sealed partial class WorkbenchPage : Page
{
//    public ObservableCollection<UserControl> Tiles { get; } =
//    [
//        new WorkbenchTile(),
//        new NuGetTile()
//    ];
//
//    public ViewModels.WorkbenchViewModel ViewModel => 
//        (ViewModels.WorkbenchViewModel)DataContext;
//
//    public string SubtitleText { get; set; } = "Workbench";
//    public string TitleText { get; set; } = "Vanara jñāna";
//
//    public WorkbenchPage()
//    {
//        InitializeComponent();
//        this.DataContext = new ViewModels.WorkbenchViewModel();
//
//        // old stuff: LaunchBar.PageRequested += OnPageRequested;
////        LeftTreeView.GotFocus += (_, __) => ViewModel.IsLeftPaneActive = true;
////        LeftListView.GotFocus += (_, __) => ViewModel.IsLeftActive = true;
////        RightTreeView.GotFocus += (_, __) => ViewModel.IsRightPaneActive = false;
////        RightListView.GotFocus += (_, __) => ViewModel.IsLeftActive = false;
//    }

    //    private void OnPageRequested(Type pageType)
    //    {
    //        // Minimal: direkte Transformation
    //        var page = Activator.CreateInstance(pageType);
    //
    //        // WorkbenchContent wird ersetzt
    //        WorkbenchContent.Children.Clear();
    //        WorkbenchContent.Children.Add(item: page as UIElement);
    //    }
}
