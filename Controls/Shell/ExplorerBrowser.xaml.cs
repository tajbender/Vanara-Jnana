using CommunityToolkit.WinUI.UI.Controls.TextToolbarSymbols;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

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