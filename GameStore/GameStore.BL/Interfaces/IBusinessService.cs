using GameStore.Models.DTO;
using GameStore.Models.Requests;
using GameStore.Models.Responses;

namespace GameStore.BL.Interfaces
{
    public interface IBusinessService
    {
        Task<GamesFromCompany> GetGamesByCompanyName(string companyName);
        Task<Game> AddGame(AddGameRequest request);
    }
}
