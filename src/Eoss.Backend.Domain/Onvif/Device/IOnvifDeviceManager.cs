using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif
{
    public interface IOnvifDeviceManager : IDomainService
    {
        Task<Device> GetDeviceInfoAsync(string host, string username, string password);

        Task<Capabilities> GetCapabilitiesAsync(string host, string username, string password);
    }
}
