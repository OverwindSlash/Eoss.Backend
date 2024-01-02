using Abp.Domain.Services;

namespace Eoss.Backend.Domain.Onvif.Capability
{
    public interface IOnvifDeviceCapabilityManager : IDomainService
    {
        Task<DeviceCapabilities> GetCapabilitiesAsync(string host, string username, string password);
    }
}
