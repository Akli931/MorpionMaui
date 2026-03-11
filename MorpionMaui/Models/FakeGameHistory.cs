namespace MorpionMaui.Models
{
    public class FakeGameHistory
    {

        private static FakeGameHistory? _instance;

        public static FakeGameHistory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FakeGameHistory();

                return _instance;
            }
        }


        private FakeGameHistory()
        {
            History = new List<GameResult>();
        }

        public List<GameResult> History { get; private set; }


        public void AjouterPartie(string resultat)
        {
            History.Add(new GameResult(resultat, "Bot", DateTime.Now));
        }

        public int Victoires => History.Count(g => g.Resultat == "Victoire");
        public int Defaites => History.Count(g => g.Resultat == "Défaite");
        public int Nuls => History.Count(g => g.Resultat == "Nul");
    }
}