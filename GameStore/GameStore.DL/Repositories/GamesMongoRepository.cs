using Microsoft.Extensions.Options;
using GameStore.DL.Interfaces;
using GameStore.Models.Configuration;
using GameStore.Models.DTO;
using MongoDB.Driver;

namespace GameStore.DL.Repositories
{
    internal class GamesMongoRepository : IGameRepository
    {
        private readonly IMongoCollection<Game> _gamesCollection;     

        public GamesMongoRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _gamesCollection = database.GetCollection<Game>($"{nameof(Game)}");
        }

        public async Task Create(Game game)
        {
            await _gamesCollection.InsertOneAsync(game);
        }

        public async Task Delete(string id)
        {
            await _gamesCollection.DeleteOneAsync(g => g.Id == id);
        }

        public async Task<List<Game>> GetAll()
        {
            var games = await _gamesCollection.FindAsync(_ => true);
            return await games.ToListAsync();
        }
        public async Task<Game?> GetById(string id)
        {
            var game = await _gamesCollection.FindAsync(g => g.Id == id);
            return await game.FirstOrDefaultAsync();
        }
        public async Task Update(Game game)
        {
            await _gamesCollection.ReplaceOneAsync(g => g.Id == game.Id, game);
        }
        public async Task<List<Game>> GetByCompanyName(string companyName)
        {
            var result = await _gamesCollection.FindAsync(g => g.Company == companyName);
            return await result.ToListAsync();
        }

        protected async Task<IEnumerable<Game?>> GetGamesAfterDateTime(DateTime date)
        {
            var result = await _gamesCollection.FindAsync(m => m.DateInserted >= date);
            return await result.ToListAsync();
        }
        public async Task<IEnumerable<Game?>> FullLoad()
        {
            return await GetAll();
        }

        public async Task<IEnumerable<Game?>> DifLoad(DateTime lastExecuted)
        {
            return await GetGamesAfterDateTime(lastExecuted);
        }
    }
}
