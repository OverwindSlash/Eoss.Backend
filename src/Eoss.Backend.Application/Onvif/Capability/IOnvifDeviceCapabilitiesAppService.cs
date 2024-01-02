using Abp.Application.Services;
using Eoss.Backend.Onvif.Capability.Dto;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif.Capability
{
    public interface IOnvifDeviceCapabilitiesAppService : IApplicationService
    {
        Task<CapabilitiesDto> GetCapabilitiesAsync(string host, string username, string password);
    }
}
