using AutoMapper;
using FluentAssertions;
using Game.Application.Services;
using Game.Domain.Interfaces.Repositories;
using Game.Domain.Entities;
using Game.Domain.Resources;
using Moq;
using System.Net;

namespace Game.Tests.Services
{
    public class GameServiceTest : BaseServiceTest
    {
        private GameService _gameService;

        private readonly IMapper _mapper;
        private readonly Mock<IGameRepository> _gameRepositoryMock = new Mock<IGameRepository>();

        public GameServiceTest()
        {
            _mapper = CreateIMapper();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);
        }

        private void SetupFailure()
        {
            _gameRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<BoardState>());
            
            _gameRepositoryMock.Setup(s => s.GetFinalStateAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<BoardState>());

            _gameRepositoryMock.Setup(s => s.GetNextStateAsync(It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<BoardState>());

            _gameRepositoryMock.Setup(s => s.RemoveByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<BoardState>());

            _gameRepositoryMock.Setup(s => s.SimulateAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<Grid>());

            _gameRepositoryMock.Setup(s => s.UploadAsync(It.IsAny<Grid>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid().ToString());
        }

        private void SetupSuccess()
        {
            var validBoardState = new BoardState()
            {
                Grid = new Grid()
                {
                    Width = 10,
                    Height = 10,
                    Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 } }
                }
            };

            _gameRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validBoardState);

            _gameRepositoryMock.Setup(s => s.GetFinalStateAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(validBoardState);

            _gameRepositoryMock.Setup(s => s.GetNextStateAsync(It.IsAny<CancellationToken>())).ReturnsAsync(validBoardState);

            _gameRepositoryMock.Setup(s => s.RemoveByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validBoardState);

            _gameRepositoryMock.Setup(s => s.SimulateAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(validBoardState.Grid);

            _gameRepositoryMock.Setup(s => s.UploadAsync(validBoardState.Grid, It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid().ToString());
        }


        [Fact(DisplayName = "GetBoardStateById_with_invalid_id")]
        public async Task GetBoardStateById_with_invalid_id()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.GetByIdAsync(Guid.Empty, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain($"{Messages.INVALID_BOARD_ID} {Guid.Empty}");
        }

        [Fact(DisplayName = "GetBoardStateById_returning_not_found")]
        public async Task GetBoardStateById_returning_not_found()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);
            var id = Guid.NewGuid();

            // Act
            var result = await _gameService.GetByIdAsync(id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.CurrentHttpStatusCode?.Should().Be(HttpStatusCode.NotFound);
            result.Errors.Should().Contain($"{Messages.BOARD_STATE_NOT_FOUND_WITH_ID} {id}");
        }

        [Fact(DisplayName = "GetBoardStateById_returning_with_success")]
        public async Task GetBoardStateById_returning_with_success()
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Theory(DisplayName = "GetFinalBoardState_with_invalid_iterations")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetFinalBoardState_with_invalid_iterations(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.GetFinalStateAsync(value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_ITERATIONS);
        }

        [Theory(DisplayName = "GetFinalBoardState_with_invalid_max_iterations")]
        [InlineData(101)]
        [InlineData(999)]
        public async Task GetFinalBoardState_with_invalid_max_iterations(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.GetFinalStateAsync(value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain($"{Messages.MAX_NUMBER_OF_ITERATIONS_WAS_REACHED} 100");
        }

        [Theory(DisplayName = "GetFinalBoardState_returning_failure")]
        [InlineData(1)]
        public async Task GetFinalBoardState_returning_failure(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.GetFinalStateAsync(value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_FINAL_STATE);
        }

        [Theory(DisplayName = "GetFinalBoardState_returning_success")]
        [InlineData(1)]
        public async Task GetFinalBoardState_returning_success(int value)
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.GetFinalStateAsync(value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = "GetFinalState_returning_failure")]
        public async Task GetNextState_returning_failure()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.GetNextStateAsync(CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_NEXT_STATE);
        }

        [Fact(DisplayName = "GetFinalState_returning_success")]
        public async Task GetNextState_returning_success()
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.GetNextStateAsync(CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = "RemoveBoardStateById_with_invalid_id")]
        public async Task RemoveBoardStateById_with_invalid_id()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.RemoveByIdAsync(Guid.Empty, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain($"{Messages.INVALID_BOARD_ID} {Guid.Empty}");
        }

        [Fact(DisplayName = "RemoveBoardStateById_returning_not_found")]
        public async Task RemoveBoardStateById_returning_not_found()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);
            var id = Guid.NewGuid();

            // Act
            var result = await _gameService.RemoveByIdAsync(id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.CurrentHttpStatusCode?.Should().Be(HttpStatusCode.NotFound);
            result.Errors.Should().Contain($"{Messages.BOARD_STATE_NOT_FOUND_WITH_ID} {id}");
        }

        [Fact(DisplayName = "RemoveBoardStateById_returning_success")]
        public async Task RemoveBoardStateById_returning_success()
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.RemoveByIdAsync(Guid.NewGuid(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Theory(DisplayName = "SimulatePopulations_with_invalid_iterations")]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task SimulatePopulations_with_invalid_iterations(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.SimulateAsync(value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_ITERATIONS);
        }

        [Theory(DisplayName = "SimulatePopulations_with_invalid_max_iterations")]
        [InlineData(101)]
        [InlineData(999)]
        public async Task SimulatePopulations_with_invalid_max_iterations(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.SimulateAsync(value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain($"{Messages.MAX_NUMBER_OF_ITERATIONS_WAS_REACHED} 100");
        }

        [Theory(DisplayName = "SimulatePopulations_returning_not_found")]
        [InlineData(1)]
        public async Task SimulatePopulations_returning_not_found(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.SimulateAsync(value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.CurrentHttpStatusCode?.Should().Be(HttpStatusCode.NotFound);
            result.Errors.Should().Contain(Messages.CURRRENT_BOARD_STATE_NOT_FOUND);
        }

        [Theory(DisplayName = "SimulatePopulations_returning_success")]
        [InlineData(1)]
        public async Task SimulatePopulations_returning_success(int value)
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.SimulateAsync(value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = "UploadBoard_with_invalid_grid_null_value")]
        public async Task UploadBoard_with_invalid_grid_null_value()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.UploadAsync(It.IsAny<Grid>(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_BOARD_GRID);
        }

        [Fact(DisplayName = "UploadBoard_with_invalid_grid")]
        public async Task UploadBoard_with_invalid_grid()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.UploadAsync(new Grid()
            {
                Width = 10,
                Height = 7,
                Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 } }
            }, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_BOARD_GRID);
        }

        [Fact(DisplayName = "UploadBoard_returning_success")]
        public async Task UploadBoard_returning_success()
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = await _gameService.UploadAsync(new Grid()
            {
                Width = 10,
                Height = 10,
                Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 } }
            }, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }
    }
}
