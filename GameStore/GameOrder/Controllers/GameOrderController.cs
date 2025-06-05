using GameStore.DL.Interfaces;
using GameStore.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameOrder.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class ExternalOrderController : ControllerBase
        {
            private readonly IGameOrderGateway _gameOrderGateway;

            public ExternalOrderController(IGameOrderGateway orderGateway)
            {
                _gameOrderGateway = orderGateway;
            }
        [HttpPost("Order")]
        public async Task<ActionResult<GameOrderResponse>> GetGameOrderByName([FromBody] string gameName)
        {
            if (string.IsNullOrWhiteSpace(gameName))
                return BadRequest("Game name is required.");

            var result = await _gameOrderGateway.GetByName(gameName);

            if (result == null)
                return NotFound($"Game '{gameName}' is not available.");

            return Ok(result);
        }
    }

}


