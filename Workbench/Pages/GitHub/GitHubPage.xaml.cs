using Microsoft.UI.Xaml.Controls;
using System;

namespace Jnana.Workbench.Pages.GitHub;

public sealed partial class GitHubPage : Page
{
    public GitHubPage()
    {
        this.InitializeComponent();
        this.InitializeWebView();
    }

    private async void InitializeWebView()
    {
        await this.GitHubView.EnsureCoreWebView2Async();

        // TODO: Add settings, disable context menus, inject CSS, etc.
        this.GitHubView.Source = new Uri("https://github.com/dahall/vanara");
    }
}
