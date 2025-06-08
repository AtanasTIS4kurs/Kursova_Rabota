using GameStore.DL.Interfaces;
using GameStore.Models.DTO;
using GameStore.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
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
        public async Task<GameOrderResponse?> GetOrder(Game game)
        {
            var request = new RestRequest($"/api/OrderData", Method.Post);

            var json = JsonConvert.SerializeObject(game);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            request.AddBody(data);

            var response = await _client.ExecuteAsync<GameOrderResponse>(request);

            return response.Data;
        }
    }
}
