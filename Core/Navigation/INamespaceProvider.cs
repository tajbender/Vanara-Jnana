using static Jnana.Core.Navigation.NamespaceUri;

namespace Jnana.Core.Navigation;

public interface INamespaceProvider
{
    bool CanHandle(NamespaceUri uri);
    NamespaceNode Resolve(NamespaceUri uri);
}
