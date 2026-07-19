using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace GitHubRepositoryViewer.Models;

public sealed partial class GitHubRepositoryModel : ObservableObject
{
    [ObservableProperty] private string name;
    [ObservableProperty] private string description;
    [ObservableProperty] private string url;
    [ObservableProperty] private string? language;
    [ObservableProperty] private int stars;
    [ObservableProperty] private int forks;
}

public sealed partial class GitHubRepositoryFolderModel : ObservableObject
{
    public string Title { get; }
    public ObservableCollection<GitHubRepositoryModel> Items { get; } = new();

    public GitHubRepositoryFolderModel(string title)
    {
        Title = title;
    }
}

public sealed class GitHubDataProvider
{
    public async Task<GitHubRepositoryFolderModel> GetVanaraReposAsync()
    {
        var folder = new GitHubRepositoryFolderModel("Vanara GitHub");

//        foreach (var repo in VanaraData.Repositories) // deine vorhandenen Daten
//        {
//            folder.Items.Add(new GitHubRepositoryModel
//            {
//                Name = repo.Name,
//                Description = repo.Description,
//                Url = repo.Url,
//                Language = repo.Language,
//                Stars = repo.Stars,
//                Forks = repo.Forks
//            });
//        }

        return folder;
    }
}

public sealed partial class GitHubExplorerViewModel : ObservableObject
{
    private readonly GitHubDataProvider provider = new();

    [ObservableProperty]
    private GitHubRepositoryFolderModel? currentFolder;

    public async Task InitializeAsync()
    {
//        CurrentFolder = await provider.GetVanaraReposAsync();
    }
}
