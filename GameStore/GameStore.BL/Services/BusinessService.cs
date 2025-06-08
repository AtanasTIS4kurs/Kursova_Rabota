using FluentValidation;
using GameStore.BL.Interfaces;
using GameStore.DL.Interfaces;
using GameStore.Models.DTO;
using GameStore.Models.Requests;
using GameStore.Models.Responses;
using Mapster;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameStore.Test")]

namespace GameStore.BL.Services
{
    internal class BusinessService : IBusinessService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IGameOrderGateway _gameOrderGateway;
        private readonly IValidator<AddGameRequest> _addGameValidator;

        public BusinessService(IGameRepository gameRepository, ICompanyRepository companyRepository, IGameOrderGateway gameOrderGateway, IValidator<AddGameRequest> addGameValidator)
        {
            _gameRepository = gameRepository;
            _companyRepository = companyRepository;
            _gameOrderGateway = gameOrderGateway;
            _addGameValidator = addGameValidator;
        }

        public async Task<GamesFromCompany> GetGamesByCompanyName(string companyName)
        {
            var company = await _companyRepository.GetByName(companyName);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company '{companyName}' not found.");
            }

            var games = await _gameRepository.GetByCompanyName(company.Name);

            var companyWithGames = company.Adapt<GamesFromCompany>();
            companyWithGames.Games = games; 

            return companyWithGames;
        }
        public async Task<Game> AddGame(AddGameRequest request)
        {
            var validationResult = _addGameValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }
            var newGame = request.Adapt<Game>();

            await _gameRepository.Create(newGame);

            return newGame;
        }
        public async Task<GameWithOrder?> GetGameOrder(string id)
        {
            var game = await _gameRepository.GetById(id);
            if (game == null) return null;

            var orderResponse = await _gameOrderGateway.GetOrder(game);

            return new GameWithOrder { Game = game, OrderId = orderResponse };
        }

    }
}