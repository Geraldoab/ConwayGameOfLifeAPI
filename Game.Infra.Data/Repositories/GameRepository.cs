using Game.Domain.Core;
using Game.Domain.Interfaces.Repositories;
using Game.Domain.Entities;
using Game.Infra.Data.Repositories;
using Game.Infra.Data.Context;

namespace Infra.Data.Repositories
{
    internal class GameRepository : BaseRepository<Grid>, IGameRepository
    {
        private static Dictionary<int, BoardState> _boards = new Dictionary<int, BoardState>();
        private static int _boardId = 1;
        private BaseGameOfLife _game;

        public GameRepository(GameContext gameContext) : base(gameContext) 
        {
            _game = new GameOfLife();
        }

        public async Task<int> UploadAsync(Grid grid, CancellationToken cancellationToken)
        {
            var newBoardState = CreateState(grid);
            _boards.Add(newBoardState.Id, newBoardState);

            _game = new GameOfLife(grid.Width, grid.Height, grid.Cells);

            return newBoardState.Id;
        }

        public async Task<BoardState> GetNextStateAsync(CancellationToken cancellationToken)
        {
            _game.CreateNextGeneration();

            var newGrid = new Grid()
            {
                Width = _game.Cols,
                Height = _game.Rows,
                Cells = _game.CurrentBoardGeneration
            };

            var nextBoardState = CreateState(newGrid);

            _boards.Add(nextBoardState.Id, nextBoardState);

            return nextBoardState;
        }

        public async Task<Grid?> SimulateAsync(int iterations, CancellationToken cancellationToken)
        {
            if (iterations < 0)
                return null;

            var newSimulatedState = new BoardState();

            for (int i = 0; i < iterations; i++)
            {
                _game.CreateNextGeneration();

                newSimulatedState = CreateState(new Grid()
                {
                    Width = _game.Cols,
                    Height = _game.Rows,
                    Cells = _game.CurrentBoardGeneration
                });

                _boards.Add(newSimulatedState.Id, newSimulatedState);
            }

            return new Grid()
            {
                Width = _game.Cols,
                Height = _game.Rows,
                Cells = _game.CurrentBoardGeneration
            };
        }

        public async Task<BoardState?> GetFinalStateAsync(int iterations, CancellationToken cancellationToken)
        {
            if (iterations < 0)
                return null;

            bool isCompleted = false;

            for (int i = 0; i < iterations; i++)
            {
                _game.CreateNextGeneration();
                if(_game.IsStable())
                {
                    isCompleted = true;
                    break;
                }
            }

            return isCompleted ? CreateState(new Grid()
            {
                Width = _game.Cols,
                Height = _game.Rows,
                Cells = _game.CurrentBoardGeneration
            }) : null;
        }

        public async Task<BoardState?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (!_boards.ContainsKey(id))
                return null;

            return _boards[id];
        }

        public async Task<BoardState?> RemoveByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (!_boards.ContainsKey(id))
                return null;

            var selectedBoard = _boards[id];
            _boards.Remove(id);

            return selectedBoard;
        }

        private BoardState CreateState(Grid grid)
        {
            return new BoardState()
            {
                Id = _boardId++,
                Grid = grid
            };
        }
    }
}
