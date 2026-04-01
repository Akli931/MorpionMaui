using SQLite;

namespace MorpionMaui.Models
{
    [Table("GameResults")]
    public class GameResult
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Resultat { get; set; } = "";
        public string Adversaire { get; set; } = "";
        public DateTime Date { get; set; }

        public GameResult() { }

        public GameResult(string resultat, string adversaire, DateTime date)
        {
            Resultat = resultat;
            Adversaire = adversaire;
            Date = date;
        }
    }
}