using Game.Domain.Entities;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task<string> UploadAsync(Grid grid, CancellationToken cancellationToken);
        Task<BoardState?> GetNextStateAsync(CancellationToken cancellationToken);
        Task<Grid?> SimulateAsync(int iterations, CancellationToken cancellationToken);
        Task<BoardState?> GetFinalStateAsync(int iterations, CancellationToken cancellationToken);
        Task<BoardState?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BoardState?> RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
