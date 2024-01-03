using Abp.Application.Services;
using Eoss.Backend.Domain.Onvif.Capability;
using Eoss.Backend.Onvif.Capability.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif.Capability
{
    public class OnvifDeviceCapabilitiesAppService : ApplicationService, IOnvifDeviceCapabilitiesAppService
    {
        private readonly IOnvifDeviceCapabilityManager _capabilityManager;

        public OnvifDeviceCapabilitiesAppService(IOnvifDeviceCapabilityManager capabilityManager)
        {
            _capabilityManager = capabilityManager;
        }

        [HttpGet]
        public async Task<CapabilitiesDto> GetCapabilitiesAsync(string host, string username, string password)
        {
            var capabilities = await _capabilityManager.GetCapabilitiesAsync(host, username, password);
            return ObjectMapper.Map<CapabilitiesDto>(capabilities);
        }
    }
}
