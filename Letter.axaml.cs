using System;
using System.Security.Cryptography;
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
    
    private readonly IBrush? _disabledBrush  = new BrushConverter().ConvertFrom("#155e75") as Brush;
    private readonly IBrush? _enabledBrush  = new BrushConverter().ConvertFrom("#9ca3af") as Brush;

    private readonly Label _text;
    private readonly Border _coverBorder;
    private readonly Border[] _borders;
    
    public Letter() {
        InitializeComponent();

        _text = this.FindControl<Label>("Text");
        _coverBorder = this.FindControl<Border>("CoverBorder");

        _borders = new[] {
            this.FindControl<Border>("Border1"),
            this.FindControl<Border>("Border2"),
            this.FindControl<Border>("Border3"),
            this.FindControl<Border>("Border4")
        };
        

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

    /// <summary>
    /// Returns an int[4] array of the border status on the letter.
    /// NULL = disabled letter
    /// 0 = grey border
    /// 1 = yellow border
    /// 2 = green border
    /// </summary>
    internal int[]? GetArrangement() {
        // The letter isn't anywhere
        if (Equals(_text.Background, _disabledBrush)) {
            return null;
        }

        var sides = new int[4];

        for (int i = 0; i < 4; i++) {
            sides[i] = 0;
            var label = (Label)_borders[i].Child;
            if (label.Content.ToString() == "?") {
                throw new Exception("Cannot have question marks!");
            }
            
            if (Equals(_borders[i].BorderBrush, _yellowBrush)) {
                sides[i] = 1;
            }
            else if (Equals(_borders[i].BorderBrush, _greenBrush)) {
                sides[i] = 2;
            }
        }

        return sides;
    }

    /// <summary>
    /// Sets each yellow border to '?' once it has stored the current value
    /// </summary>
    internal void ResetYellow() {
        for (int i = 0; i < 4; i++) {
            if (Equals(_borders[i].BorderBrush, _yellowBrush)) {
                var label = (Label)_borders[i].Child;
                label.Content = "?";
            }
        }
    }

    private void Border_Pressed(object? sender, PointerPressedEventArgs e) {
        var border = (Border)sender!;
        if (Equals(border.BorderBrush, _greyBrush)) {
            border.BorderBrush = _yellowBrush;
            var label = (Label)border.Child;
            label.Content = 1;
            return;
        }
        if (Equals(border.BorderBrush, _yellowBrush)) {
            var label = (Label)border.Child;
            if (label.Content.ToString() == "  " || label.Content.ToString() == "?") {
                label.Content = 1;
                return;
            }
            if (int.Parse(label.Content.ToString() ?? "0") < 5) {
                label.Content = int.Parse(label.Content.ToString() ?? "0") + 1;
                return;
            }
            border.BorderBrush = _greenBrush;
            label.Content = "1";
            return;
        }
        if (Equals(border.BorderBrush, _greenBrush)) {
            var label = (Label)border.Child;
            if (int.Parse(label.Content.ToString() ?? "0") < 5) {
                label.Content = int.Parse(label.Content.ToString() ?? "0") + 1;
                return;
            }

            label.Content = "  ";
            border.BorderBrush = _greyBrush;
            return;
        }
        
        // If we got here it means none triggered (e.g. it's in the normal border state) so we set to yellow
        border.BorderBrush = _yellowBrush;
        var label1 = (Label)border.Child;
        label1.Content = 1;
    }

    private void Text_OnPointerPressed(object? sender, PointerPressedEventArgs e) {
        if (Equals(_text.Background, _disabledBrush)) {
            _text.Background = _enabledBrush;
            _coverBorder.IsVisible = false;
            return;
        }
        _text.Background = _disabledBrush;
        _coverBorder.IsVisible = true;
    }
}