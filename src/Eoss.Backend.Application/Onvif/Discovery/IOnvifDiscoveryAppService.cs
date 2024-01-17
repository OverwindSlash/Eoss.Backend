using Abp.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eoss.Backend.Onvif.Dto;

namespace Eoss.Backend.Onvif
{
    public interface IOnvifDiscoveryAppService : IApplicationService
    {
        Task<List<DiscoveredDeviceDto>> DiscoveryDeviceAsync(int timeoutSecs = 1);
    }
}
