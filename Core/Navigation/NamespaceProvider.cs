using Jnana.Core.Navigation.Providers;
using System.Collections.Generic;
using System.Linq;
using static Jnana.Core.Navigation.NamespaceUri;

namespace Jnana.Core.Navigation;

public static class NamespaceProvider
{
    private static readonly List<INamespaceProvider> Providers = new()
    {
        new ShellProvider(),
        new WebProvider(),
        new ReflectionProvider(),
        new WorkbenchProvider()
    };

    public static NamespaceNode Resolve(string uri)
        => Resolve(NamespaceUri.Parse(uri));

    public static NamespaceNode Resolve(NamespaceUri uri)
        => Providers.FirstOrDefault(p => p.CanHandle(uri))?.Resolve(uri);
}
