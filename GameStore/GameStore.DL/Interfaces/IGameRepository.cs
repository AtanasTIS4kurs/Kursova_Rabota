using GameStore.Models.DTO;

namespace GameStore.DL.Interface
{
    public interface IGameRepository
    {
        List<Game> GetAll();
        Game GetById(string id);
        void Create(Game game);
        void Update(Game game);
        void Delete(string id);
        List<Game> GetByCompanyName(string companyName);
    }
}
