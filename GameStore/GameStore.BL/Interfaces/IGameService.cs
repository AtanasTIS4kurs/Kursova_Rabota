using GameStore.Models.DTO;

namespace GameStore.BL.Interfaces
{
    public interface IGameService
    {
        List<Game> GetAll();
        Game GetById(string id);
        void Create(Game game);
        void Update(Game game);
        void Delete(string id);
    }
}
