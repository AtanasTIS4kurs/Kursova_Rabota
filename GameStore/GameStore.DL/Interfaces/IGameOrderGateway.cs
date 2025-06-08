using GameStore.Models.DTO;
using GameStore.Models.Responses;

namespace GameStore.DL.Interfaces
{
    public interface IGameOrderGateway
    {
        Task<GameOrderResponse?> GetOrder(Game game);
    }
}
