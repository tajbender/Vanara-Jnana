using System;
using System.Collections.Generic;
using System.Linq;

namespace Jnana.Core.Navigation;

public sealed class NamespaceUri
{
    public string Scheme { get; }
    public string Path { get; }
    public IReadOnlyDictionary<string, string> Parameters { get; }
    public string Fragment { get; }

    public static NamespaceUri Parse(string uri)
    {
        var u = new Uri(uri);

        var scheme = u.Scheme;                     // shell32, https, jnana, etc.
        var path = u.AbsolutePath.Trim('/');       // c:/windows/temp, workbench/dashboard

        var parameters = u.Query
            .TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(p => p.Split('='))
            .ToDictionary(p => p[0], p => p[1]);

        var fragment = u.Fragment.TrimStart('#');  // goback, tile-id, etc.

        return new NamespaceUri(scheme, path, parameters, fragment);
    }

    private NamespaceUri(string scheme, string path,
        IReadOnlyDictionary<string, string> parameters, string fragment)
    {
        Scheme = scheme;
        Path = path;
        Parameters = parameters;
        Fragment = fragment;
    }

    // TODO: Implement the Resolve method to find the appropriate provider and resolve the URI to a NamespaceNode
    //    public static NamespaceNode Resolve(NamespaceUri uri)
    //    {
    ////        return Providers
    ////            .FirstOrDefault(p => p.CanHandle(uri))
    ////            ?.Resolve(uri);
    //    }

    public sealed class NamespaceNode
    {
        public Type PageType { get; init; }
        public object TargetObject { get; init; }
        public object Payload { get; init; }
    }

}

