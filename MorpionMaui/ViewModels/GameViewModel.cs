using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MorpionMaui.Models;

namespace MorpionMaui.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        private readonly GameBoard _board = new();

        [ObservableProperty]
        private string statusText = "Tour du joueur X";

        [ObservableProperty]
        private string cell00 = "";
        [ObservableProperty]
        private string cell01 = "";
        [ObservableProperty]
        private string cell02 = "";
        [ObservableProperty]
        private string cell10 = "";
        [ObservableProperty]
        private string cell11 = "";
        [ObservableProperty]
        private string cell12 = "";
        [ObservableProperty]
        private string cell20 = "";
        [ObservableProperty]
        private string cell21 = "";
        [ObservableProperty]
        private string cell22 = "";

        [RelayCommand]
        private void Play(string param)
        {

            var parts = param.Split(',');
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            bool valide = _board.Play(row, col);
            if (!valide) return;


            string symbole = _board.GetCell(row, col).ToString();
            SetCell(row, col, symbole);

            if (_board.IsGameOver)
            {
                if (_board.Winner.HasValue)
                    StatusText = $"Joueur {_board.Winner} a gagné !";
                else
                    StatusText = "Match nul !";

                return;
            }

            StatusText = $"Tour du joueur {_board.CurrentPlayer}";
        }


        private void SetCell(int row, int col, string valeur)
        {
            switch (row, col)
            {
                case (0, 0): Cell00 = valeur; break;
                case (0, 1): Cell01 = valeur; break;
                case (0, 2): Cell02 = valeur; break;
                case (1, 0): Cell10 = valeur; break;
                case (1, 1): Cell11 = valeur; break;
                case (1, 2): Cell12 = valeur; break;
                case (2, 0): Cell20 = valeur; break;
                case (2, 1): Cell21 = valeur; break;
                case (2, 2): Cell22 = valeur; break;
            }
        }
    }
}