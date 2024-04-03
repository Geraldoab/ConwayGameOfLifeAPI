namespace Game.Domain.Core
{
    /// <summary>
    /// Base class to control the game behavior. It's possible to improve memory consumption by changing <code>int[,]</code> to <code>byte[,]</code>
    /// </summary>
    public abstract class BaseGameOfLife
    {
        protected const int DEAD = 0;
        public const int ALIVE = 1;
        
        public int Cols { get; private set; }
        public int Rows { get; private set; }

        protected int[,] _nextBoardGeneration { get; set; } = null!;
        public int[,] CurrentBoardGeneration { get; set; } = null!;
        public int[,] PreviousBoardGeneration { get; set; } = null!;

        public BaseGameOfLife()
        {
            Cols = 10;
            Rows = 10;

            CreateInitialPopulation();
        }

        public BaseGameOfLife(int numberOfColumns, int numberOfRows)
        {
            Cols = numberOfColumns;
            Rows = numberOfRows;

            CreateInitialPopulation();
        }

        public BaseGameOfLife(int numberOfColumns, int numberOfRows, int[,] cells)
        {
            Cols = numberOfColumns;
            Rows = numberOfRows;

            CurrentBoardGeneration = cells;
            PreviousBoardGeneration = cells;
            _nextBoardGeneration = cells;
        }

        /// <summary>
        /// Create the first population by inserting random ALIVE(1) and DEAD(0) cells into the game board
        /// </summary>
        protected abstract void CreateInitialPopulation();

        /// <summary>
        /// Check if the current game is stable. Track whether the game has reached a conclusion.
        /// </summary>
        /// <returns>Returns <code>true</code> if the current game of life is stable</returns>
        public abstract bool IsStable();

        /// <summary>
        /// Create the next generation by applying the four rules of the Game of Life
        /// </summary>
        public virtual void CreateNextGeneration()
        {
            int liveNeighborsTotal;

            for (int column = 0; column < Cols; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    liveNeighborsTotal = CountLiveNeighbors(CurrentBoardGeneration, column, row);

                    if (IsDeadByUnderPopulationRule(CurrentBoardGeneration[column, row], liveNeighborsTotal))
                        _nextBoardGeneration[column, row] = DEAD;
                    else if (IsDeadByOverPopulationRule(CurrentBoardGeneration[column, row], liveNeighborsTotal))
                        _nextBoardGeneration[column, row] = DEAD;
                    else if (IsAliveByReproductionRule(CurrentBoardGeneration[column, row], liveNeighborsTotal))
                        _nextBoardGeneration[column, row] = ALIVE;
                    else
                        _nextBoardGeneration[column, row] = CurrentBoardGeneration[column, row];
                }
            }

            MoveNextGenerations();
        }

        /// <summary>
        /// Any live cell with fewer than two live neighbors dies, as if by underpopulation.
        /// </summary>
        /// <param name="currentCell">The current cell (x, y)</param>
        /// <param name="liveNeighborsTotal">The total of neighbors</param>
        /// <returns>True if is dead by underpopulation</returns>
        protected bool IsDeadByUnderPopulationRule(int currentCell, int liveNeighborsTotal)
        {
            return currentCell == ALIVE && liveNeighborsTotal < 2;
        }

        /// <summary>
        /// Any live cell with more than three live neighbors dies, as if by overpopulation.
        /// </summary>
        /// <param name="currentCell">The current cell (x, y)</param>
        /// <param name="liveNeighborsTotal">The total of neighbors</param>
        /// <returns>True if is dead by overpopulation</returns>
        protected bool IsDeadByOverPopulationRule(int currentCell, int liveNeighborsTotal)
        {
            return currentCell == ALIVE && liveNeighborsTotal > 3;
        }

        /// <summary>
        /// Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
        /// </summary>
        /// <param name="currentCell">The current dead cell (x, y)</param>
        /// <param name="liveNeighborsTotal">The total of neighbors</param>
        /// <returns>True if is alive by reproduction</returns>
        protected bool IsAliveByReproductionRule(int currentCell, int liveNeighborsTotal)
        {
            return currentCell == DEAD && liveNeighborsTotal == 3;
        }

        /// <summary>
        /// Count the number of alive neighbors for a given cell
        /// </summary>
        /// <param name="currentBoardGeneration">The current board generation</param>
        /// <param name="currentCellColumn">The current cell column(x or i)</param>
        /// <param name="currentCellRow">The current cell row(y or j)</param>
        /// <returns>Returns the total of alive neighbors</returns>
        protected virtual int CountLiveNeighbors(int[,] currentBoardGeneration, int currentCellColumn, int currentCellRow)
        {
            var liveNeighborsTotal = 0;

            for (int column = -1; column <= 1; column++)
            {
                for (int row = -1; row <= 1; row++)
                {
                    if (IsOutOfHorizontalBoundaries(currentCellColumn, column))
                        continue;
                    if (IsOutOfVerticalBoundaries(currentCellRow, row))
                        continue;
                    if (IsTheSameCell(currentCellColumn, column, currentCellRow, row))
                        continue;

                    // Add cells value
                    liveNeighborsTotal += currentBoardGeneration[currentCellColumn + column, currentCellRow + row];
                }
            }

            return liveNeighborsTotal;
        }

        /// <summary>
        /// Check if a cell should be ignored because it's out of the horizontal boundaries of the game board
        /// </summary>
        /// <param name="currentCellColumn">The current cell column</param>
        /// <param name="column">The current column of the <code>for</code> loop</param>
        /// <returns>Returns <code>true</code> when the cell is out of the horizontal boundaries</returns>
        private bool IsOutOfHorizontalBoundaries(int currentCellColumn, int column)
        {
            return (currentCellColumn + column < 0 || currentCellColumn + column >= Cols);
        }

        /// <summary>
        /// Check if a cell should be ignored because it's out of the vertical boundaries of the game board
        /// </summary>
        /// <param name="currentCellRow">The current cell row</param>
        /// <param name="row">The current row of the <code>for</code> loop</param>
        /// <returns>Returns <code>true</code> when the cell is out of the vertical boundaries</returns>
        private bool IsOutOfVerticalBoundaries(int currentCellRow, int row)
        {
            return (currentCellRow + row < 0 || currentCellRow + row >= Rows);
        }

        /// <summary>
        /// Check if a cell should be ignored because its position is the same as the current cell
        /// </summary>
        /// <param name="currentCellColumn">The current cell column</param>
        /// <param name="column">The current column of the <code>for</code> loop</param>
        /// <param name="currentCellRow">The current cell row</param>
        /// <param name="row">The current row of the <code>for</code> loop</param>
        /// <returns>Returns <code>true</code> when the cell is the same as the current cell</returns>
        private bool IsTheSameCell(int currentCellColumn, int column, int currentCellRow, int row)
        {
            var isTheSameColumn = currentCellColumn + column == currentCellColumn;
            var isTheSameRow = currentCellRow + row == currentCellRow;

            return isTheSameColumn && isTheSameRow;
        }

        /// <summary>
        /// Transfer next generation to current generation 
        /// </summary>
        private void MoveNextGenerations()
        {
            for (int column = 0; column < Cols; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    CurrentBoardGeneration[column, row] = _nextBoardGeneration[column, row];
                }
            }
        }
    }
}
