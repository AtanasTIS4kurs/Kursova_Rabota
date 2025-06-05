using GameStore.DL.Interfaces;
using GameStore.Models.DTO;
using GameStore.Models.Responses;
using RestSharp;
using System.Text;

namespace GameStore.DL.Gateway
{
    
    public class GameOrderGateway : IGameOrderGateway
    {
        private readonly RestClient _client;
        public GameOrderGateway()
        {
            var options = new RestClientOptions("https://localhost:7077");

            _client = new RestClient(options);
        }
        public async Task<GameOrderResponse?> GetByName(string gameName)
        {
            if (string.IsNullOrWhiteSpace(gameName))
                return null;

            var request = new RestRequest("api/order/by-name", Method.Post); 
            request.AddJsonBody(gameName);

            var response = await _client.ExecuteAsync<GameOrderResponse>(request);

            return response.IsSuccessful ? response.Data : null;
        }
    }
}
