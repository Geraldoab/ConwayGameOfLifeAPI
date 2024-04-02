using Game.Domain.Model;
using Game.Infra.Data.Core;

namespace Game.Domain.Interfaces.Services
{
    public interface IGameService
    {
        CustomResult Upload(Grid grid);
        CustomResult GetNextState();
        CustomResult Simulate(int iterations);
        CustomResult GetFinalState(int iterations);
        CustomResult GetById(int id);
        CustomResult RemoveById(int id);
    }
}
