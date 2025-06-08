using GameStore.Models.DTO;
using GameStore.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameOrder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameOrderController : ControllerBase
    {

        [HttpPost]
        public ActionResult<GameOrderResponse> OrderGame([FromBody] Game game)
        {
            var orderId = $"ORD-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            Console.WriteLine($"Received order for: {game.Name} from {game.Company}");

            var response = new GameOrderResponse
            {
                Order = orderId
            };

            return Ok(response);
        }
    }

}


