using Abp.Application.Services;
using Eoss.Backend.Onvif.Dto;
using System.Threading.Tasks;
using Eoss.Backend.CloudSense.Dto;

namespace Eoss.Backend.Onvif
{
    public interface IOnvifDeviceAppService : IApplicationService
    {
        Task<DeviceGetDto> GetDeviceInfoAsync(string host, string username, string password);
        Task<CapabilitiesDto> GetCapabilitiesAsync(string host, string username, string password);
    }
}
