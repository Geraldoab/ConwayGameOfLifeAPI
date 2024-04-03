using FluentValidation.Results;
using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Game.Infra.Data.Context
{
    public class GameContext : DbContext
    {
        public GameContext()
        {
        }

        public GameContext(DbContextOptions<GameContext> options) : base(options) { }

        public DbSet<Grid> Grids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameContext).Assembly);

            /*
             * For test purposes only. In a production environment with many microservices, a rollback strategy could be quite complex to implement.
             * However, we can use some patterns like SAGA Pattern to achieve it.
             */
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}
