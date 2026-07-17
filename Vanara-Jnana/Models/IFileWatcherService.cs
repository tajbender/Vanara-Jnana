using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jnana.Models;

public interface IFileWatcherService
{
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
