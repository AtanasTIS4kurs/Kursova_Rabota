using GameStore.BL.Interfaces;
using GameStore.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IGameService, GameService>()
                .AddScoped<ICompanyService, CompanyService>()
                .AddScoped<IBusinessService, BusinessService>();
        }
    }
}
