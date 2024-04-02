using Game.Domain.Core;

namespace Game.Interface.Core.Strategy
{
    internal class ConsoleRenderConcreteStrategy : RenderStrategy
    {
        private readonly BaseGameOfLife _currentGameOfLife;

        public ConsoleRenderConcreteStrategy(BaseGameOfLife currentGameOfLife)
        {
            _currentGameOfLife = currentGameOfLife;
            Setup();
        }

        public override void Render()
        {
            for (int column = 0; column < _currentGameOfLife.Cols; column++)
            {
                for (int row = 0; row < _currentGameOfLife.Rows; row++)
                {
                    if (_currentGameOfLife.CurrentBoardGeneration[column, row] == BaseGameOfLife.ALIVE)
                        Console.Write("██");
                    else
                        Console.Write("  ");
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(0, 0);
        }

        private void Setup()
        {
            Console.CursorVisible = false;
        }
    }
}
