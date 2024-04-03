using System.Linq.Expressions;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task<T?> FindAsync(object id, CancellationToken cancellationToken);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<T?> RemoveByIdAsync(object id, CancellationToken cancellationToken);
        Task<T?> EditAsync(object id, T entity, CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<int> CommitAsync();
        void Dispose();
    }
}
