using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.Service.OperationsCache.Core.Services;
using Lykke.Service.OperationsCache.Services;
using Lykke.Service.OperationsCache.Settings;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.OperationsCache.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _settings;
        private readonly ILog _log;
        private readonly IServiceCollection _services;

        public ServiceModule(IReloadingManager<AppSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            _services.AddDistributedRedisCache(options =>
            {
                options.Configuration = _settings.CurrentValue.RedisSettings.Configuration;
                options.InstanceName = _settings.CurrentValue.OperationsCacheService.CacheInstanceName;
            });
            
            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();

            builder.RegisterType<HistoryCache>()
                .WithParameter(TypedParameter.From(_settings.CurrentValue.OperationsCacheService.ItemsPerPage))
                .As<IHistoryCache>()
                .SingleInstance();

            builder.Populate(_services);
        }
    }
}
