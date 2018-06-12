using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.OperationsCache.Settings.ServiceSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
