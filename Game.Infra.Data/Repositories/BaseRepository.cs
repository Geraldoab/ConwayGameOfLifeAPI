using Game.Domain.Interfaces.Repositories;
using Game.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Game.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class, new()
    {
        protected GameContext GameContext;
        protected DbSet<T> DbSet;

        public BaseRepository(GameContext context)
        {
            GameContext = context;
            DbSet = GameContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await GameContext.Set<T>().AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await GameContext.Set<T>().AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await DbSet.CountAsync(cancellationToken);
        }

        public async Task<T?> FindAsync(object id, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync(id, cancellationToken);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await GameContext.Set<T>().AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> EditAsync(object id, T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                return null;

            T? selectedEntity = await FindAsync(id, cancellationToken);

            if (selectedEntity != null)
                GameContext.Entry(selectedEntity).CurrentValues.SetValues(entity);

            return selectedEntity;
        }

        public async Task<T?> RemoveByIdAsync(object id, CancellationToken cancellationToken)
        {
            var selectedEntity = await FindAsync(id, cancellationToken).ConfigureAwait(false);
            
            if (selectedEntity != null)
                GameContext.Set<T>().Remove(selectedEntity);

            return selectedEntity;
        }

        public async Task<int> CommitAsync()
        {
            var result = await GameContext.SaveChangesAsync().ConfigureAwait(false);

            foreach (var entity in GameContext.ChangeTracker.Entries())
                entity.State = EntityState.Detached;

            return result;
        }

        public void Dispose()
        {
            GameContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
