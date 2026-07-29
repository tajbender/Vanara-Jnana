using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanara.WinUI.Extensions.Helpers;

public enum MessageBoxType
{
    Info,
    Warning,
    Error,
    Confirm,
    YesNo,
    RetryCancel,
    Debug,
    Workbench
}

public enum MessageBoxResult
{
    None,
    Ok,
    Cancel,
    Yes,
    No,
    Retry
}

public static class MessageBox
{
    public static async Task<MessageBoxResult> ShowAsync(
        string message,
        string title,
        MessageBoxType type,
        XamlRoot xamlRoot)
    {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = message,
            XamlRoot = xamlRoot
        };

        ConfigureButtons(dialog, type);
        ApplyWorkbenchStyle(dialog, type);

        var result = await dialog.ShowAsync();
        return MapResult(result, type);
    }

    private static void ConfigureButtons(ContentDialog dialog, MessageBoxType type)
    {
        switch (type)
        {
            case MessageBoxType.Info:
            case MessageBoxType.Warning:
            case MessageBoxType.Error:
            case MessageBoxType.Workbench:
            case MessageBoxType.Debug:
                dialog.CloseButtonText = "OK";
                break;

            case MessageBoxType.Confirm:
                dialog.PrimaryButtonText = "OK";
                dialog.CloseButtonText = "Abbrechen";
                break;

            case MessageBoxType.YesNo:
                dialog.PrimaryButtonText = "Ja";
                dialog.SecondaryButtonText = "Nein";
                break;

            case MessageBoxType.RetryCancel:
                dialog.PrimaryButtonText = "Wiederholen";
                dialog.CloseButtonText = "Abbrechen";
                break;
        }
    }

    private static MessageBoxResult MapResult(ContentDialogResult result, MessageBoxType type)
    {
        return type switch
        {
            MessageBoxType.YesNo => result switch
            {
                ContentDialogResult.Primary => MessageBoxResult.Yes,
                ContentDialogResult.Secondary => MessageBoxResult.No,
                _ => MessageBoxResult.Cancel
            },
            MessageBoxType.RetryCancel => result switch
            {
                ContentDialogResult.Primary => MessageBoxResult.Retry,
                _ => MessageBoxResult.Cancel
            },
            MessageBoxType.Confirm => result switch
            {
                ContentDialogResult.Primary => MessageBoxResult.Ok,
                _ => MessageBoxResult.Cancel
            },
            _ => MessageBoxResult.Ok
        };
    }

    private static void ApplyWorkbenchStyle(ContentDialog dialog, MessageBoxType type)
    {
        // Hier später: Workbench‑Theme, Icons, Farben, OS‑Look
        // z.B. via Style/ResourceKey oder Attached Properties
    }
}


public static class MessageBoxHelpers
{
    public static Task<MessageBoxResult> Info(
        string message, string title, XamlRoot root) =>
        MessageBox.ShowAsync(message, title, MessageBoxType.Info, root);

    public static Task<MessageBoxResult> Warning(
        string message, string title, XamlRoot root) =>
        MessageBox.ShowAsync(message, title, MessageBoxType.Warning, root);

    public static Task<MessageBoxResult> Error(
        string message, string title, XamlRoot root) =>
        MessageBox.ShowAsync(message, title, MessageBoxType.Error, root);

    public static Task<MessageBoxResult> Confirm(
        string message, string title, XamlRoot root) =>
        MessageBox.ShowAsync(message, title, MessageBoxType.Confirm, root);

    public static Task<MessageBoxResult> YesNo(
        string message, string title, XamlRoot root) =>
        MessageBox.ShowAsync(message, title, MessageBoxType.YesNo, root);

    public static Task<MessageBoxResult> RetryCancel(
        string message, string title, XamlRoot root) =>
        MessageBox.ShowAsync(message, title, MessageBoxType.RetryCancel, root);

    public static Task<MessageBoxResult> Debug(
        string message, string title, XamlRoot root) =>
        MessageBox.ShowAsync(message, title, MessageBoxType.Debug, root);

    public static Task<MessageBoxResult> Workbench(
        string message, string title, XamlRoot root) =>
        MessageBox.ShowAsync(message, title, MessageBoxType.Workbench, root);
}
