using Abp.Application.Services;
using Eoss.Backend.Onvif.Capability.Dto;
using System.Threading.Tasks;
using Eoss.Backend.CloudSense.Device.Dto;

namespace Eoss.Backend.Onvif.Capability
{
    public interface IOnvifDeviceAppService : IApplicationService
    {
        Task<DeviceGetDto> GetDeviceInfoAsync(string host, string username, string password);
        Task<CapabilitiesDto> GetCapabilitiesAsync(string host, string username, string password);
    }
}
