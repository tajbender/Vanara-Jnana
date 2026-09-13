using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Core.Services;
using Microsoft.UI.Xaml.Controls;

namespace Jnana.Workbench.Controls;

public interface INuGetDependencyGraphService
{
    Task<DependencyGraphResult> GetDependencyGraphAsync(string projectPath);
}

public sealed class DependencyGraphResult
{
    public DependencyGraphResult(
        IReadOnlyList<PackageInfo> topLevel,
        IReadOnlyList<PackageInfo> transitive)
    {
        TopLevelPackages = topLevel;
        TransitivePackages = transitive;
    }

    public IReadOnlyList<PackageInfo> TopLevelPackages { get; }
    public IReadOnlyList<PackageInfo> TransitivePackages { get; }
}

public sealed class PackageInfo
{
    public PackageInfo(string id, string version)
    {
        Id = id;
        Version = version;
    }

    public string Id { get; }
    public string Version { get; }
}

public abstract class TreeNode
{
    private readonly List<TreeNode> _children = [];

    protected TreeNode(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public IReadOnlyList<TreeNode> Children => _children;

    public void AddChild(TreeNode node)
    {
        _children.Add(node);
    }
}

public sealed class PackageNode : TreeNode
{
    public PackageNode(string name, string version, bool isTopLevel)
        : base(name)
    {
        Version = version;
        IsTopLevel = isTopLevel;
    }

    public string Version { get; }
    public bool IsTopLevel { get; }
}

public sealed class PackageGroupNode : TreeNode
{
    public PackageGroupNode(string name)
        : base(name)
    {
    }
}

public sealed class NuGetTreeRoot : TreeNode
{
    public NuGetTreeRoot()
        : base("Root")
    {
    }
}

public sealed partial class NuGetTreeViewModel : ObservableObject
{
    private readonly INuGetDependencyGraphService _graphService;

    // -----------------------------
    // Loading State
    // -----------------------------
    //private bool _isLoading = true;

    [ObservableProperty] public bool _isLoading;

    public NuGetTreeViewModel(INuGetDependencyGraphService graphService)
    {
        _graphService = graphService;
        RootNodes = [];
    }

    // -----------------------------
    // Tree Nodes
    // -----------------------------
    public ObservableCollection<TreeNode> RootNodes { get; }

    // -----------------------------
    // Commands
    // -----------------------------
    [RelayCommand]
    public async Task LoadAsync(string projectPath)
    {
        try
        {
            IsLoading = true;
            RootNodes.Clear();

            var graph = await _graphService.GetDependencyGraphAsync(projectPath);

            var root = BuildTree(graph);

            foreach (var node in root.Children)
                RootNodes.Add(node);
        }
        finally
        {
            IsLoading = false;
        }
    }

    // -----------------------------
    // Tree Builder
    // -----------------------------
    public static NuGetTreeRoot BuildTree(DependencyGraphResult graph)
    {
        var root = new NuGetTreeRoot();

        var topLevelNode = new PackageGroupNode("Top-Level Packages");
        var transitiveNode = new PackageGroupNode("Transitive Packages");

        root.AddChild(topLevelNode);
        root.AddChild(transitiveNode);

        foreach (var pkg in graph.TopLevelPackages)
            topLevelNode.AddChild(new PackageNode(pkg.Id, pkg.Version, true));

        foreach (var pkg in graph.TransitivePackages)
            transitiveNode.AddChild(new PackageNode(pkg.Id, pkg.Version, false));

        return root;
    }
}

public sealed partial class NuGetTreeView : UserControl
{
    private readonly NuGetDependencyGraphService _dependencyGraphService = new();

    public NuGetTreeView()
    {
        InitializeComponent();

        ViewModel = new NuGetTreeViewModel(_dependencyGraphService);
        NuGetTreeViewControl.ItemsSource = ViewModel.RootNodes;
    }

    public ObservableCollection<TreeNode> RootNodes => ViewModel.RootNodes;
    public NuGetTreeViewModel ViewModel { get; }

    public static NuGetTreeRoot BuildTree(DependencyGraphResult graph)
    {
        var root = new NuGetTreeRoot();

        var topLevelNode = new PackageGroupNode("Top-Level Packages");
        var transitiveNode = new PackageGroupNode("Transitive Packages");

        root.AddChild(topLevelNode);
        root.AddChild(transitiveNode);

        foreach (var pkg in graph.TopLevelPackages)
            topLevelNode.AddChild(new PackageNode(pkg.Id, pkg.Version, true));

        foreach (var pkg in graph.TransitivePackages)
            transitiveNode.AddChild(new PackageNode(pkg.Id, pkg.Version, false));

        return root;
    }
}