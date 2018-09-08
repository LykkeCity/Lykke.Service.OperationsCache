using System;
using Autofac;
using Common.Log;

namespace Lykke.Service.OperationsCache.Client
{
    public static class AutofacExtension
    {
        /// <summary>
        /// Adds Operations Cache client to the ContainerBuilder.
        /// </summary>
        /// <param name="builder">ContainerBuilder instance.</param>
        /// <param name="serviceUrl">Effective Operations Cache service location.</param>
        /// <param name="log">Logger.</param>
        [Obsolete("Please, use the overload without explicitly passed logger.")]
        public static void RegisterOperationsCacheClient(this ContainerBuilder builder, string serviceUrl, ILog log)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (log == null) throw new ArgumentNullException(nameof(log));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterType<OperationsCacheClient>()
                .WithParameter("serviceUrl", serviceUrl)
                .WithParameter("log", log)
                .As<IOperationsCacheClient>()
                .SingleInstance();
        }

        /// <summary>
        /// Adds Operations Cache client to the ContainerBuilder.
        /// </summary>
        /// <param name="builder">ContainerBuilder instance. It's not required to have a logger injected in.</param>
        /// <param name="serviceUrl">Effective Operations Cache service location.</param>
        public static void RegisterOperationsCacheClient(this ContainerBuilder builder, string serviceUrl)
        {
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterType<OperationsCacheClient>()
                .WithParameter("serviceUrl", serviceUrl)
                .As<IOperationsCacheClient>()
                .SingleInstance();
        }

        /// <summary>
        /// Adds Operations Cache client to the ContainerBuilder.
        /// </summary>
        /// <param name="builder">ContainerBuilder instance.</param>
        /// <param name="settings">Settings.</param>
        /// <param name="log">Logger.</param>
        [Obsolete("Please, use the overload without explicitly passed logger.")]
        public static void RegisterOperationsCacheClient(this ContainerBuilder builder, OperationsCacheServiceClientSettings settings, ILog log)
        {
            builder.RegisterOperationsCacheClient(settings?.ServiceUrl, log);
        }

        /// <summary>
        /// Adds Operations Cache client to the ContainerBuilder.
        /// </summary>
        /// <param name="builder">ContainerBuilder instance. It's not required to have a logger injected in.</param>
        /// <param name="settings">Settings.</param>
        public static void RegisterOperationsCacheClient(this ContainerBuilder builder, OperationsCacheServiceClientSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            builder.RegisterOperationsCacheClient(settings.ServiceUrl);
        }
    }
}
