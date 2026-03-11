using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MorpionMaui.Models;

namespace MorpionMaui.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        private readonly GameBoard _board = new();
        private readonly BotPlayer _bot = new();
        private readonly FakeGameHistory _history = FakeGameHistory.Instance;

        private bool _isVsBot = false;

        [ObservableProperty] private bool modeVisible = true;
        [ObservableProperty] private bool jeuVisible = false;
        [ObservableProperty] private string statusText = "Choisis ton mode de jeu";
        [ObservableProperty] private string cell00 = "";
        [ObservableProperty] private string cell01 = "";
        [ObservableProperty] private string cell02 = "";
        [ObservableProperty] private string cell10 = "";
        [ObservableProperty] private string cell11 = "";
        [ObservableProperty] private string cell12 = "";
        [ObservableProperty] private string cell20 = "";
        [ObservableProperty] private string cell21 = "";
        [ObservableProperty] private string cell22 = "";
        [ObservableProperty] private int victoires;
        [ObservableProperty] private int defaites;
        [ObservableProperty] private int nuls;

        public GameViewModel()
        {
            Victoires = _history.Victoires;
            Defaites = _history.Defaites;
            Nuls = _history.Nuls;
        }

        [RelayCommand]
        private void ChoisirMode(string param)
        {
            _isVsBot = param == "bot";
            ModeVisible = false;
            JeuVisible = true;
            StatusText = "Tour du joueur X";
        }

        [RelayCommand]
        private void NouvellePartie()
        {
            _board.Reset();
            ModeVisible = true;
            JeuVisible = false;
            StatusText = "Choisis ton mode de jeu";

            Cell00 = ""; Cell01 = ""; Cell02 = "";
            Cell10 = ""; Cell11 = ""; Cell12 = "";
            Cell20 = ""; Cell21 = ""; Cell22 = "";
        }

        [RelayCommand]
        private async Task Play(string param)
        {
            var parts = param.Split(',');
            int row = int.Parse(parts[0]);
            int col = int.Parse(parts[1]);

            bool valide = _board.Play(row, col);
            if (!valide) return;

            SetCell(row, col, _board.GetCell(row, col).ToString());

            if (_board.IsGameOver)
            {
                HandleEndGame();
                return;
            }

            if (_isVsBot)
            {
                StatusText = "Bot réfléchit...";
                await Task.Delay(500);

                var (botRow, botCol) = _bot.ChooseMove(_board);
                _board.Play(botRow, botCol);
                SetCell(botRow, botCol, _board.GetCell(botRow, botCol).ToString());

                if (_board.IsGameOver)
                {
                    HandleEndGame();
                    return;
                }
            }

            StatusText = $"Tour du joueur {_board.CurrentPlayer}";
        }

        private void HandleEndGame()
        {
            if (_board.Winner.HasValue)
            {
                if (_isVsBot)
                {
                    if (_board.Winner.Value == 'X')
                    {
                        StatusText = "Tu as gagné !";
                        _history.AjouterPartie("Victoire");
                    }
                    else
                    {
                        StatusText = "Le bot a gagné !";
                        _history.AjouterPartie("Défaite");
                    }
                }
                else
                {
                    StatusText = $"Joueur {_board.Winner.Value} a gagné !";
                }
            }
            else
            {
                StatusText = "Match nul !";
                if (_isVsBot) _history.AjouterPartie("Nul");
            }

            Victoires = _history.Victoires;
            Defaites = _history.Defaites;
            Nuls = _history.Nuls;
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