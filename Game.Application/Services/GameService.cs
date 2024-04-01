using Game.Domain.Interfaces.Services;

namespace Game.Application.Services
{
    internal class GameService : IGameService
    {
        public GameService()
        {
            
        }

        public void Start()
        {
            Console.WriteLine("Ok");
        }
    }
}
