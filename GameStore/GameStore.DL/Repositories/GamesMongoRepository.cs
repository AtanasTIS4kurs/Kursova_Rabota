using Microsoft.Extensions.Options;
using GameStore.DL.Interface;
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

        public void Create(Game game)
        {
            _gamesCollection.InsertOne(game);
        }

        public void Delete(string id)
        {
            _gamesCollection.DeleteOne(g => g.Id == id);
        }

        public List<Game> GetAll()
        {
            return _gamesCollection.Find(_ => true).ToList();
        }
        public Game GetById(string id)
        {
            return _gamesCollection.Find(g => g.Id == id).FirstOrDefault();
        }
        public void Update(Game game)
        {
            _gamesCollection.ReplaceOne(g => g.Id == game.Id, game);
        }
        public List<Game> GetByCompanyName(string companyName)
        {
            return _gamesCollection.Find(g => g.Company == companyName).ToList();
        }
    }
}
