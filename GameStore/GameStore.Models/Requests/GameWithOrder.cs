using GameStore.Models.DTO;
using GameStore.Models.Responses;
using Newtonsoft.Json;

public class GameWithOrder
{
    public required Game Game { get; set; }
    [JsonProperty("orderId")]
    public GameOrderResponse? OrderId { get; set; }
}

