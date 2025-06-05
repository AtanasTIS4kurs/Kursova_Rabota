using GameStore.DL.Cache;
using GameStore.Models.DTO;

namespace GameStore.DL.Interface
{
    public interface IGameRepository : ICacheRepository<string, Game>
    {
        Task<List<Game>> GetAll();
        Task<Game?> GetById(string id);
        Task Create(Game game);
        Task Update(Game game);
        Task Delete(string id);
        Task<List<Game>> GetByCompanyName(string companyName);
    }
}
