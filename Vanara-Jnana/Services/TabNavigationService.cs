using Jnana.Views;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System;

namespace Jnana.Services;

public static class TabNavigationService
{
    private static TabView _tabView;
    public static IList<object> TabItems => _tabView.TabItems;

    public static void Initialize( TabView tabView)
    {
        _tabView = tabView;
    }

    public static void AddPageTab<T>(string header, Type pageType, object? parameter = null, bool selectTab = true)
        where T : Control
    {
        var tab = new TabViewItem
        {
            Header = header,
            Content = Activator.CreateInstance(pageType, parameter)
        };

        _tabView.TabItems.Add(tab);
        if (selectTab)
            _tabView.SelectedItem = tab;
    }

    public static void AddSettingsPageTab(bool selectTab = true)
    {
        AddPageTab<SettingsPage>("Settings", typeof(SettingsPage), null, selectTab);
    }
}
