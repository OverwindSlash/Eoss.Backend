using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif.Device
{
    public interface IOnvifDeviceManager : IDomainService
    {
        Task<Entities.Device> GetDeviceInfoAsync(string host, string username, string password);

        Task<Capabilities> GetCapabilitiesAsync(string host, string username, string password);
    }
}
