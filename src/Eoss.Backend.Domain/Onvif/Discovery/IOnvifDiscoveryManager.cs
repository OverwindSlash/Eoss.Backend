using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif.Discovery
{
    public interface IOnvifDiscoveryManager : IDomainService
    {
        Task<List<DiscoveredDevice>> DiscoveryDeviceAsync(int timeOutSecs = 1);
    }
}
