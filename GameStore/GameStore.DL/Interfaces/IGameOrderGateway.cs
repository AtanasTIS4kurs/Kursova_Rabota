using GameStore.Models.DTO;
using GameStore.Models.Responses;

namespace GameStore.DL.Interfaces
{
    public interface IGameOrderGateway
    {
        Task<GameOrderResponse?> GetByName(string gameName);
        //Task<GameOrder?> GetByName(Game game);
    }
}
