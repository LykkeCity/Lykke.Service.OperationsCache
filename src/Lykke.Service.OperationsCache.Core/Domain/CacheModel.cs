using System.Collections.Generic;
using MessagePack;

namespace Lykke.Service.OperationsCache.Core.Domain
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class CacheModel
    {
        public List<HistoryEntry> Records;
    }
}
