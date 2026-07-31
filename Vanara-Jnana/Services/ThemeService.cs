using Jnana;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jnana.App;

namespace Vanara_Jnana.exe.Services;

public enum AppTheme
{
    Light,
    Dark,
    System
}

public class ThemeService
{
    public AppTheme CurrentTheme { get; private set; } = AppTheme.System;

    public event EventHandler<AppTheme>? ThemeChanged;

    public void SetTheme(AppTheme theme)
    {
        CurrentTheme = theme;
        ApplyTheme(theme);
        ThemeChanged?.Invoke(this, theme);
    }

    private void ApplyTheme(AppTheme theme)
    {
//        var root = (FrameworkElement)App.MainWindow.Content;
//
//        root.RequestedTheme = theme switch
//        {
//            AppTheme.Light => ElementTheme.Light,
//            AppTheme.Dark => ElementTheme.Dark,
//            _ => ElementTheme.Default
//        };
    }
}
