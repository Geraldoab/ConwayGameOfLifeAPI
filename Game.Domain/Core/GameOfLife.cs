namespace Game.Domain.Core
{
    public sealed class GameOfLife : BaseGameOfLife
    {
        public GameOfLife() : base() { }
        public GameOfLife(int numberOfColumns, int numberOfRows) : base(numberOfColumns, numberOfRows) { }

        public GameOfLife(int numberOfColumns, int numberOfRows, int[,] cells) : base(numberOfColumns, numberOfRows, cells) { }

        public override bool IsStable()
        {
            for (int column = 0; column < Cols; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    if (CurrentBoardGeneration[column, row] != PreviousBoardGeneration[column, row])
                        return false;
                }
            }

            return true;
        }

        protected override void CreateInitialPopulation()
        {
            CurrentBoardGeneration = new int[Cols, Rows];
            PreviousBoardGeneration = new int[Cols, Rows];

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
