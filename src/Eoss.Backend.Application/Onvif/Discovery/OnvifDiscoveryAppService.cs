using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Eoss.Backend.Domain.Onvif.Discovery;
using Eoss.Backend.Onvif.Discovery.Dto;

namespace Eoss.Backend.Onvif.Discovery
{
    public class OnvifDiscoveryAppService : ApplicationService, IOnvifDiscoveryAppService
    {
        private readonly IOnvifDiscoveryManager _discoveryManager;

        public OnvifDiscoveryAppService(IOnvifDiscoveryManager discoveryManager)
        {
            _discoveryManager = discoveryManager;
        }

        public async Task<List<DiscoveredDeviceDto>> DiscoveryDeviceAsync()
        {
            var discoveryDeviceDtos = await _discoveryManager.DiscoveryDeviceAsync();
            return ObjectMapper.Map<List<DiscoveredDeviceDto>>(discoveryDeviceDtos);
        }
    }
}
