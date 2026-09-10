using Jnana.ViewModels;
using Jnana.Workbench.Pages.Workbench;
using static Jnana.Core.Navigation.NamespaceUri;

namespace Jnana.Core.Navigation.Providers;

public sealed class TugorProvider : INamespaceProvider
{
    public bool CanHandle(NamespaceUri uri)
    {
        return uri.Scheme == "jnana" && uri.Path == "tugor";
    }

    public NamespaceNode Resolve(NamespaceUri uri)
    {
        return new NamespaceNode
        {
            PageType = typeof(TugorPage),
            Payload = new TugorGameState(),
            TargetObject = string.Empty // TODO: Determine if a specific target object is needed for the TugorPage
        };
    }
}