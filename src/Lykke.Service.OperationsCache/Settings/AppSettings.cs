using Lykke.Service.OperationsCache.Settings.ServiceSettings;
using Lykke.Service.OperationsCache.Settings.SlackNotifications;

namespace Lykke.Service.OperationsCache.Settings
{
    public class AppSettings
    {
        public OperationsCacheSettings OperationsCacheService { get; set; }
        public RedisSettings RedisSettings { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
