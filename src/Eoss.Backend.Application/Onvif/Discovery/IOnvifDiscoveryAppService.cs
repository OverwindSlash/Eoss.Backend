using Abp.Application.Services;
using Eoss.Backend.Onvif.Discovery.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif.Discovery
{
    public interface IOnvifDiscoveryAppService : IApplicationService
    {
        Task<List<DiscoveredDeviceDto>> DiscoveryDeviceAsync();
    }
}
