using Game.Domain.Core;

namespace Game.Domain.Model
{
    public class GameOfLife : BaseGameOfLife
    {
        public GameOfLife() : base() { }
        public GameOfLife(int numberOfColumns, int numberOfRows) : base(numberOfColumns, numberOfRows) { }

        protected override void CreateInitialPopulation()
        {
            CurrentBoardGeneration = new int[Cols, Rows];
            _nextBoardGeneration = new int[Cols, Rows];
            var randomNumber = new Random();

            for (int column = 0; column < Cols; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    CurrentBoardGeneration[column, row] = randomNumber.Next(2);
                }
            }
        }
    }
}
