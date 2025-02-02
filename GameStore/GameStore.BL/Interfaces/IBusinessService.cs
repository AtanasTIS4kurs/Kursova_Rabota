using GameStore.Models.DTO;
using GameStore.Models.Requests;
using GameStore.Models.Responses;

namespace GameStore.BL.Interfaces
{
    public interface IBusinessService
    {
        GamesFromCompany GetGamesByCompanyName(string companyName);
        Game AddGame(AddGameRequest request);
    }
}
