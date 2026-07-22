using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Vanara_Jnana.exe.ViewModels;

public class HandleInfo
{
    public int ProcessId { get; set; }
    public string ProcessName { get; set; }
    public string HandleType { get; set; }
    public string FilePath { get; set; }
}


public class HandleInspectorViewModel : INotifyPropertyChanged
{
    private string _queryPath;
    private ObservableCollection<HandleInfo> _results = new();

    public string QueryPath
    {
        get => _queryPath;
        set { _queryPath = value; OnPropertyChanged(nameof(QueryPath)); }
    }

    public ObservableCollection<HandleInfo> Results
    {
        get => _results;
        set { _results = value; OnPropertyChanged(nameof(Results)); }
    }

    public ICommand ScanCommand { get; }
    public ICommand KillProcessCommand { get; }

    public HandleInspectorViewModel()
    {
        ScanCommand = new RelayCommand(Scan);
        //KillProcessCommand = new RelayCommand(KillProcess);
    }

    private void Scan()
    {
        Results.Clear();

        // später: echte Handle-Scan-Logik
        // jetzt: Dummy-Daten für UI-Test

        Results.Add(new HandleInfo
        {
            ProcessId = 1234,
            ProcessName = "explorer.exe",
            HandleType = "File",
            FilePath = QueryPath
        });
    }

    private void KillProcess(object obj)
    {
        if (obj is HandleInfo info)
        {
            try
            {
                Process.GetProcessById(info.ProcessId).Kill();
            }
            catch { }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
