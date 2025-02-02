using FluentValidation;
using GameStore.BL.Interfaces;
using GameStore.DL.Interface;
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
        private readonly IValidator<AddGameRequest> _addGameValidator;

        public BusinessService(IGameRepository gameRepository, ICompanyRepository companyRepository, IValidator<AddGameRequest> addGameValidator)
        {
            _gameRepository = gameRepository;
            _companyRepository = companyRepository;
            _addGameValidator = addGameValidator;
        }

        public GamesFromCompany GetGamesByCompanyName(string companyName)
        {
            var company = _companyRepository.GetByName(companyName);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company '{companyName}' not found.");
            }

            var games = _gameRepository.GetByCompanyName(company.Name);

            var companyWithGames = company.Adapt<GamesFromCompany>();
            companyWithGames.Games = games; 

            return companyWithGames;
        }
        public Game AddGame(AddGameRequest request)
        {
            var validationResult = _addGameValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }
            var newGame = request.Adapt<Game>();

            _gameRepository.Create(newGame);

            return newGame;
        }
    }
}