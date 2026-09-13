using System.Collections.Generic;
using System.Linq;
using Jnana.Core.Navigation.Providers;
using static Jnana.Core.Navigation.NamespaceUri;

namespace Jnana.Core.Navigation;

public static class NamespaceProvider
{
    private static readonly List<INamespaceProvider> Providers =
    [
        new ShellProvider(),
        new WebProvider(),
        new ReflectionProvider(),
        new WorkbenchProvider()
    ];

    /// <summary>
    ///     Resolves a namespace node for the specified URI.
    /// </summary>
    /// <param name="uri">The URI to resolve.</param>
    /// <returns>The resolved namespace node, or null if not found.</returns>
    public static NamespaceNode? Resolve(NamespaceUri uri)
    {
        return Providers.FirstOrDefault(p => p.CanHandle(uri))?.Resolve(uri);
    }


    /// <summary>
    /// Resolves a namespace node for the specified URI string.
    /// </summary>
    /// <param name="uri">The URI string to resolve.</param>
    /// <returns>The resolved namespace node, or null if not found.</returns>
//    public static NamespaceNode? Resolve(string uri)
//    {
//        return Resolve(NamespaceUri.Parse(uri));
//    }

    /// <summary>
    /// Resolves all namespace nodes for the specified URI.
    /// </summary>
    /// <param name="uri">The URI to resolve.</param>
    /// <returns>A list of resolved namespace nodes.</returns>
//    public static List<NamespaceNode> ResolveAll(string uri)
//    {
//        return Resolve(NamespaceUri.Parse(uri));
//    }


/// <summary>
///     Resolves all namespace nodes for the specified URI.
/// </summary>
/// <param name="uri">The URI to resolve.</param>
/// <returns>A list of resolved namespace nodes.</returns>
public static List<NamespaceNode> ResolveAll(string uri)
    {
        return
        [
            .. Providers.Where(p => p.CanHandle(Parse(uri)))
                .Select(p => p.Resolve(Parse(uri)))
        ];
    }
}