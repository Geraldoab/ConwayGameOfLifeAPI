using Game.Domain.Interfaces.Repositories;
using Game.Infra.Data.Context;
using Game.Infra.Data.Repositories;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterData
    {
        public static IServiceCollection RegisterDataDependencies(this IServiceCollection services)
        {
            services.AddDbContext<GameContext>(options =>
            {
                options.UseInMemoryDatabase("GameOfLife").ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IGameRepository, GameRepository>();

            return services;
        }
    }
}
