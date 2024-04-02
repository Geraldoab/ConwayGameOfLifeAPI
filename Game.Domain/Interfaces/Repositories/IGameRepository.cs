using Game.Domain.Model;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        int Upload(Grid grid);
        BoardState GetNextState();
        Grid? Simulate(int iterations);
        BoardState? GetFinalState(int iterations);
        BoardState? GetById(int id);
        BoardState? RemoveById(int id);
    }
}
