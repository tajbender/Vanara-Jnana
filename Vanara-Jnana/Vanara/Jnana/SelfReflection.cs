namespace ClassicSamplesBrowser.Vanara.Jnana;

/// <summary>
/// <see cref="SelfReflection"/> class for the Jnana project. This class contains types and interfaces related to reflection
/// and async refresh functionality for elements in the Jnana application. The types defined here are used to represent
/// various elements such as assemblies, classes, delegates, enums, fields, interfaces, methods, namespaces, properties,
/// and structs, along with their associated information and refresh capabilities.
/// </summary>
internal class SelfReflection
{
    internal enum ElementType
    {
        Assembly = 1,
        Class,
        Delegate,
        Enum,
        Field,
        Interface,
        Method,
        Namespace,
        Property,
        Struct,
    }

    /// <summary>
    /// Interface for asynchronous refresh functionality. See also:
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-14.0/async-streams#async-refresh">
    /// Async Refresh Proposal</seealso>
    /// </summary>
    internal interface IAsyncRefresh
    {
        Task RefreshAsync(CancellationToken cancellationToken, IProgress<int>? progress);
    }

    internal interface IElementInfo : IAsyncRefresh
    {
        public IEnumerable<IElementInfo> Children => [];
        public ElementType ElementType { get; }
        public string? ImageUrl => null;
        public string Name { get; }
        public string? Summary { get; }
        public IAsyncRefresh Refresh { get; }
    }
}
