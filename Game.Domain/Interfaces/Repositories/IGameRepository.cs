using Game.Domain.Entities;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task<int> UploadAsync(Grid grid, CancellationToken cancellationToken);
        Task<BoardState> GetNextStateAsync(CancellationToken cancellationToken);
        Task<Grid?> SimulateAsync(int iterations, CancellationToken cancellationToken);
        Task<BoardState?> GetFinalStateAsync(int iterations, CancellationToken cancellationToken);
        Task<BoardState?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<BoardState?> RemoveByIdAsync(int id, CancellationToken cancellationToken);
    }
}
