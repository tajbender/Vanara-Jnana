using Jnana.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanara_Jnana.exe.Models.Contracts;


public interface INavigationProvider
{
    bool CanHandle(string provider);
    Task<NavigationNode> ResolveAsync(NamespaceAddress address);
}


public interface INavigationService
{
    public enum NavigationArea
    {
        Void,
        NuGets,
        GitHub,
        Samples,
        Disassembler,
        Utilities,
        Settings,
        SysInfo,
        Shell,
        Workbench,
    }


    public sealed record NavigationState(
        Type PageType,
        object? Parameter,
        Guid? TabId,
        string? Title,
        DateTime Timestamp);


    void NavigateTo(NavigationArea area);
}
