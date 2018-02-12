using System.Threading.Tasks;

namespace Lykke.Service.OperationsCache.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}