using System;
using System.Collections.Generic;

namespace Jnana.Core.Navigation;

public enum PageKeys
{
    Workbench,
    GitHub,
    NuGets,
    Samples,
    SysInfo
}


public static class PageRegistry
{
    private static readonly Dictionary<PageKeys, Type> _pages = new();

    public static void Register(PageKeys key, Type pageType)
    {
        _pages[key] = pageType;
    }

    public static Type? Resolve(PageKeys key)
    {
        return _pages.TryGetValue(key, out var type) ? type : null;
    }
}
