using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanara_Jnana.exe.Models.Contracts;

public interface INavigationService
{
    public enum Area
    {
        Void,
        NuGets,
        GitHub,
        Samples,
        Disassembler,
        Utilities,
        Settings
    }

    void NavigateTo(Area area);
}
