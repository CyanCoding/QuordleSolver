using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace QuordleSolver {
    public partial class MainWindow : Window {
        private readonly Label _mainLabel;
        private int round = 0;

        private readonly Letter[] _letters;
        public MainWindow() {
            InitializeComponent();

            _mainLabel = this.FindControl<Label>("MainLabel");

            _letters = new[] {
                this.FindControl<Letter>("A"),
                this.FindControl<Letter>("B"),
                this.FindControl<Letter>("C"),
                this.FindControl<Letter>("D"),
                this.FindControl<Letter>("E"),
                this.FindControl<Letter>("F"),
                this.FindControl<Letter>("G"),
                this.FindControl<Letter>("H"),
                this.FindControl<Letter>("I"),
                this.FindControl<Letter>("J"),
                this.FindControl<Letter>("K"),
                this.FindControl<Letter>("L"),
                this.FindControl<Letter>("M"),
                this.FindControl<Letter>("N"),
                this.FindControl<Letter>("O"),
                this.FindControl<Letter>("P"),
                this.FindControl<Letter>("Q"),
                this.FindControl<Letter>("R"),
                this.FindControl<Letter>("S"),
                this.FindControl<Letter>("T"),
                this.FindControl<Letter>("U"),
                this.FindControl<Letter>("V"),
                this.FindControl<Letter>("W"),
                this.FindControl<Letter>("X"),
                this.FindControl<Letter>("Y"),
                this.FindControl<Letter>("Z"),
            };
        }

        private List<string> illegalLetters;
        private List<string> requiredLetters;
        private void Button_OnClick(object? sender, RoutedEventArgs e) {
            string suggestion = "";
            var alphabet = new[] {
                "a",
                "b",
                "c",
                "d",
                "e",
                "f",
                "g",
                "h",
                "i",
                "j",
                "k",
                "l",
                "m",
                "n",
                "o",
                "p",
                "q",
                "r",
                "s",
                "t",
                "u",
                "v",
                "w",
                "x",
                "y",
                "z"
            };
            
            // Get the configuration of each letter on keyboard
            int[]?[] letterArrangements;
            try {
                letterArrangements = new[] {
                    _letters[0].GetArrangement(),
                    _letters[1].GetArrangement(),
                    _letters[2].GetArrangement(),
                    _letters[3].GetArrangement(),
                    _letters[4].GetArrangement(),
                    _letters[5].GetArrangement(),
                    _letters[6].GetArrangement(),
                    _letters[7].GetArrangement(),
                    _letters[8].GetArrangement(),
                    _letters[9].GetArrangement(),
                    _letters[10].GetArrangement(),
                    _letters[11].GetArrangement(),
                    _letters[12].GetArrangement(),
                    _letters[13].GetArrangement(),
                    _letters[14].GetArrangement(),
                    _letters[15].GetArrangement(),
                    _letters[16].GetArrangement(),
                    _letters[17].GetArrangement(),
                    _letters[18].GetArrangement(),
                    _letters[19].GetArrangement(),
                    _letters[20].GetArrangement(),
                    _letters[21].GetArrangement(),
                    _letters[22].GetArrangement(),
                    _letters[23].GetArrangement(),
                    _letters[24].GetArrangement(),
                    _letters[25].GetArrangement()
                };
            }
            catch (Exception) {
                // Occurs if there are question marks on the board
                _mainLabel.Content = "YOU CANNOT HAVE QUESTION MARKS!";
                return;
            }
            

            // Add letters to illegal list
            for (int i = 0; i < 26; i++) {
                if (letterArrangements[i] == null) {
                    illegalLetters.Add(alphabet[i]);
                }
            }
            
            // Add letters to required list (all sides green)
            
            // Reset the yellow values after recording letter placement
            foreach (var letter in _letters) {
                letter.ResetYellow();
            }

            // Update suggestion
            round++;
            _mainLabel.Content = "Round: " + round + " - Try: " + suggestion;
        }
    }
}