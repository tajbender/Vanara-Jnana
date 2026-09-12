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
    private readonly IReadOnlyList<PackageInfo> _topLevelPackages = topLevel;
    private readonly IReadOnlyList<PackageInfo> _transitivePackages = transitive;

    public IReadOnlyList<PackageInfo> TopLevelPackages => _topLevelPackages;

    public IReadOnlyList<PackageInfo> TransitivePackages => _transitivePackages;
}

public sealed class PackageInfo(string id, string version)
{
    private readonly string _id = id;
    private readonly string _version = version;

    public string Id => _id;

    public string Version => _version;
}

public abstract class TreeNode(string name)
{
    private readonly List<TreeNode> _children = [];
    private readonly string _name = name;

    public string Name => _name;

    public IReadOnlyList<TreeNode> Children => _children;

    public void AddChild(TreeNode node)
    {
        _children.Add(node);
    }
}

public sealed class PackageNode(string name, string version, bool isTopLevel) : TreeNode(name)
{
    private readonly string _version = version;
    private readonly bool _isTopLevel = isTopLevel;

    public string Version => _version;

    public bool IsTopLevel => _isTopLevel;
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
    private readonly ObservableCollection<TreeNode> _rootNodes = [];

    // -----------------------------
    // Tree Nodes
    // -----------------------------
    public ObservableCollection<TreeNode> RootNodes => _rootNodes;

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
    private readonly NuGetTreeViewModel _viewModel;

    public NuGetTreeView()
    {
        InitializeComponent();

        _viewModel = new NuGetTreeViewModel(_dependencyGraphService);
        NuGetTreeViewControl.ItemsSource = ViewModel.RootNodes;
    }

    public ObservableCollection<TreeNode> RootNodes => ViewModel.RootNodes;

    public NuGetTreeViewModel ViewModel => _viewModel;

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