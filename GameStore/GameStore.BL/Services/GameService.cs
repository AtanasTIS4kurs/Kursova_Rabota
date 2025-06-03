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

        public async Task Create(Game game)
        {
            await _gameRepository.Create(game);
        }

        public async Task Delete(string id)
        {
            await _gameRepository.Delete(id);
        }

        public async Task<List<Game>> GetAll()
        {
            return await _gameRepository.GetAll();        }

        public async Task<Game?> GetById(string id) => await _gameRepository.GetById(id);

        public async Task Update(Game game)
        {
           await _gameRepository.Update(game);
        }
    }

}
