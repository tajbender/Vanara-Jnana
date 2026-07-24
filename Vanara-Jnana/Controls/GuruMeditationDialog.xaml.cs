using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace Jnana.Controls;

public sealed partial class GuruMeditationDialog : ContentDialog
{
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public string StackTrace { get; set; }
    public string DumpFilePath { get; set; }

    public GuruMeditationDialog(Exception ex, string dumpPath)
    {
        // TODO: this.InitializeComponent();

        DumpFilePath = dumpPath ?? "";
        ErrorCode = $"GURU MEDITATION #{ex.HResult:X8}";
        ErrorMessage = ex.Message;
        StackTrace = ex.ToString();
    }

    /// <summary>
    /// Show the Guru Meditation dialog asynchronously.
    /// </summary>
    public void ShowDialogAsync()
    {
        IAsyncOperation<ContentDialogResult> asyncOperation = this.ShowAsync();
    }


    public void CloseDialog()
    {
        this.Hide();
    }

    public void SetDumpFilePath(string dumpPath)
    {
        // Set the dump file path for the dialog
        // TODO: Use property binding to display the dump file path in the dialog
        DumpFilePath = dumpPath;
    }

    /// <summary>
    /// Tie the Guru Meditation dialog to the application to catch unhandled exceptions and display the dialog.
    /// </summary>
    public void AttachToApp()
    {
        // add. the event hook here...



        // Attach the dialog to the application
        // TODO:  Application.Current.UnhandledException += (sender, e) =>
        //        {   this.ErrorCode = $"GURU MEDITATION #{e.Exception.HResult:X8}";
        //            this.ErrorMessage = e.Exception.Message;
        //            this.StackTrace = e.Exception.ToString();
        //            this.ShowDialog(); }
        // App.Current.UnhandledException += (sender, e) =>
        // {
        //     var dialog = new GuruMeditationDialog(e.Exception);
        //     _ = dialog.ShowAsync();
        //     e.Handled = true;
        // };
         
    }
}


/*
 ICE CREAM! ICE CREAM!
A delightful malfunction has occurred.
Meditation recommended.

 */