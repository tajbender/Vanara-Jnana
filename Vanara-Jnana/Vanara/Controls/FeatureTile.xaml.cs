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

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(string), typeof(FeatureTile),
            new PropertyMetadata(string.Empty, (d, e) =>
            {
                ((FeatureTile)d).IconElement.Glyph = (string)e.NewValue;
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
    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
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
