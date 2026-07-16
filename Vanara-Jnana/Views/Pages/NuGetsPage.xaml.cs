using CommunityToolkit.Mvvm.Input;
using Jnana.Services;
using Jnana.ViewModels;
using Microsoft.UI.Xaml.Controls;
using NuGet.Common;
using NuGet.Versioning;
using System.Diagnostics;
using System.Windows.Input;

namespace Jnana.Views.Pages;

public sealed partial class NuGetsPage : Page
{
    private ILogger? logger;
    public NuGetsAreaViewModel ViewModel { get; }

    private CancellationToken cancelToken = CancellationToken.None;
    public PackageVersionInfo? SelectedPackage { get; private set; } = null;

    public NuGetsPage(NuGetsAreaViewModel viewModel, ILogger? logger = null)
    {
        InitializeComponent();
        ViewModel = viewModel;
        this.logger = logger ?? new NullLogger();

        //ViewModel.SynchronizePackageCacheAsync().ContinueWith(t =>
        //    {
        //        if (t.IsFaulted)
        //        {
        //            Debug.Print($"Failed to load NuGet packages: {t.Exception}");
        //            //logger?.LogError(t.Exception, "Failed to load NuGet packages");
        //        }
        //    }, cancelToken);
    }

    public void Dispose()
    {
        cancelToken.ThrowIfCancellationRequested();
        cancelToken = new CancellationToken(true);
    }

    //    public void SetSelectedPackage(PackageVersionInfo package)
    //    {
    //        SelectedPackage = package;
    //        Debug.Print($"Set selected package: {SelectedPackage.Id} {SelectedPackage.Version}");
    //    }

    public void SetLogger(ILogger logger)
    {
        if (logger != null)
        {
            this.logger = logger;
            Debug.Print($"Logger set to: {logger.GetType().Name}");
        }
        else
        {
            this.logger = null;
            Debug.Print("Logger set to null, old logger discarded");
        }
    }

    //private void PackageTreeView_SelectionChanged(TreeView sender, TreeViewSelectionChangedEventArgs args)
    //{
    //    if (sender.Equals(PackageTreeView))
    //    {
    //        SelectedPackage = args.AddedItems.FirstOrDefault() as PackageVersionInfo;

    //        if (SelectedPackage != null)
    //        {
    //            Debug.Print($"Selected package: {SelectedPackage.Id} {SelectedPackage.Version}");
    //            ViewModel.SelectVersionCommand.Execute(SelectedPackage);
    //        }
    //    }
    //}
}
