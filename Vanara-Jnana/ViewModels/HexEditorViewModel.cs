using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanara_Jnana.exe.ViewModels;

public class HexEditorViewModel : INotifyPropertyChanged
{
    private string _filePath;
    private string _hexDump;

    public string FilePath
    {
        get => _filePath;
        set { _filePath = value; OnPropertyChanged(nameof(FilePath)); }
    }

    public string HexDump
    {
        get => _hexDump;
        set { _hexDump = value; OnPropertyChanged(nameof(HexDump)); }
    }

    public void LoadFile(string path)
    {
        FilePath = path;

        // Stub: später echter Hex-Dump
        HexDump = "00 11 22 33 44 55 66 77 88 99 AA BB CC DD EE FF\n" +
                  "Hex-View kommt hier hin...";
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
