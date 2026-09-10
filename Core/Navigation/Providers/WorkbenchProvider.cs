using System;
using static Jnana.Core.Navigation.NamespaceUri;

namespace Jnana.Core.Navigation.Providers;

public class WorkbenchProvider : INamespaceProvider
{
    public bool CanHandle(NamespaceUri uri)
    {
        throw new NotImplementedException();
    }

    public NamespaceNode Resolve(NamespaceUri uri)
    {
        throw new NotImplementedException();
    }
}