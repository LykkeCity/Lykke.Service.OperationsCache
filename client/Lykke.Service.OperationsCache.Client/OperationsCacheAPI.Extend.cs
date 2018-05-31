using System;
using System.Net.Http;

// ReSharper disable once CheckNamespace
namespace Lykke.Service.OperationsCache.AutorestClient
{
    public partial class OperationsCacheAPI
    {
        /// <inheritdoc />
        /// <summary>
        /// Should be used to prevent memory leak in RetryPolicy
        /// </summary>
        public OperationsCacheAPI(Uri baseUri, HttpClient client) : base(client)
        {
            Initialize();

            BaseUri = baseUri ?? throw new ArgumentNullException("baseUri");
        }
    }
}
