using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jnana.ViewModels;

internal class VoidPageViewModel
{
    public List<String> NugetItems { get; set; }
    public List<String> GitHubItems { get; set; }
    public List<String> SamplesItems { get; set; }
    public VoidPageViewModel()
    {
        NugetItems = new List<String>();
        GitHubItems = new List<String>();
        SamplesItems = new List<String>();

        NugetItems.AddRange(new String[] { "Vanara.Core", "Vanara.PInvoke", "Vanara.Windows", "etc." });
        GitHubItems.AddRange(new String[] { "Home", "Releases", "Issues", "Pull Requests" });
        SamplesItems.AddRange(new String[] { "Item 1", "Item 2", "Item 3", "etc." });
    }
}
