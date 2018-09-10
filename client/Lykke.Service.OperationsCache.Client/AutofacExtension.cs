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
