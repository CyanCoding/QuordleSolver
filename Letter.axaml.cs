using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace QuordleSolver; 

public partial class Letter : UserControl {
    private readonly IBrush? _yellowBrush  = new BrushConverter().ConvertFrom("#ffcc00") as Brush;
    private readonly IBrush? _greenBrush  = new BrushConverter().ConvertFrom("#00cc88") as Brush;
    private readonly IBrush? _greyBrush  = new BrushConverter().ConvertFrom("#9ca3af") as Brush;

    private readonly Label _text;
    public Letter() {
        InitializeComponent();

        _text = this.FindControl<Label>("Text");
    }
    
    /// <summary>
    /// Gets or sets the assignment type.
    /// </summary>
    public string? LetterText {
        get => _text.Content.ToString();
        set => _text.Content = value;
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    private void Border_Pressed(object? sender, PointerPressedEventArgs e) {
        var border = (Border)sender!;
        if (Equals(border.BorderBrush, _greyBrush)) {
            border.BorderBrush = _yellowBrush;
            return;
        }
        if (Equals(border.BorderBrush, _yellowBrush)) {
            border.BorderBrush = _greenBrush;
            return;
        }
        if (Equals(border.BorderBrush, _greenBrush)) {
            border.BorderBrush = _greyBrush;
            return;
        }
        
        // If we got here it means none triggered (e.g. it's in the normal border state) so we set to yellow
        border.BorderBrush = _yellowBrush;
    }
}