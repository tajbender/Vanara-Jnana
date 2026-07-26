using CommunityToolkit.Mvvm.ComponentModel;
using Jnana.Views;
using Jnana.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using Vanara_Jnana.exe.Models.Contracts;
using static Vanara_Jnana.exe.Models.Contracts.INavigationService;

namespace Jnana.Services;

//public static class AreaExtensions
//{
//    public static Type GetPageType(this NavigationArea area) =>
//        area switch
//        {
//            NavigationArea.Settings => typeof(SettingsPage),
//            NavigationArea.Void => typeof(VoidPage),
//            //NavigationArea.SysInfo => typeof(SysInfoPage),
//            NavigationArea.Utilities => typeof(UtilitiesPage),
//            //NavigationArea.Shell => typeof(ShellPage),
//            _ => throw new NotImplementedException()
//        };
//}

public sealed class NamespaceAddress
{
    public string Provider { get; }
    public string Path { get; }

    public NamespaceAddress(string address)
    {
        var parts = address.Split("://", 2);
        Provider = parts[0];
        Path = parts.Length > 1 ? parts[1] : string.Empty;
    }

    public override string ToString() => $"{Provider}://{Path}";
}

public sealed class NavigationNode
{
    public string Title { get; init; }
    public IconSource Icon { get; init; }
    public Type PageType { get; init; }
    public object ViewModel { get; init; }
    public bool IsPanel { get; init; }
    public bool OpenInNewTab { get; init; }
}



public partial class NavigationService : ObservableObject /* TODO: INavigationService */
{

    private readonly Dictionary<string, INavigationProvider> _providers = new();
    private readonly List<NavigationNode> _history = new();
    private int _historyIndex = -1;

    public NavigationNode CurrentNode { get; private set; }
    public event Action<NavigationNode> Navigated;

    public void RegisterProvider(INavigationProvider provider)
    {
        _providers[provider.GetType().Name.Replace("Provider", "").ToLower()] = provider;
    }

    public async Task NavigateAsync(string address)
    {
        var ns = new NamespaceAddress(address);

        if (!_providers.TryGetValue(ns.Provider.ToLower(), out var provider))
            throw new InvalidOperationException($"No provider for '{ns.Provider}'");

        var node = await provider.ResolveAsync(ns);

        CurrentNode = node;
        Navigated?.Invoke(node);

        // History aktualisieren
        if (_historyIndex < _history.Count - 1)
            _history.RemoveRange(_historyIndex + 1, _history.Count - _historyIndex - 1);

        _history.Add(node);
        _historyIndex = _history.Count - 1;
    }

    public bool CanGoBack => _historyIndex > 0;
    public bool CanGoForward => _historyIndex < _history.Count - 1;

    public void GoBack()
    {
        if (!CanGoBack) return;
        _historyIndex--;
        CurrentNode = _history[_historyIndex];
        Navigated?.Invoke(CurrentNode);
    }

    public void GoForward()
    {
        if (!CanGoForward) return;
        _historyIndex++;
        CurrentNode = _history[_historyIndex];
        Navigated?.Invoke(CurrentNode);
    }



    /// <summary>
    /// TODO: OLD STUFF BELOW HERE, NEEDS TO BE REPLACED WITH THE NEW NAVIGATION SERVICE
    /// </summary>
    private readonly Frame _frame;
    private readonly Dictionary<NavigationArea, Type> _areaPageMap = new()
    { 
            //{ NavigationArea.Disassembler, typeof(DisassemblerPage)  },
            //{ NavigationArea.GitHub, typeof(GitHubPage) },
            //{ NavigationArea.NuGets, typeof(NuGetsPage) },
            //{ NavigationArea.Samples, typeof(SamplesPage) },
            { NavigationArea.Settings, typeof(SettingsPage) },
            { NavigationArea.Utilities, typeof(UtilitiesPage) },
            { NavigationArea.Void, typeof(VoidPage) },
    };

    public NavigationService(Frame frame)
    {
        _frame = frame ?? throw new ArgumentNullException(nameof(frame));
    }

    public void NavigateTo(NavigationArea area)
    {
        try
        {
            if (_areaPageMap.TryGetValue(area, out var pageType))
            {
                Debug.Print($"Navigating to `{area}` page.");
                _frame.Navigate(pageType);
            }
            else
            {
                Debug.Print($"Failed to get page for `{area}` from PageMap.");
            }
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.ToString());
            throw;
        }
    }
}

public sealed class NavigationStack
{
    private readonly Stack<NavigationState> _back = new();
    private readonly Stack<NavigationState> _forward = new();

    public NavigationState Current { get; private set; }

    public void Navigate(NavigationState state) { }
    public bool CanGoBack => _back.Count > 0;
    public bool CanGoForward => _forward.Count > 0;
    public NavigationState GoBack() { return _back.Pop(); }
    public NavigationState GoForward() { _back.Push(Current); return Current; }
}

//    public void Navigate(NavigationArea area)
//    {
//        if (_frame.Content is not IAreaPage current || current.NavigationArea != area)
//        {
////            if (_frame.Content is IAreaPage curr)
////                _historyBack.Push(curr.NavigationArea);
////
////            _historyForward.Clear();
////
////            _frame.Navigate(area.GetPageType());
////            AreaChanged?.Invoke(this, area);
//        }
//    }

//    public bool CanGoBack => _historyBack.Count > 0;
//    public bool CanGoForward => _historyForward.Count > 0;

//    public void GoBack()
//    {
//        if (!CanGoBack) return;
//
//        var prev = _historyBack.Pop();
//        if (_frame.Content is IAreaPage curr)
//            _historyForward.Push(curr.NavigationArea);
//
//        _frame.Navigate(prev.GetPageType());
//        AreaChanged?.Invoke(this, prev);

//    public void GoForward()
//    {
//        if (!CanGoForward) return;
//
//        var next = _historyForward.Pop();
//        if (_frame.Content is IAreaPage curr)
//            _historyBack.Push(curr.NavigationArea);
//
//        _frame.Navigate(next.GetPageType());
//        AreaChanged?.Invoke(this, next); }
