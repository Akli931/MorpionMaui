namespace MorpionMaui.Models
{
    public class GameBoard
    {
        private char[,] plateau;

        public char CurrentPlayer { get; private set; } = 'X';
        public bool IsGameOver { get; private set; } = false;
        public char? Winner { get; private set; } = null;

        public GameBoard()
        {
            plateau = new char[3, 3]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' }
            };
        }

        public bool IsCellEmpty(int row, int col)
        {
            return plateau[row, col] == ' ';
        }

        public void PlayMove(int row, int col, char player)
        {
            plateau[row, col] = player;
        }

        public bool IsGameWon(char player)
        {
            bool ligne = Enumerable.Range(0, 3)
                .Any(i => Enumerable.Range(0, 3)
                    .All(j => plateau[i, j] == player));

            bool colonne = Enumerable.Range(0, 3)
                .Any(j => Enumerable.Range(0, 3)
                    .All(i => plateau[i, j] == player));

            bool diag1 = Enumerable.Range(0, 3)
                .All(i => plateau[i, i] == player);

            bool diag2 = Enumerable.Range(0, 3)
                .All(i => plateau[i, 2 - i] == player);

            return ligne || colonne || diag1 || diag2;
        }

        public bool IsDraw()
        {
            return plateau.Cast<char>().All(c => c != ' ');
        }

    
        public bool Play(int row, int col)
        {
            if (IsGameOver) return false;
            if (!IsCellEmpty(row, col)) return false;

            PlayMove(row, col, CurrentPlayer);

            if (IsGameWon(CurrentPlayer))
            {
                Winner = CurrentPlayer;
                IsGameOver = true;
            }
            else if (IsDraw())
            {
                IsGameOver = true;
                Winner = null;
            }
            else
            {
                CurrentPlayer = CurrentPlayer == 'X' ? 'O' : 'X';
            }

            return true;
        }

        public char GetCell(int row, int col)
        {
            return plateau[row, col];
        }

        public List<(int row, int col)> GetWinningCells()
        {
            for (int r = 0; r < 3; r++)
                if (plateau[r, 0] != ' ' && plateau[r, 0] == plateau[r, 1] && plateau[r, 1] == plateau[r, 2])
                    return new() { (r, 0), (r, 1), (r, 2) };

            for (int c = 0; c < 3; c++)
                if (plateau[0, c] != ' ' && plateau[0, c] == plateau[1, c] && plateau[1, c] == plateau[2, c])
                    return new() { (0, c), (1, c), (2, c) };

            if (plateau[0, 0] != ' ' && plateau[0, 0] == plateau[1, 1] && plateau[1, 1] == plateau[2, 2])
                return new() { (0, 0), (1, 1), (2, 2) };

            if (plateau[0, 2] != ' ' && plateau[0, 2] == plateau[1, 1] && plateau[1, 1] == plateau[2, 0])
                return new() { (0, 2), (1, 1), (2, 0) };

            return new();
        }


        public void Reset()
        {
            plateau = new char[3, 3]
            {
        { ' ', ' ', ' ' },
        { ' ', ' ', ' ' },
        { ' ', ' ', ' ' }
            };
            CurrentPlayer = 'X';
            IsGameOver = false;
            Winner = null;
        }
    }
}