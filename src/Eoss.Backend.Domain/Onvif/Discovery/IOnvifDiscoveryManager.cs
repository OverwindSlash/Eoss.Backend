using Abp.Domain.Services;

namespace Eoss.Backend.Domain.Onvif.Discovery
{
    public interface IOnvifDiscoveryManager : IDomainService
    {
        Task<List<DiscoveredDevice>> DiscoveryDeviceAsync();
    }
}
