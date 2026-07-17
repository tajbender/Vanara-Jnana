using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jnana.Models;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Linq;

namespace Jnana.ViewModels;

public class FileWatcherViewModel : ObservableObject
{
    private readonly IFileWatcherService _service;

    public ObservableCollection<FileWatchSource> Sources { get; } = new();
    public ObservableCollection<FileWatchEvent> Events { get; } = new();

    private FileWatchSource? _selectedSource;
    public FileWatchSource? SelectedSource
    {
        get => _selectedSource;
        set => SetProperty(ref _selectedSource, value);
    }

    public ICommand AddFileCommand { get; }
    public ICommand AddFolderCommand { get; }
    public ICommand RemoveSourceCommand { get; }
    public ICommand StartCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand ClearEventsCommand { get; }

    public FileWatcherViewModel(IFileWatcherService service)
    {
        _service = service;

        _service.EventReceived += OnEventReceived;

        AddFileCommand = new RelayCommand(AddFile);
        AddFolderCommand = new RelayCommand(AddFolder);
        RemoveSourceCommand = new RelayCommand(RemoveSource, () => SelectedSource != null);
        StartCommand = new RelayCommand(() => _service.Start());
        PauseCommand = new RelayCommand(() => _service.Pause());
        ClearEventsCommand = new RelayCommand(() => Events.Clear());
    }

    private void AddFile()
    {
//        string path? = null; // FilePicker.PickFolder(); // TODO: dein eigenes Utility
//        if (path == null) return;
//
//        var src = new FileWatchSource(path, isDirectory: false, recursive: false);
//        Sources.Add(src);
//        _service.AddSource(src);
    }

    private void AddFolder()
    {
//      string path? = null; // FilePicker.PickFolder(); // TODO: dein eigenes Utility
//      if (path == null) return;
//
//      var src = new FileWatchSource(path, isDirectory: true, recursive: true);
//      Sources.Add(src);
//      _service.AddSource(src);
    }

    private void RemoveSource()
    {
        if (SelectedSource == null) return;

        _service.RemoveSource(SelectedSource);
        Sources.Remove(SelectedSource);
    }

    private void OnEventReceived(object? sender, FileWatchEvent e)
    {
        App.DispatcherQueue.TryEnqueue(() =>
        {
            Events.Add(e);
        });
    }
}

public enum FileWatchEventType
{
    Created,
    Deleted,
    Changed,
    Renamed
}

public class FileWatchSource
{
    public string Path { get; }
    public bool IsDirectory { get; }
    public bool Recursive { get; }

    public FileWatchSource(string path, bool isDirectory, bool recursive)
    {
        Path = path;
        IsDirectory = isDirectory;
        Recursive = recursive;
    }
}

public class FileWatchEvent
{
    public DateTime Timestamp { get; }
    public FileWatchEventType Type { get; }
    public string Path { get; }
    public string? OldPath { get; }
    public long? Size { get; }
    public string? Hash { get; }
    public FileWatchSource Source { get; }

    public FileWatchEvent(DateTime ts, FileWatchEventType type, string path,
        string? oldPath, long? size, string? hash, FileWatchSource src)
    {
        Timestamp = ts;
        Type = type;
        Path = path;
        OldPath = oldPath;
        Size = size;
        Hash = hash;
        Source = src;
    }
}
