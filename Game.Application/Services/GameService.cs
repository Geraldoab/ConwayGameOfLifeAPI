using Game.Domain.Interfaces.Services;
using Game.Domain.Model;

namespace Game.Application.Services
{
    internal class GameService : IGameService
    {
        public GameService()
        {
            
        }

        public void Start()
        {
            GameOfLife newGame = new GameOfLife();

            var currentBoard = newGame.CurrentBoardGeneration;

            Console.WriteLine("Ok");
        }
    }
}
