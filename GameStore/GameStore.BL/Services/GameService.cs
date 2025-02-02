using GameStore.BL.Interfaces;
using GameStore.DL.Interface;
using GameStore.Models.DTO;

namespace GameStore.BL.Services
{
    internal class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public void Create(Game game)
        {
            _gameRepository.Create(game);
        }

        public void Delete(string id)
        {
            _gameRepository.Delete(id);
        }

        public List<Game> GetAll()
        {
            return _gameRepository.GetAll();        }

        public Game GetById(string id) => _gameRepository.GetById(id);

        public void Update(Game game)
        {
            _gameRepository.Update(game);
        }
    }

}
