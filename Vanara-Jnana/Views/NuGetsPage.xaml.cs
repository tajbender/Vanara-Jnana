using CommunityToolkit.Mvvm.Input;
using Jnana.Services;
using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;
using NuGet.Common;
using NuGet.Versioning;
using System.Diagnostics;
using System.Windows.Input;

namespace Jnana.Views;

public sealed partial class NuGetsPage : Page
{
    private readonly ILogger? logger;
    public NuGetsAreaViewModel ViewModel { get; }

    private CancellationToken cancelToken = CancellationToken.None;
    public PackageVersionInfo? SelectedPackage { get; private set; } = null;

    public NuGetsPage()
    {
        InitializeComponent();

        logger = new NullLogger();
        ViewModel = new NuGetsAreaViewModel(logger);

        
        ViewModel.LoadPackagesAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Debug.Print($"Failed to load NuGet packages: {t.Exception}");
                    //logger?.LogError(t.Exception, "Failed to load NuGet packages");
                }
            }, cancelToken);
    }

    private void PackageTreeView_SelectionChanged(TreeView sender, TreeViewSelectionChangedEventArgs args)
    {
        if (sender.Equals(PackageTreeView))
        {
            SelectedPackage = args.AddedItems.FirstOrDefault() as PackageVersionInfo;

            if (SelectedPackage != null)
            {
                Debug.Print($"Selected package: {SelectedPackage.Id} {SelectedPackage.Version}");
                ViewModel.SelectVersionCommand.Execute(SelectedPackage);
            }
        }
    }
}
