using GameStore.BL.Interfaces;
using GameStore.Models.DTO;
using GameStore.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<BusinessController> _logger;
        public GamesController(IGameService gameService, ILogger<BusinessController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet("GetALL")]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetAll();
            if (games.Count == 0)
            {
                _logger.LogError("No games found in database");
                return NotFound(new { message = "No games found." });
            }
            _logger.LogInformation("Getting all games from database");
            return Ok(games);
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<Game>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id) || !System.Text.RegularExpressions.Regex.IsMatch(id, @"^[a-fA-F0-9]{24}$"))
            {
                _logger.LogError("The provided id must be a 24-digit hexadecimal string.");
                return BadRequest(new { message = "The provided id must be a 24-digit hexadecimal string." });
            }
            var game = await _gameService.GetById(id);
            if (game == null)
            {
                _logger.LogError("Game with id:{id} not found", id);
                return NotFound(new { message = "Game not found." });
            }
            _logger.LogInformation("Getting game from database with id:{id}", id);
            return Ok(game);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Game game)
        {
            if (string.IsNullOrEmpty(game.Id) || !System.Text.RegularExpressions.Regex.IsMatch(game.Id, @"^[a-fA-F0-9]{24}$"))
            {
                _logger.LogError("The provided id must be a 24-digit hexadecimal string");
                return BadRequest(new { message = "The provided id must be a 24-digit hexadecimal string" });
            }
            var existingGame = await _gameService.GetById(game.Id);
            if (existingGame != null)
            {
                _logger.LogError("Game creation failed. Game with id:{game.id} is already in use", game.Id);
                return Conflict(new { message = "A game with this id already exists. Please use a unique id" });
            }
            if (game.Price <= 0)
            {
                _logger.LogError("Game creation failed. Game can't have a negative or no price");
                return Conflict(new { message = "Game can't have a negative or no price" });
            }
            await _gameService.Create(game);
            _logger.LogInformation("Creating game with id:{game.id} in database", game.Id);
            return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
        }

        [HttpPut("Update")]
        public IActionResult Update(string id, Game game)
        {
            if (id != game.Id)
            {
                _logger.LogError("Game with id:{@id} not found", id);
                return BadRequest(); 
            }
            _gameService.Update(game);
            return NoContent();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || !System.Text.RegularExpressions.Regex.IsMatch(id, @"^[a-fA-F0-9]{24}$"))
            {
                _logger.LogError("The provided id must be a 24-digit hexadecimal string.");
                return BadRequest(new { message = "The provided id must be a 24-digit hexadecimal string." });
            }
            var game = _gameService.GetById(id);
            if (game == null)
            {
                _logger.LogWarning("Game with id:{Id} not found.", id);
                return NotFound(new { message = "Game not found." });
            }
            _gameService.Delete(id);
            _logger.LogInformation("Deleting game with id:{@id}", id);
            return NoContent();
        }
    }
}
