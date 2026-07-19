using Microsoft.UI.Xaml;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace Jnana.Services;

public sealed class SystemSnapshot
{
    public DateTime Timestamp { get; init; }
    public string MachineName { get; init; }
    public string OSDescription { get; init; }
    public string WinAppSdkVersion { get; init; }
    public string WorkbenchVersion { get; init; }

    public double CpuLoadPercent { get; init; }
    public long MemoryWorkingSetMB { get; init; }

    public IReadOnlyDictionary<string, string> Assemblies { get; init; }
}

public interface ISysInfoProvider
{
    Task<SystemSnapshot> CollectAsync();
}



public sealed class SysInfoProvider : ISysInfoProvider
{
    public async Task<SystemSnapshot> CollectAsync()
    {
        var process = Process.GetCurrentProcess();

        return new SystemSnapshot
        {
            Timestamp = DateTime.Now,
            MachineName = Environment.MachineName,
            OSDescription = RuntimeInformation.OSDescription,
            WinAppSdkVersion = "3.0.0.0", // später dynamisch
            WorkbenchVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),

            //            CpuLoadPercent = await GetCpuLoadAsync(),
            MemoryWorkingSetMB = process.WorkingSet64 / (1024 * 1024),

            //            Assemblies = AppDomain.CurrentDomain.GetAssemblies()
            //                .Where(a => !a.IsDynamic)
            //                .ToDictionary(
            //                    a => a.GetName().Name,
            //                    a => a.GetName().Version?.ToString() ?? "n/a"
            //                )
        };
    }

    //    private async Task<double> GetCpuLoadAsync()
    //    {
    //        using var cpu = new PerformanceCounter(
    //            $"Processor", $"% Processor Time", $"_Total");
    //        _ = cpu.NextValue();
    //        await Task.Delay(250);
    //        return Math.Round(cpu.NextValue(), 1);
    //    }
}

public interface ISnapshotWriter
{
    Task<string> WriteAsync(SystemSnapshot snapshot);
}

public sealed class JsonSnapshotWriter : ISnapshotWriter
{
    private readonly string _folder;

    public JsonSnapshotWriter()
    {
        _folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Vanara",
            "Snapshots");

        Directory.CreateDirectory(_folder);
    }

    public async Task<string> WriteAsync(SystemSnapshot snapshot)
    {
        string fileName = $"snapshot_{snapshot.Timestamp:yyyyMMdd_HHmmss}.json";
        string path = Path.Combine(_folder, fileName);

        var json = JsonSerializer.Serialize(snapshot, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(path, json);
        return path;
    }
}

public sealed class SnapshotManager
{
    private readonly ISysInfoProvider _provider;
    private readonly ISnapshotWriter _writer;

    public SnapshotManager(ISysInfoProvider provider, ISnapshotWriter writer)
    {
        _provider = provider;
        _writer = writer;
    }

    public async Task<string> TakeSnapshotAsync()
    {
        var snapshot = await _provider.CollectAsync();
        return await _writer.WriteAsync(snapshot);
    }


    private async void OnTakeSnapshotClicked(object sender, RoutedEventArgs e)
    {
        var manager = new SnapshotManager(
            new SysInfoProvider(),
            new JsonSnapshotWriter());

        string path = await manager.TakeSnapshotAsync();

        //        StatusBar.ShowMessage($"Snapshot gespeichert: {path}");
    }
}
//protected override async void OnLaunched(LaunchActivatedEventArgs args)
//{
//    var manager = new SnapshotManager(
//        new SysInfoProvider(),
//        new JsonSnapshotWriter());
//
//    _ = manager.TakeSnapshotAsync(); // Fire & forget
//
//    base.OnLaunched(args);
//}
