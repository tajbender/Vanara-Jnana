using ClassicSamplesBrowser.Views;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
//using System.Reflection.Metadata;
//using static ICSharpCode.Decompiler.SingleFileBundle;

namespace ClassicSamplesBrowser.Services;

public static class TabNavigationService
{
    private static TabView _tabView;
    public static IList<object> TabItems => _tabView.TabItems;

    public static void Initialize(TabView tabView)
    {
        _tabView = tabView;
    }

    public static void AddTypedTab(string header, Type pageType, object parameter = null)
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
            Header = "Api Explorer",
            Content = new ApiExplorerPage(typeof(ApiExplorerPage))
        };

        _tabView.TabItems.Add(tab);
        _tabView.SelectedItem = tab;
    }

    public static void AddNuGetsPageTab()
    {
        var tab = new TabViewItem
        {
            Header = "NuGets",
            Content = new NuGetsPage()
        };

        _tabView.TabItems.Add(tab);
        _tabView.SelectedItem = tab;
    }
    public static void AddSamplesPageTab()
    {
        var tab = new TabViewItem
        {
            Header = "Samples",
            Content = new SamplesPage()
        };

        _tabView.TabItems.Add(tab);
        _tabView.SelectedItem = tab;
    }
    public static void AddShellPageTab()
    {
        var tab = new TabViewItem
        {
            Header = "Shell",
            Content = new ShellPage()
        };

        _tabView.TabItems.Add(tab);
        _tabView.SelectedItem = tab;
    }
    public static void AddSettingsPageTab()
    {
        var tab = new TabViewItem
        {
            Header = "Settings",
            Content = new SettingsPage()
        };

        _tabView.TabItems.Add(tab);
        _tabView.SelectedItem = tab;
    }
}
