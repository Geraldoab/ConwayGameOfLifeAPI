using Game.Domain.Interfaces.Repositories;
using Infra.Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterData
    {
        public static IServiceCollection RegisterDataDependencies(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();

            return services;
        }
    }
}
