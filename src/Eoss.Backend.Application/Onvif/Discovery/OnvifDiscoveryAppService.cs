using System;
using Abp.Application.Services;
using Eoss.Backend.Domain.Onvif.Discovery;
using Eoss.Backend.Onvif.Discovery.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.UI;

namespace Eoss.Backend.Onvif.Discovery
{
    public class OnvifDiscoveryAppService : ApplicationService, IOnvifDiscoveryAppService
    {
        private readonly IOnvifDiscoveryManager _discoveryManager;

        public OnvifDiscoveryAppService(IOnvifDiscoveryManager discoveryManager)
        {
            _discoveryManager = discoveryManager;
        }

        [HttpGet]
        public async Task<List<DiscoveredDeviceDto>> DiscoveryDeviceAsync()
        {
            try
            {
                var discoveryDeviceDtos = await _discoveryManager.DiscoveryDeviceAsync();
                return ObjectMapper.Map<List<DiscoveredDeviceDto>>(discoveryDeviceDtos);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
    }
}
