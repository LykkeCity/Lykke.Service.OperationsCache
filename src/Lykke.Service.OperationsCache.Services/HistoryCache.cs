using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.OperationsCache.Core.Domain;
using Lykke.Service.OperationsCache.Core.Services;
using MessagePack;
using Microsoft.Extensions.Caching.Distributed;

namespace Lykke.Service.OperationsCache.Services
{
    public class HistoryCache : IHistoryCache
    {
        private readonly IDistributedCache _redisCache;
        private readonly int _valuesPerPage;

        public HistoryCache(
            IDistributedCache redisCache,
            int valuesPerPage
            )
        {
            _redisCache = redisCache;
            _valuesPerPage = valuesPerPage;
        }
        
        public async Task<IEnumerable<HistoryEntry>> GetRecordsByClient(string clientId, int page = 1)
        {
            var value = await _redisCache.GetAsync(GetCacheKey(clientId));
            if (value == null)
                return Array.Empty<HistoryEntry>();

            var cacheModel = MessagePackSerializer.Deserialize<CacheModel>(value);
            
            int skip = cacheModel.Records.Count <= _valuesPerPage && page > 1
                ? 0
                : (page - 1) * _valuesPerPage;
            
            return cacheModel.Records
                .Skip(skip)
                .Take(_valuesPerPage);
        }
        
        private static string GetCacheKey(string clientId)
        {
            return $":client:{clientId}:history";
        }
    }
}
