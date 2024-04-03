using Game.Domain.Core;
using Game.Interface.Core.Strategy;

namespace Game.Interface
{
    internal class Program
    {
        private const int DELAY_MILLISECONDS = 1750;
        private const int BOARD_SIZE = 36;
        private const int MAX_GENERATIONS = 36;

        static void Main(string[] args)
        {
            BaseGameOfLife newGame = new GameOfLife(BOARD_SIZE, BOARD_SIZE);

            var strategy = new RenderContext();
            Console.WriteLine("-------------------- Select The Render Mode --------------------");
            Console.WriteLine("1 - Console");
            Console.WriteLine("2 - Image");

            var selectedOption = Console.ReadLine();

            Console.WriteLine("----------------------------------------- ((( Conway's Game of Life ))) -----------------------------------------");

            switch (selectedOption)
            {
                case "1":
                    {
                        strategy.RenderStrategy = new ConsoleRenderConcreteStrategy(newGame);

                        Thread.Sleep(DELAY_MILLISECONDS);

                        for (int i = 0; i < MAX_GENERATIONS; i++)
                        {
                            Console.Clear();
                            Console.WriteLine($" - Generation ({i})");
                            Console.WriteLine();

                            strategy.Execute();
                            newGame.CreateNextGeneration();

                            Thread.Sleep(DELAY_MILLISECONDS);
                        }

                        break;
                    }
                case "2":
                    {
                        strategy.RenderStrategy = new ImageRenderConcreteStrategy(newGame);
                        strategy.Execute();
                        break;
                    }
                default:
                    {
                        Console.WriteLine($"The selected operation ({selectedOption}) is invalid");
                        break;
                    }
            }

            Console.ReadLine();
        }
    }
}
