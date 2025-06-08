using GameStore.BL.Interfaces;
using GameStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;
        private readonly ILogger<BusinessController> _logger;

        public BusinessController(IBusinessService businessService, ILogger<BusinessController> logger)
        {
            _businessService = businessService;
            _logger = logger;
        }

        [HttpGet("GetGamesByCompany")]
        public async Task<IActionResult> GetGamesByCompany(string companyName)
        {
            try
            {
                var result = await _businessService.GetGamesByCompanyName(companyName);
                _logger.LogInformation("Getting games from company {companyName}", companyName);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError("Company {companyName} not Found", companyName);
                return NotFound(ex.Message);
            }
        }
        [HttpPost("AddGame")]
        public async Task<IActionResult> AddGame([FromBody] AddGameRequest request)
        {

            _logger.LogInformation("Adding a new game");
            var newGame = await _businessService.AddGame(request);
            return CreatedAtAction(nameof(AddGame), new { id = newGame.Id }, newGame);
        }
        [HttpGet("GetGameOrder")]
        public async Task<IActionResult> GetGameOrder(string id)
        {
            try
            {
                var result = await _businessService.GetGameOrder(id);
                _logger.LogInformation("Getting order info for game with id:{id}", id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError("Game with id:{id} not Found", id);
                return NotFound(ex.Message);
            }
        }
    }


}
