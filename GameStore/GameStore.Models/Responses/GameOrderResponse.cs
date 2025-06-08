using Newtonsoft.Json;

namespace GameStore.Models.Responses
{
    public class GameOrderResponse
    {
        [JsonProperty("order")]
        public string Order { get; set; } = null!;
    }
}
