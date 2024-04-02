using AutoMapper;
using FluentAssertions;
using Game.Application.Services;
using Game.Domain.Interfaces.Repositories;
using Game.Domain.Model;
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
            _gameRepositoryMock.Setup(s => s.GetById(It.IsAny<int>())).Returns(It.IsAny<BoardState>());
            
            _gameRepositoryMock.Setup(s => s.GetFinalState(It.IsAny<int>())).Returns(It.IsAny<BoardState>());

            _gameRepositoryMock.Setup(s => s.GetNextState()).Returns(It.IsAny<BoardState>());

            _gameRepositoryMock.Setup(s => s.RemoveById(It.IsAny<int>())).Returns(It.IsAny<BoardState>());

            _gameRepositoryMock.Setup(s => s.Simulate(It.IsAny<int>())).Returns(It.IsAny<Grid>());

            _gameRepositoryMock.Setup(s => s.Upload(It.IsAny<Grid>())).Returns(1);
        }

        private void SetupSuccess()
        {
            var validBoardState = new BoardState()
            {
                Id = 1,
                Grid = new Grid()
                {
                    Width = 10,
                    Height = 10,
                    Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 } }
                }
            };

            _gameRepositoryMock.Setup(s => s.GetById(It.IsAny<int>())).Returns(validBoardState);

            _gameRepositoryMock.Setup(s => s.GetFinalState(It.IsAny<int>())).Returns(validBoardState);

            _gameRepositoryMock.Setup(s => s.GetNextState()).Returns(validBoardState);

            _gameRepositoryMock.Setup(s => s.RemoveById(It.IsAny<int>())).Returns(validBoardState);

            _gameRepositoryMock.Setup(s => s.Simulate(It.IsAny<int>())).Returns(validBoardState.Grid);

            _gameRepositoryMock.Setup(s => s.Upload(validBoardState.Grid)).Returns(1);
        }


        [Theory(DisplayName = "GetBoardStateById_with_invalid_id")]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetBoardStateById_with_invalid_id(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.GetById(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain($"{Messages.INVALID_BOARD_ID} {value}");
        }

        [Theory(DisplayName = "GetBoardStateById_returning_not_found")]
        [InlineData(100)]
        public void GetBoardStateById_returning_not_found(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.GetById(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.CurrentHttpStatusCode?.Should().Be(HttpStatusCode.NotFound);
            result.Errors.Should().Contain($"{Messages.BOARD_STATE_NOT_FOUND_WITH_ID} {value}");
        }

        [Theory(DisplayName = "GetBoardStateById_returning_with_success")]
        [InlineData(1)]
        public void GetBoardStateById_returning_with_success(int value)
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.GetById(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Theory(DisplayName = "GetFinalBoardState_with_invalid_iterations")]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetFinalBoardState_with_invalid_iterations(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.GetFinalState(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_ITERATIONS);
        }

        [Theory(DisplayName = "GetFinalBoardState_returning_failure")]
        [InlineData(1)]
        public void GetFinalBoardState_returning_failure(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.GetFinalState(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_FINAL_STATE);
        }

        [Theory(DisplayName = "GetFinalBoardState_returning_success")]
        [InlineData(1)]
        public void GetFinalBoardState_returning_success(int value)
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.GetFinalState(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Fact(DisplayName = "GetFinalState_returning_failure")]
        public void GetNextState_returning_failure()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.GetNextState();

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_NEXT_STATE);
        }

        [Fact(DisplayName = "GetFinalState_returning_success")]
        public void GetNextState_returning_success()
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.GetNextState();

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Theory(DisplayName = "RemoveBoardStateById_with_invalid_id")]
        [InlineData(-1)]
        [InlineData(0)]
        public void RemoveBoardStateById_with_invalid_id(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.RemoveById(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain($"{Messages.INVALID_BOARD_ID} {value}");
        }

        [Theory(DisplayName = "RemoveBoardStateById_returning_not_found")]
        [InlineData(1)]
        public void RemoveBoardStateById_returning_not_found(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.RemoveById(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.CurrentHttpStatusCode?.Should().Be(HttpStatusCode.NotFound);
            result.Errors.Should().Contain($"{Messages.BOARD_STATE_NOT_FOUND_WITH_ID} {value}");
        }

        [Theory(DisplayName = "RemoveBoardStateById_returning_success")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void RemoveBoardStateById_returning_success(int value)
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.RemoveById(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Theory(DisplayName = "SimulatePopulations_with_invalid_iterations")]
        [InlineData(-1)]
        [InlineData(0)]
        public void SimulatePopulations_with_invalid_iterations(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.Simulate(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_ITERATIONS);
        }

        [Theory(DisplayName = "SimulatePopulations_returning_not_found")]
        [InlineData(1)]
        public void SimulatePopulations_returning_not_found(int value)
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.Simulate(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.CurrentHttpStatusCode?.Should().Be(HttpStatusCode.NotFound);
            result.Errors.Should().Contain(Messages.CURRRENT_BOARD_STATE_NOT_FOUND);
        }

        [Theory(DisplayName = "SimulatePopulations_returning_success")]
        [InlineData(1)]
        public void SimulatePopulations_returning_success(int value)
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.Simulate(value);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }

        [Fact(DisplayName = "UploadBoard_with_invalid_grid_null_value")]
        public void UploadBoard_with_invalid_grid_null_value()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.Upload(It.IsAny<Grid>());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_BOARD_GRID);
        }

        [Fact(DisplayName = "UploadBoard_with_invalid_grid")]
        public void UploadBoard_with_invalid_grid()
        {
            // Arrange
            SetupFailure();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.Upload(new Grid()
            {
                Width = 10,
                Height = 7,
                Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 } }
            });

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain(Messages.INVALID_BOARD_GRID);
        }

        [Fact(DisplayName = "UploadBoard_returning_success")]
        public void UploadBoard_returning_success()
        {
            // Arrange
            SetupSuccess();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapper);

            // Act
            var result = _gameService.Upload(new Grid()
            {
                Width = 10,
                Height = 10,
                Cells = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 0 } }
            });

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNull();
        }
    }
}
