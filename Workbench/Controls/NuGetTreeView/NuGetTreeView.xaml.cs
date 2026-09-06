using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Core.Services;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Jnana.Workbench.Controls;

public interface INuGetDependencyGraphService
{
    Task<DependencyGraphResult> GetDependencyGraphAsync(string projectPath);
}
public sealed class DependencyGraphResult
{
    public IReadOnlyList<PackageInfo> TopLevelPackages { get; }
    public IReadOnlyList<PackageInfo> TransitivePackages { get; }

    public DependencyGraphResult(
        IReadOnlyList<PackageInfo> topLevel,
        IReadOnlyList<PackageInfo> transitive)
    {
        this.TopLevelPackages = topLevel;
        this.TransitivePackages = transitive;
    }
}

public sealed class PackageInfo
{
    public string Id { get; }
    public string Version { get; }

    public PackageInfo(string id, string version)
    {
        this.Id = id;
        this.Version = version;
    }
}

public abstract class TreeNode
{
    public string Name { get; }
    public IReadOnlyList<TreeNode> Children => this._children;
    private readonly List<TreeNode> _children = [];

    protected TreeNode(string name)
    {
        this.Name = name;
    }

    public void AddChild(TreeNode node)
    {
        this._children.Add(node);
    }
}

public sealed class PackageNode : TreeNode
{
    public string Version { get; }
    public bool IsTopLevel { get; }

    public PackageNode(string name, string version, bool isTopLevel)
        : base(name)
    {
        this.Version = version;
        this.IsTopLevel = isTopLevel;
    }
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

    public NuGetTreeViewModel(INuGetDependencyGraphService graphService)
    {
        this._graphService = graphService;
        this.RootNodes = [];
    }

    // -----------------------------
    // Tree Nodes
    // -----------------------------
    public ObservableCollection<TreeNode> RootNodes { get; }

    // -----------------------------
    // Loading State
    // -----------------------------
    //private bool _isLoading = true;

    [ObservableProperty]
    public bool _isLoading;

    // -----------------------------
    // Commands
    // -----------------------------
    [RelayCommand]
    public async Task LoadAsync(string projectPath)
    {
        try
        {
            this.IsLoading = true;
            this.RootNodes.Clear();

            var graph = await this._graphService.GetDependencyGraphAsync(projectPath);

            var root = BuildTree(graph);

            foreach (var node in root.Children)
                this.RootNodes.Add(node);
        }
        finally
        {
            this.IsLoading = false;
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
    private NuGetDependencyGraphService _dependencyGraphService = new();
    private NuGetTreeViewModel _viewModel;

    public ObservableCollection<TreeNode> RootNodes => this._viewModel.RootNodes;
    public NuGetTreeViewModel ViewModel => this._viewModel;

    public NuGetTreeView()
    {
        this.InitializeComponent();

        this._viewModel = new NuGetTreeViewModel(this._dependencyGraphService);
        this.NuGetTreeViewControl.ItemsSource = this._viewModel.RootNodes;
    }

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
