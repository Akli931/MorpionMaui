namespace MorpionMaui.Models
{
    public class GameResult
    {
        public string Resultat { get; set; } 
        public string Adversaire { get; set; } 
        public DateTime Date { get; set; }   

        public GameResult(string resultat, string adversaire, DateTime date)
        {
            Resultat = resultat;
            Adversaire = adversaire;
            Date = date;
        }
    }
}