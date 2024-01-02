using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif.Capability
{
    public interface IOnvifDeviceCapabilityManager : IDomainService
    {
        Task<Capabilities> GetCapabilitiesAsync(string host, string username, string password);
    }
}
