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

        try
        {
            using var stream = File.OpenRead(path);
            var buffer = new byte[512];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            var sb = new StringBuilder();
            for (int i = 0; i < bytesRead; i += 16)
            {
                // Offset
                sb.Append(i.ToString("X8")).Append("  ");

                // Hex bytes
                for (int j = 0; j < 16; j++)
                {
                    if (i + j < bytesRead)
                        sb.Append(buffer[i + j].ToString("X2")).Append(' ');
                    else
                        sb.Append("   ");
                }

                sb.Append("  ");

                // ASCII view
                for (int j = 0; j < 16 && i + j < bytesRead; j++)
                {
                    byte b = buffer[i + j];
                    sb.Append(b >= 32 && b <= 126 ? (char)b : '.');
                }

                sb.AppendLine();
            }

            HexDump = sb.ToString();
        }
        catch (Exception ex)
        {
            HexDump = $"TODO: GuruMeditation: Can't open file:\n{ex.Message}";
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
