using GameStore.Models.DTO;
using GameStore.Models.Requests;
using GameStore.Models.Responses;
using Mapster;

namespace GameStore.MapConfig
{
    public class MapsterConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<Company, GamesFromCompany>
                .NewConfig()
                .Map(dest => dest.Games, src => new List<Game>());
            TypeAdapterConfig<AddGameRequest, Game>
                .NewConfig()
                .Ignore(dest => dest.Id);
        }
    }
}
