using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Diagnostics;

namespace Jnana.Vanara.Controls;

public sealed partial class FeatureTile : UserControl
{
    // Dependency Properties
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(FeatureTile),
            new PropertyMetadata(string.Empty, (d, e) =>
            {
                ((FeatureTile)d).TitleElement.Text = (string)e.NewValue;
            }));

    public static readonly DependencyProperty SubtitleProperty =
        DependencyProperty.Register(nameof(Subtitle), typeof(string), typeof(FeatureTile),
            new PropertyMetadata(string.Empty, (d, e) =>
            {
                ((FeatureTile)d).SubtitleElement.Text = (string)e.NewValue;
            }));

    public static readonly DependencyProperty IconSourceProperty =
        DependencyProperty.Register(nameof(IconSource), typeof(IconSource), typeof(FeatureTile),
            new PropertyMetadata(null, (d, e) =>
            {
                ((FeatureTile)d).IconSource = (IconSource)e.NewValue;
            }));

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(StandardUICommand), typeof(FeatureTile),
            new PropertyMetadata(null, (d, e) =>
            {
                ((FeatureTile)d).Command = (StandardUICommand)e.NewValue;
            }));

    public event EventHandler Click;
    public StandardUICommand Command
    {
        get => (StandardUICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    public IconSource IconSource
    {
        get => (IconSource)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }
    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeatureTile"/> class, setting up the UI components and pointer interactions.
    /// </summary>
    public FeatureTile()
    {
        InitializeComponent();
        SetupInteractions();

        this.Click += (_, __) => Debug.WriteLine($"FeatureTile.Click(Title='{Title}', Subtitle='{Subtitle}')");
    }

    /// <summary>
    /// Sets up the pointer interactions for the FeatureTile, including hover and click effects.
    /// </summary>
    private void SetupInteractions()
    {
        Root.PointerEntered += (_, __) =>
        {
            ScaleTransform.ScaleX = 1.03;
            ScaleTransform.ScaleY = 1.03;
            Root.Opacity = 0.95;
        };

        Root.PointerExited += (_, __) =>
        {
            ScaleTransform.ScaleX = 1.0;
            ScaleTransform.ScaleY = 1.0;
            Root.Opacity = 1.0;
        };

        Root.PointerPressed += (_, __) =>
        {
            ScaleTransform.ScaleX = 0.97;
            ScaleTransform.ScaleY = 0.97;
        };

        Root.PointerReleased += (_, __) =>
        {
            ScaleTransform.ScaleX = 1.03;
            ScaleTransform.ScaleY = 1.03;
            Click?.Invoke(this, EventArgs.Empty);
        };
    }
}

/// <summary>
/// Provides extension methods for the <see cref="FeatureTile"/> class, 
/// allowing for easier access and manipulation of its properties.
/// </summary>
public static class FeatureTileExtensions
{
    public static StandardUICommand GetCommand(this FeatureTile featureTile)
    {
        return featureTile.Command;
    }

    public static FeatureTile SetCommand(this FeatureTile featureTile, StandardUICommand command)
    {
        Debug.Assert(featureTile != null, "FeatureTileExtensions.SetCommand: featureTile is null.");
        Debug.Assert(command != null, "FeatureTileExtensions.SetCommand: command is null.");
        Debug.Assert(command is StandardUICommand, "FeatureTileExtensions.SetCommand: command is not a StandardUICommand.");

        featureTile.Command = command;

        return featureTile;
    }

    public static FeatureTile SetIconSource(this FeatureTile featureTile, IconSource icon)
    {
        Debug.Assert(featureTile != null, "FeatureTileExtensions.SetIcon: featureTile is null.");
        Debug.Assert(icon != null, "FeatureTileExtensions.SetIcon: icon is null.");
        Debug.Assert(icon is IconSource, "FeatureTileExtensions.SetIcon: icon is not a IconSource.");

        featureTile.IconSource = icon;

        return featureTile;
    }

    public static FeatureTile SetTitle(this FeatureTile featureTile, string title)
    {
        Debug.Assert(featureTile != null, "FeatureTileExtensions.SetTitle: featureTile is null.");
        Debug.Assert(!string.IsNullOrEmpty(title), "FeatureTileExtensions.SetTitle: title is null or empty.");
        Debug.Assert(title is string, "FeatureTileExtensions.SetTitle: title is not a string.");

        featureTile.Title = title;

        return featureTile;
    }

    public static FeatureTile SetSubtitle(this FeatureTile featureTile, string subtitle)
    {
        Debug.Assert(featureTile != null, "FeatureTileExtensions.SetSubtitle: featureTile is null.");
        Debug.Assert(!string.IsNullOrEmpty(subtitle), "FeatureTileExtensions.SetSubtitle: subtitle is null or empty.");
        Debug.Assert(subtitle is string, "FeatureTileExtensions.SetSubtitle: subtitle is not a string.");

        featureTile.Subtitle = subtitle;

        return featureTile;
    }
}
