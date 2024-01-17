using Abp.Application.Services;
using Eoss.Backend.Onvif.Dto;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif
{
    public interface IOnvifDeviceAppService : IApplicationService
    {
        Task<DeviceInfoDto> GetDeviceInfoAsync(string host, string username, string password);
        Task<CapabilitiesDto> GetCapabilitiesAsync(string host, string username, string password);
    }
}
