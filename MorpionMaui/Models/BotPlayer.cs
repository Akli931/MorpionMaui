namespace MorpionMaui.Models
{
    public class BotPlayer
    {
        private readonly Random _random = new();

        public (int row, int col) ChooseMove(GameBoard board)
        {

            var casesVides = new List<(int row, int col)>();

            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (board.IsCellEmpty(r, c))
                        casesVides.Add((r, c));

            int index = _random.Next(casesVides.Count);
            return casesVides[index];
        }
    }
}