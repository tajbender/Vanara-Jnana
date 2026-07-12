using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jnana.ViewModels;

public class VanaraScienceLaboratoriesViewModel
{
    public class LabExperimentModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Action Execute { get; set; }
    }

    public ObservableCollection<LabExperimentModel> LabExperiments { get; } = new()
    {
        new() { Title = "Reflection Test", Description = "Inspect Vanara.Core types dynamically." },
        new() { Title = "Interop Sandbox", Description = "Run safe PInvoke experiments." },
        new() { Title = "Unit Test Runner", Description = "Execute sample tests inline." }
    };
}
