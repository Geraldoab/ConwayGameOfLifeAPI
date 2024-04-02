using Game.Domain.Model;
using Game.Interface.Core;

namespace Game.Interface
{
    internal class Program
    {
        private const int DELAY_MILLISECONDS = 1750;
        private const int BOARD_SIZE = 36;
        private const int MAX_GENERATIONS = 36;

        static void Main(string[] args)
        {
            Console.WriteLine("----------------------------------------- ((( Conway's Game of Life ))) -----------------------------------------");
            Thread.Sleep(DELAY_MILLISECONDS);

            GameOfLife newGame = new GameOfLife(BOARD_SIZE, BOARD_SIZE);
            BaseRender consoleRender = new ConsoleRender(newGame);
            var maxGenerations = MAX_GENERATIONS;

            for(int i = 0; i < maxGenerations; i ++)
            {
                Console.Clear();
                Console.WriteLine($" - Generation ({i})");
                Console.WriteLine();

                consoleRender.Render();
                newGame.CreateNextGeneration();

                Thread.Sleep(DELAY_MILLISECONDS);
            }

            Console.ReadLine();
        }
    }
}
