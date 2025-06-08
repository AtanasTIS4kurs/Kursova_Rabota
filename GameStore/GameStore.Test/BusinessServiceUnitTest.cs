using Moq;
using GameStore.Models.DTO;
using GameStore.Models.Responses;
using GameStore.BL.Services;
using FluentValidation.Results;
using GameStore.Models.Requests;
using FluentValidation;
using GameStore.DL.Interfaces;
namespace GameStore.Test
{
    public class BusinessServiceUnitTest
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly Mock<IGameOrderGateway> _gameOrderGatewayMock; 
        private readonly Mock<IValidator<AddGameRequest>> _mockValidator;
        private readonly BusinessService _businessService;

        public static List<Game> _games = new List<Game>()
        {
            new Game()
            {
                Id = "06D596ABE18AB5959D4ACE80",
                Name = "Game1",
                Company = "Company1",
                Price = 10.12m,
            },
            new Game()
            {
                Id = "248387864212F13DE842E37A",
                Name = "Game2",
                Company= "Company1",
                Price = 20.3m,
            },
            new Game()
            {
                Id = "AFD10E8B1CB2432E87E15579",
                Name = "Game3",
                Company = "Company2",
                Price = 29.99m,
            }
        };
        
        public static List<GamesFromCompany> _companies = new List<GamesFromCompany>()
        {
            new GamesFromCompany()
            {
                Id = "06D596ABE18AB5959D4ACE80",
                Name = "Company1",
                Employees = 10,
                Games = []
            },
            new GamesFromCompany()
            {
                Id = "248387864212F13DE842E37A",
                Name = "Company2",
                Employees = 20,
                Games = []
            },
            new GamesFromCompany()
            {
                Id = "AFD10E8B1CB2432E87E15579",
                Name = "Company3",
                Employees = 30,
                Games = []
            }
        };
        public BusinessServiceUnitTest()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _gameOrderGatewayMock = new Mock<IGameOrderGateway>();
            _mockValidator = new Mock<IValidator<AddGameRequest>>();
            _businessService = new BusinessService(_gameRepositoryMock.Object, _companyRepositoryMock.Object, _gameOrderGatewayMock.Object, _mockValidator.Object);
        }
        [Fact]
        public async Task GetGamesByCompanyName_OK()
        {
            // Arrange
            var companyName = "TestCompany";
            var company = new Company { Name = companyName };
            var games = new List<Game> { new Game { Name = "Game1" }, new Game { Name = "Game2" } };

            _companyRepositoryMock.Setup(x => x.GetByName(companyName)).ReturnsAsync(company);
            _gameRepositoryMock.Setup(x => x.GetByCompanyName(companyName)).ReturnsAsync(games);

            //// Act
            var result = await _businessService.GetGamesByCompanyName(companyName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(companyName, result.Name);
            Assert.Equal(2, result.Games.Count);
        }

        [Fact]
        public async Task GetGamesByCompanyName_NotOk()
        {
            // Arrange
            var companyName = "TestCompany";
            _companyRepositoryMock.Setup(x => x.GetByName(companyName)).ReturnsAsync((Company?)null);

            // Act 

            // Assert

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _businessService.GetGamesByCompanyName(companyName));
            Assert.Equal($"Company '{companyName}' not found.", exception.Message);
        }
        [Fact]
        public async Task AddGame_OK()
        {
            // Arrange
            var request = new AddGameRequest { Name = "NewGame" };
            var game = new Game { Name = "NewGame" };
            _mockValidator.Setup(v => v.Validate(request)).Returns(new ValidationResult());
            _gameRepositoryMock.Setup(x => x.Create(It.IsAny<Game>()));

            // Act

            var result = await _businessService.AddGame(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NewGame", result.Name);
            _gameRepositoryMock.Verify(x => x.Create(It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task AddGame_NotOK()
        {
            // Arrange
            var request = new AddGameRequest { Name = "" }; // Invalid request
            var validationErrors = new List<ValidationFailure> { new ValidationFailure("Title", "Title is required.") };
            _mockValidator.Setup(x => x.Validate(request)).Returns(new ValidationResult(validationErrors));

            // Act
           
            //Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _businessService.AddGame(request));
            Assert.Contains("Title is required.", exception.Message);
        }
    }
}