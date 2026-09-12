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
    private static readonly Dictionary<PageKeys, Type> Pages = [];

    public static void Register(PageKeys key, Type pageType)
    {
        Pages[key] = pageType;
    }

    public static Type? Resolve(PageKeys key)
    {
        return Pages.GetValueOrDefault(key, null);
    }
}
