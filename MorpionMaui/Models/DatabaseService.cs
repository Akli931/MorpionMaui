using SQLite;

namespace MorpionMaui.Models
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _db;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "morpion.db3");
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<GameResult>().Wait();
        }

        public Task AjouterPartieAsync(string resultat, string adversaire = "Bot")
        {
            var entry = new GameResult(resultat, adversaire, DateTime.Now);
            return _db.InsertAsync(entry);
        }

        public Task<List<GameResult>> GetHistoriqueAsync()
            => _db.Table<GameResult>().OrderByDescending(g => g.Date).ToListAsync();

        public async Task<int> GetVictoiresAsync()
            => await _db.Table<GameResult>().CountAsync(g => g.Resultat == "Victoire");

        public async Task<int> GetDefaitesAsync()
            => await _db.Table<GameResult>().CountAsync(g => g.Resultat == "Défaite");

        public async Task<int> GetNulsAsync()
            => await _db.Table<GameResult>().CountAsync(g => g.Resultat == "Nul");
    }
}