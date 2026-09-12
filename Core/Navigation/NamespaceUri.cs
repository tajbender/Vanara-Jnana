using System;
using System.Collections.Generic;
using System.Linq;
using Jnana.Workbench.Pages.Workbench;

namespace Jnana.Core.Navigation;

public sealed class NamespaceUri
{
    // NamespaceNode.Void is a special value indicating that there is no specific target object for the page.
    /// <summary>
    ///     Gets the void namespace node.
    /// </summary>
    public static readonly NamespaceNode Void = new()
    {
        PageType = typeof(WorkbenchPage),
        TargetObject = null,
        Payload = null
    };

    private readonly string _scheme;
    private readonly string _path;
    private readonly IReadOnlyDictionary<string, string> _parameters;
    private readonly string _fragment;

    private NamespaceUri(string scheme, string path,
        IReadOnlyDictionary<string, string> parameters, string fragment)
    {
        _scheme = scheme;
        _path = path;
        _parameters = parameters;
        _fragment = fragment;
    }

    public string Scheme => _scheme;

    public string Path => _path;

    public IReadOnlyDictionary<string, string> Parameters => _parameters;

    public string Fragment => _fragment;

    public static NamespaceUri Parse(string uri)
    {
        var u = new Uri(uri);

        var scheme = u.Scheme; // shell32, https, jnana, etc.
        var path = u.AbsolutePath.Trim('/'); // c:/windows/temp, workbench/dashboard

        var parameters = u.Query
            .TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(p => p.Split('='))
            .ToDictionary(p => p[0], p => p[1]);

        var fragment = u.Fragment.TrimStart('#'); // goback, tile-id, etc.

        return new NamespaceUri(scheme, path, parameters, fragment);
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
        private readonly Type _pageType;
        private readonly object _targetObject;
        private readonly object _payload;

        public Type PageType
        {
            get => _pageType;
            init => _pageType = value;
        }

        public object TargetObject
        {
            get => _targetObject;
            init => _targetObject = value;
        }

        public object Payload
        {
            get => _payload;
            init => _payload = value;
        }
    }


    //public static readonly NamespaceNode VoidNsNode = new(PageType: typeof(WorkbenchPage), TargetObject: null, Payload: null);
}