using System;
using Autofac;
using Common.Log;
using Lykke.Common.Log;

namespace Lykke.Service.OperationsCache.Client
{
    public static class AutofacExtension
    {
        [Obsolete("Please, use the overload which consumes ILogFactory instead.")]
        public static void RegisterOperationsCacheClient(this ContainerBuilder builder, string serviceUrl, ILog log)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (log == null) throw new ArgumentNullException(nameof(log));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterType<OperationsCacheClient>()
                .WithParameter("serviceUrl", serviceUrl)
                .As<IOperationsCacheClient>()
                .SingleInstance();
        }

        public static void RegisterOperationsCacheClient(this ContainerBuilder builder, string serviceUrl, ILogFactory logFactory)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (logFactory == null) throw new ArgumentNullException(nameof(logFactory));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterType<OperationsCacheClient>()
                .WithParameter("serviceUrl", serviceUrl)
                .WithParameter("log", logFactory.CreateLog(nameof(OperationsCacheClient)))
                .As<IOperationsCacheClient>()
                .SingleInstance();
        }

        [Obsolete("Please, use the overload which consumes ILogFactory instead.")]
        public static void RegisterOperationsCacheClient(this ContainerBuilder builder, OperationsCacheServiceClientSettings settings, ILog log)
        {
            builder.RegisterOperationsCacheClient(settings?.ServiceUrl, log);
        }

        public static void RegisterOperationsCacheClient(this ContainerBuilder builder, OperationsCacheServiceClientSettings settings, ILogFactory logFactory)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            builder.RegisterOperationsCacheClient(settings.ServiceUrl, logFactory);
        }
    }
}
