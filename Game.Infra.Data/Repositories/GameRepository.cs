using Game.Domain.Core;
using Game.Domain.Interfaces.Repositories;
using Game.Domain.Entities;
using Game.Infra.Data.Repositories;
using Game.Infra.Data.Context;
using Game.Infra.Data.Helpers;

namespace Infra.Data.Repositories
{
    internal class GameRepository : BaseRepository<Grid>, IGameRepository
    {
        private BaseGameOfLife _game;

        // TODO: For test purpose only. It needs to be removed for production
        private static int _items;

        public GameRepository(GameContext gameContext) : base(gameContext) 
        {
            _game = new GameOfLife();
        }

        public async Task<string> UploadAsync(Grid grid, CancellationToken cancellationToken)
        {
            _game = new GameOfLife(grid.Width, grid.Height, grid.Cells);

            var newGrid = await AddAsync(new Grid()
            {
                Width = _game.Cols,
                Height = _game.Rows,
                TwoDimensionalStringArray = ArrayHelper.SerializeFrom2DArrayToString(_game.CurrentBoardGeneration)
            }, cancellationToken);

            await CommitAsync();

            _items = await CountAsync(cancellationToken);

            return newGrid.Id;
        }

        public async Task<BoardState?> GetNextStateAsync(CancellationToken cancellationToken)
        {
            _game.CreateNextGeneration();

            var newGrid = new Grid()
            {
                Width = _game.Cols,
                Height = _game.Rows,
                Cells = _game.CurrentBoardGeneration,
                TwoDimensionalStringArray = ArrayHelper.SerializeFrom2DArrayToString(_game.CurrentBoardGeneration)
            };

            var nextBoardState = CreateBoardState(newGrid);

            await AddAsync(newGrid, cancellationToken);

            await CommitAsync();

            _items = await CountAsync(cancellationToken);

            return nextBoardState;
        }

        public async Task<Grid?> SimulateAsync(int iterations, CancellationToken cancellationToken)
        {
            if (iterations < 0)
                return null;

            Grid newGrid;

            var gridTaskList = new List<Task>();
            var gridList = new List<Grid>();

            for (int i = 0; i < iterations; i++)
            {
                _game.CreateNextGeneration();

                gridTaskList.Add(Task.Factory.StartNew(() =>
                {
                    newGrid = new Grid()
                    {
                        Width = _game.Cols,
                        Height = _game.Rows,
                        Cells = _game.CurrentBoardGeneration,
                        TwoDimensionalStringArray = ArrayHelper.SerializeFrom2DArrayToString(_game.CurrentBoardGeneration)
                    };

                    gridList.Add(newGrid);

                }));
            }

            Task.WaitAll(gridTaskList.ToArray());

            await AddAsync(gridList, cancellationToken);
            await CommitAsync();

            _items = await CountAsync(cancellationToken);

            return gridList.Last();
        }

        public async Task<BoardState?> GetFinalStateAsync(int iterations, CancellationToken cancellationToken)
        {
            if (iterations < 0)
                return null;

            bool isCompleted = false;

            Grid newGrid;

            var gridTaskList = new List<Task>();
            var gridList = new List<Grid>();

            for (int i = 0; i < iterations; i++)
            {
                _game.CreateNextGeneration();

                gridTaskList.Add(Task.Factory.StartNew(() =>
                {
                    newGrid = new Grid()
                    {
                        Width = _game.Cols,
                        Height = _game.Rows,
                        Cells = _game.CurrentBoardGeneration,
                        TwoDimensionalStringArray = ArrayHelper.SerializeFrom2DArrayToString(_game.CurrentBoardGeneration)
                    };

                    gridList.Add(newGrid);

                }));

                if (_game.IsStable())
                {
                    isCompleted = true;
                    break;
                }
            }

            Task.WaitAll(gridTaskList.ToArray());

            await AddAsync(gridList, cancellationToken);
            await CommitAsync();

            _items = await CountAsync(cancellationToken);

            return isCompleted ? CreateBoardState(gridList.Last()) : null;
        }

        public async Task<BoardState?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var grid = await FindAsync(id.ToString(), cancellationToken);
            if (grid != null)
                grid.Cells = ArrayHelper.DeserializeFromStringTo2DArray(grid.TwoDimensionalStringArray);

            return CreateBoardState(grid);
        }

        public async Task<BoardState?> RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var grid = await RemoveByIdAsync(id.ToString(), cancellationToken);
            
            if (grid == null)
                return null;

            await CommitAsync();

            if (grid != null)
                grid.Cells = ArrayHelper.DeserializeFromStringTo2DArray(grid.TwoDimensionalStringArray);

            _items = await CountAsync(cancellationToken);

            return CreateBoardState(grid); 
        }

        private BoardState? CreateBoardState(Grid? grid)
        {
            if (grid == null)
                return null;

            return new BoardState()
            {
                Grid = grid
            };
        }
    }
}
