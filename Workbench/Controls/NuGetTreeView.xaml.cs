using System;
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

public sealed class DependencyGraphResult(
    IReadOnlyList<PackageInfo> topLevel,
    IReadOnlyList<PackageInfo> transitive)
{
    public IReadOnlyList<PackageInfo> TopLevelPackages { get; } = topLevel;

    public IReadOnlyList<PackageInfo> TransitivePackages { get; } = transitive;
}

public sealed class PackageInfo(string id, string version)
{
    public string Id { get; } = id;

    public string Version { get; } = version;
}

public abstract class TreeNode(string name)
{
    private readonly List<TreeNode> _children = [];

    public string Name { get; } = name;

    public IReadOnlyList<TreeNode> Children => _children;

    public void AddChild(TreeNode node)
    {
        _children.Add(node);
    }
}

public sealed class PackageNode(string name, string version, bool isTopLevel) : TreeNode(name)
{
    public string Version { get; } = version;

    public bool IsTopLevel { get; } = isTopLevel;
}

public sealed class PackageGroupNode(string name) : TreeNode(name);

public sealed class NuGetTreeRoot() : TreeNode("Root");

public sealed partial class NuGetTreeViewModel(INuGetDependencyGraphService graphService) : ObservableObject
{
    // -----------------------------
    // Loading State
    // -----------------------------
    //private bool _isLoading = true;

    [ObservableProperty] public bool _isLoading;

    // -----------------------------
    // Tree Nodes
    // -----------------------------
    public ObservableCollection<TreeNode> RootNodes { get; } = [];

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

            var graph = await graphService.GetDependencyGraphAsync(projectPath);

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

[Obsolete("Use NuGetTreeViewModel instead.")]
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