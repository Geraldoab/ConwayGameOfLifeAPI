using Game.Application.Services;
using Game.Domain.Interfaces.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterApplication
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
