namespace Lykke.Service.OperationsCache.Settings.ServiceSettings
{
    public class OperationsCacheSettings
    {
        public string CacheInstanceName { get; set; }
        public int ItemsPerPage { get; set; }
        public DbSettings Db { get; set; }
    }
}
