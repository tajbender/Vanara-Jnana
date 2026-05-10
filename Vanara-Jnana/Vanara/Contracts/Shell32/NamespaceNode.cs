using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicSamplesBrowser.Vanara.Contracts.Shell32;

public sealed class NamespaceNode
{
    public NamespaceAddress Address { get; }
    public string DisplayName { get; }
    public string? Description { get; }
    public string? IconKey { get; } // for glyphs / icons
    public object? Payload { get; } // strongly-typed model (file info, issue, package, clipboard content, ...)

    // Optional: for breadcrumbs
    public NamespaceNode? Parent { get; }
}
