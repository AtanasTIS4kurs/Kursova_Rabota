using GameStore.DL.Cache;
using GameStore.DL.Gateway;
using GameStore.DL.Interface;
using GameStore.DL.Interfaces;
using GameStore.DL.Kafka;
using GameStore.DL.Kafka.KafkaCache;
using GameStore.DL.Repositories;
using GameStore.Models.Configuration.CachePopulator;
using GameStore.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            RegisterRepositories(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddScoped<IGameRepository, GamesMongoRepository>()
                .AddScoped<ICompanyRepository, CompaniesMongoRepository>()
                .AddScoped<IGameOrderGateway, GameOrderGateway>()
                .AddCache<CompanyCacheConfiguration, CompaniesMongoRepository, Company, string>(config)
                .AddCache<GamesCacheConfiguration, GamesMongoRepository, Game, string>(config)
                .AddHostedService<KafkaCache<string, Game>>();
            return services;
        }
        public static IServiceCollection AddCache<TCacheConfiguration, TCacheRepository, TData, TKey>(this IServiceCollection services, IConfiguration config)
           where TCacheConfiguration : CacheConfiguration
           where TCacheRepository : class, ICacheRepository<TKey, TData>
           where TData : ICacheItem<TKey>
           where TKey : notnull
        {
            var configSection = config.GetSection(typeof(TCacheConfiguration).Name);

            if (!configSection.Exists())
            {
                throw new ArgumentNullException(typeof(TCacheConfiguration).Name, "Configuration section is missing!");
            }

            services.Configure<TCacheConfiguration>(configSection);

            services.AddSingleton<ICacheRepository<TKey, TData>, TCacheRepository>();
            services.AddSingleton<IKafkaProducer<TData>, KafkaProducer<TKey, TData, TCacheConfiguration>>();
            services.AddHostedService<MongoCachePopulator<TData, TCacheConfiguration, TKey>>();

            return services;
        }
    }
}
