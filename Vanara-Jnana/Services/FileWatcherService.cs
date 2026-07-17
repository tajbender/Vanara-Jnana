using System;
using System.Collections.Generic;
using System.IO;
using Jnana.Models;
using Jnana.Services;

namespace Jnana.Services.FileWatcher;

public class FileWatcherService : IFileWatcherService
{
    private readonly List<FileSystemWatcher> _watchers = new();

    public event EventHandler<FileWatchEvent>? EventReceived;

    public void AddSource(FileWatchSource source)
    {
        var watcher = new FileSystemWatcher
        {
            Path = source.IsDirectory ? source.Path : Path.GetDirectoryName(source.Path)!,
            Filter = source.IsDirectory ? "*.*" : Path.GetFileName(source.Path),
            IncludeSubdirectories = source.Recursive,
            NotifyFilter =
                NotifyFilters.FileName |
                NotifyFilters.DirectoryName |
                NotifyFilters.LastWrite |
                NotifyFilters.Size
        };

        watcher.Created += (_, e) => RaiseEvent(FileWatchEventType.Created, e.FullPath, null, source);
        watcher.Deleted += (_, e) => RaiseEvent(FileWatchEventType.Deleted, e.FullPath, null, source);
        watcher.Changed += (_, e) => RaiseEvent(FileWatchEventType.Changed, e.FullPath, null, source);
        watcher.Renamed += (_, e) => RaiseEvent(FileWatchEventType.Renamed, e.FullPath, e.OldFullPath, source);

        _watchers.Add(watcher);
    }

    public void RemoveSource(FileWatchSource source)
    {
        foreach (var w in _watchers.ToArray())
        {
            if (MatchesSource(w, source))
            {
                w.EnableRaisingEvents = false;
                w.Dispose();
                _watchers.Remove(w);
            }
        }
    }

    public void Start()
    {
        foreach (var w in _watchers)
            w.EnableRaisingEvents = true;
    }

    public void Pause()
    {
        foreach (var w in _watchers)
            w.EnableRaisingEvents = false;
    }

    private void RaiseEvent(FileWatchEventType type, string path, string? oldPath, FileWatchSource src)
    {
        EventReceived?.Invoke(this, new FileWatchEvent(
            DateTime.Now,
            type,
            path,
            oldPath,
            null,
            null,
            src));
    }

    private static bool MatchesSource(FileSystemWatcher w, FileWatchSource src)
    {
        if (src.IsDirectory)
            return w.Path.Equals(src.Path, StringComparison.OrdinalIgnoreCase);

        return w.Path.Equals(Path.GetDirectoryName(src.Path), StringComparison.OrdinalIgnoreCase)
            && w.Filter.Equals(Path.GetFileName(src.Path), StringComparison.OrdinalIgnoreCase);
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
