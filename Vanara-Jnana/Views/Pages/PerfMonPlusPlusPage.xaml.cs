using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using Vanara_Jnana.ViewModels;

namespace Vanara_Jnana.Views.Pages;

public sealed partial class PerfMonPlusPlusPage : Page
{
    private PerfMonViewModel ViewModel => (PerfMonViewModel)DataContext;

    public PerfMonPlusPlusPage()
    {
        InitializeComponent();
        DataContext = new PerfMonViewModel();
    }

    private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine($"CategoryList_SelectionChanged({string.Join(", ", ((ListView)sender).SelectedItems)})");
        //TODO: ViewModel.UpdateSelectedCategories();
    }
}
