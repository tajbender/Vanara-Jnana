using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jnana.ViewModels;

internal class VoidPageViewModel
{
    public List<String> Items { get; set; }
    public VoidPageViewModel()
    {
        Items = new List<String>();

        Items.AddRange(new String[] { "Item 1", "Item 2", "Item 3" });
    }
}
