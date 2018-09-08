using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsCache.Client.AutorestClient;
using Lykke.Service.OperationsCache.Client.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace Lykke.Service.OperationsCache.Client
{
    public class OperationsCacheClient : IOperationsCacheClient, IDisposable
    {
        private readonly ILog _log; // Unused. Left for compatibility.
        private IOperationsCacheAPI _apiClient;

        [Obsolete("Please, use the overload which does not consume logger.")]
        public OperationsCacheClient(string serviceUrl, ILog log)
        {
            _log = log;
            _apiClient = new OperationsCacheAPI(new Uri(serviceUrl), new HttpClient());
        }

        public OperationsCacheClient(string serviceUrl)
        {
            _apiClient = new OperationsCacheAPI(new Uri(serviceUrl), new HttpClient());
        }

        public void Dispose()
        {
            if (_apiClient == null)
                return;
            _apiClient.Dispose();
            _apiClient = null;
        }

        public async Task<IEnumerable<HistoryClientEntry>> GetHistoryByClientId(string clientId)
        {
            var operations = await _apiClient.GetHistoryAsync(clientId);
            return operations == null
                ? new List<HistoryClientEntry>()
                : operations.Select(x => x.FromApiModel());
        }

        public async Task<IEnumerable<HistoryClientEntry>> GetAssetHistoryByClientIdAsync(string clientId, string assetId)
        {
            var operations = await _apiClient.GetAssetHistoryAsync(clientId, assetId);
            return operations == null
                ? new List<HistoryClientEntry>()
                : operations.Select(x => x.FromApiModel());
        }

        public async Task<IEnumerable<HistoryClientEntry>> GetAssetsHistoryByClientIdAsync(string clientId, string[] assetIds)
        {
            var operations = await _apiClient.GetAssetsHistoryAsync(clientId, assetIds);
            return operations == null
                ? new List<HistoryClientEntry>()
                : operations.Select(x => x.FromApiModel());
        }
    }
}
