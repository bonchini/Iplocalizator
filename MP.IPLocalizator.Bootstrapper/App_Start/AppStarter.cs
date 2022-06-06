using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MP.IPLocalizator.Business.IConnectors;
using MP.IPLocalizator.Business.Localizators;
using MP.IPLocalizator.Business.Searchers;
using MP.IPLocalizator.Business.Updaters;
using MP.IPLocalizator.Integration.Clients;
using MP.IPLocalizator.Integration.Connectors;
using MP.IPLocalizator.Integration.Data;
using MP.IPLocalizator.IServices;
using MP.IPLocalizator.Persistence.Searcher;
using MP.IPLocalizator.Persistence.Settings;
using MP.IPLocalizator.Persistence.Updaters;
using MP.IPLocalizator.Services;
using StackExchange.Redis;

namespace MP.IPLocalizator.Bootstrapper.App_Start
{
    public class AppStarter
    {      
        public static void StartServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureBusiness(services, configuration);
            ConfigureIntegration(services, configuration);
            ConfigureServices(services, configuration);
            ConfigurePersistence(services, configuration);
        }

        public static void ConfigureIntegration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Ip2CountryCredentials>(configuration.GetSection("Ip2CountryCredentials"));
            services.Configure<RestCountriesCredentials>(configuration.GetSection("RestCountriesCredentials"));
            services.Configure<ApiLayerCredentials>(configuration.GetSection("ApiLayerCredentials"));
            services.AddTransient<Ip2CountryClient>();
            services.AddTransient<RestCountryClient>();
            services.AddTransient<ApiLayerClient>();
            services.AddTransient<ICountryConnector, CountryConnector>();
            services.AddTransient<IIpCountryConnector, IpCountryConnector>();
            services.AddTransient<ICurrencyRateConnector, CurrencyRateConnector>();
        }
        public static void ConfigureBusiness(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IpLocalizator>();
            services.AddTransient<DistanceMetricsSearcher>();
        }
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ILocalizatorService, LocalizatorService>();
        }        
        public static void ConfigurePersistence(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDistanceUpdater, RedisDistanceUpdater>();
            services.AddTransient<IDistanceSearcher, RedisDistanceSearcher>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });

            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                try
                {
                    return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
            ThreadPool.SetMinThreads(300, 300);
        }
    }
}
