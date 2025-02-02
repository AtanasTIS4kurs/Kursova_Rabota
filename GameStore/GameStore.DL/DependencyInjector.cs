using GameStore.DL.Interface;
using GameStore.DL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IGameRepository, GamesMongoRepository>()
                .AddScoped<ICompanyRepository, CompaniesMongoRepository>();
        }
    }
}
