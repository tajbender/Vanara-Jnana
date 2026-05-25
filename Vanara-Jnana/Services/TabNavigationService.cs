using ClassicSamplesBrowser.Views;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;

namespace ClassicSamplesBrowser.Services;

public static class TabNavigationService
{
    private static TabView _tabView;

    public static void Initialize(TabView tabView)
    {
        _tabView = tabView;
    }

    public static void OpenTab(string header, Type pageType, object parameter = null)
    {
        var tab = new TabViewItem
        {
            Header = header,
            Content = Activator.CreateInstance(pageType, parameter)
        };

        _tabView.TabItems.Add(tab);
        _tabView.SelectedItem = tab;
    }

    public static void AddApiExplorerPageTab(Type type)
    {
        var tab = new TabViewItem
        {
            Header = $"Explore {type.Name}",
            Content = new ApiExplorerPage(type)
        };

        _tabView.TabItems.Add(tab);
        _tabView.SelectedItem = tab;
    }
}
