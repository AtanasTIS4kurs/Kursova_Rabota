using GameStore.Models.DTO;

namespace GameStore.BL.Interfaces
{
    public interface IGameService
    {
        Task<List<Game>> GetAll();
        Task<Game?> GetById(string id);
        Task Create(Game game);
        Task Update(Game game);
        Task Delete(string id);
    }
}
