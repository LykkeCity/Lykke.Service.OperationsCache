﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsCache.Core.Domain;

namespace Lykke.Service.OperationsCache.Core.Services
{
    public interface IHistoryCache
    {
        Task<IEnumerable<HistoryEntry>> GetRecordsByClient(string clientId, int page = 1);

        Task<IEnumerable<HistoryEntry>> GetRecordsForAssetByClientAsync(string clientId, string assetId);

        Task<IEnumerable<HistoryEntry>> GetRecordsForAssetsByClientAsync(string clientId, string[] assetIds);
    }
}
