using MorpionMaui.Models;

namespace MorpionMaui;

public partial class MainPage : ContentPage
{
    private readonly GameBoard _board = new();
    private Button[,] _buttons = null!;

    public MainPage()
    {
        InitializeComponent();

        _buttons = new Button[3, 3]
        {
            { Btn00, Btn01, Btn02 },
            { Btn10, Btn11, Btn12 },
            { Btn20, Btn21, Btn22 }
        };
    }

    private async void OnCellClicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var parts = btn.CommandParameter.ToString()!.Split(',');
        int row = int.Parse(parts[0]);
        int col = int.Parse(parts[1]);

        bool valide = _board.Play(row, col);
        if (!valide) return;

        string symbole = _board.GetCell(row, col).ToString();
        btn.Text = symbole;
        btn.TextColor = symbole == "X"
            ? Color.FromArgb("#e94560")
            : Color.FromArgb("#4fc3f7");
        btn.IsEnabled = false;

        if (_board.IsGameOver)
        {
            await HandleGameOver();
            return;
        }

        StatusLabel.Text = $"Tour du joueur {_board.CurrentPlayer}";
    }

    private async Task HandleGameOver()
    {
        if (_board.Winner.HasValue)
        {
            var casesGagnantes = _board.GetWinningCells();
            foreach ((int r, int c) in casesGagnantes)
                _buttons[r, c].BackgroundColor = Color.FromArgb("#2a9d8f");

            StatusLabel.Text = $"Joueur {_board.Winner} a gagné !";
            await DisplayAlertAsync("Victoire !", $"Le joueur {_board.Winner} a gagné !", "OK");
        }
        else
        {
            StatusLabel.Text = "Match nul !";
            await DisplayAlertAsync("Match nul", "Personne ne gagne cette fois !", "OK");
        }
    }
    private void OnResetClicked(object sender, EventArgs e)
    {
        ResetBoard();
    }

    private void ResetBoard()
    {
        _board.Reset();
        StatusLabel.Text = "Tour du joueur X";

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                _buttons[r, c].Text = string.Empty;
                _buttons[r, c].IsEnabled = true;
                _buttons[r, c].BackgroundColor = Color.FromArgb("#16213e");
            }
        }
    }
}