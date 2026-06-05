using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;   // INFO: `ContentPresenter` here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jnana.ViewModels;

public class VoidPageViewModel : ObservableObject
{
    public NuGetsAreaViewModel NuGetArea { get; }
    public GitHubAreaViewModel GitHubArea { get; }
    public SamplesAreaViewModel SamplesArea { get; }

    private object _currentArea;
    public object CurrentArea
    {
        get => _currentArea;
        set => SetProperty(ref _currentArea, value);
    }

    public ICommand NavigateCommand { get; }

    public VoidPageViewModel(
        NuGetsAreaViewModel nuget,
        GitHubAreaViewModel github,
        SamplesAreaViewModel samples)
    {
        NuGetArea = nuget;
        GitHubArea = github;
        SamplesArea = samples;

        CurrentArea = NuGetArea;

        NavigateCommand = new RelayCommand<string>(OnNavigate);
    }

    private void OnNavigate(string target)
    {
        CurrentArea = target switch
        {
            "NuGet" => NuGetArea,
            "GitHub" => GitHubArea,
            "Samples" => SamplesArea,
            _ => CurrentArea
        };
    }

    /// <summary>
    /// the following properties are just placeholders for the actual content of each area. In a real application, these would likely be more complex types representing the data and functionality of each area.
    /// </summary>
//    public List<String> NugetItems { get; set; }
//    public List<String> GitHubItems { get; set; }
//    public List<String> SamplesItems { get; set; }
}

// internal class VoidPageViewModel {
//    public List<String> NugetItems { get; set; }
//    public List<String> GitHubItems { get; set; }
//    public List<String> SamplesItems { get; set; }
//    public VoidPageViewModel()
//    {   NugetItems = new List<String>();
//        GitHubItems = new List<String>();
//        SamplesItems = new List<String>();
//
//        NugetItems.AddRange(new String[] { "Vanara.Core", "Vanara.PInvoke", "Vanara.Windows", "etc." });
//        GitHubItems.AddRange(new String[] { "Home", "Releases", "Issues", "Pull Requests" });
//        SamplesItems.AddRange(new String[] { "Item 1", "Item 2", "Item 3", "etc." }); } }
