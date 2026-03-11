using MorpionMaui.Models;

namespace MorpionMaui.Tests
{
    public class GameBoardTests
    {
        private GameBoard _board;

        public GameBoardTests()
        {
            _board = new GameBoard();
        }

  
        [Fact]
        public void Play_CaseVide_RetourneTrue()
        {
            bool resultat = _board.Play(0, 0);
            Assert.True(resultat);
        }

  
        [Fact]
        public void Play_CaseOccupee_RetourneFalse()
        {
            _board.Play(0, 0); // X joue en (0,0)
            bool resultat = _board.Play(0, 0); // on rejoue sur la même case
            Assert.False(resultat);
        }


        [Fact]
        public void IsGameWon_LigneHorizontale_RetourneTrue()
        {
            // X joue toute la ligne 0
            _board.Play(0, 0); // X
            _board.Play(1, 0); // O
            _board.Play(0, 1); // X
            _board.Play(1, 1); // O
            _board.Play(0, 2); // X gagne

            Assert.True(_board.IsGameOver);
            Assert.True(_board.Winner.HasValue);
            Assert.Equal('X', _board.Winner.Value);
        }


        [Fact]
        public void IsDraw_PlateauPlein_RetourneTrue()
        {
            // X O X
            // X X O
            // O X O
            _board.Play(0, 0); // X
            _board.Play(0, 1); // O
            _board.Play(0, 2); // X
            _board.Play(1, 1); // O
            _board.Play(1, 0); // X
            _board.Play(2, 0); // O
            _board.Play(1, 2); // X
            _board.Play(2, 2); // O
            _board.Play(2, 1); // X

            Assert.True(_board.IsGameOver);
            Assert.False(_board.Winner.HasValue);
        }


        [Fact]
        public void Reset_ApresUnePartie_PlateauVide()
        {
            _board.Play(0, 0); // X
            _board.Play(1, 1); // O
            _board.Reset();

            Assert.False(_board.IsGameOver);
            Assert.False(_board.Winner.HasValue);
            Assert.Equal('X', _board.CurrentPlayer);
            Assert.True(_board.IsCellEmpty(0, 0));
            Assert.True(_board.IsCellEmpty(1, 1));
        }
    }
}