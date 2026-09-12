using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Controls.Shell;

public sealed partial class ExplorerBrowser : Control
{
    public List<string> Items;

    public object ViewModel = new();

    public ExplorerBrowser()
    {
        DefaultStyleKey = typeof(ExplorerBrowser);
    }
}