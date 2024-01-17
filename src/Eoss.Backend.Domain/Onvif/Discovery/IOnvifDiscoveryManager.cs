using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif
{
    public interface IOnvifDiscoveryManager : IDomainService
    {
        Task<List<DiscoveredDevice>> DiscoveryDeviceAsync(int timeoutSecs = 1);
    }
}
