using Abp.Application.Services;
using Abp.UI;
using Eoss.Backend.Domain.Onvif.Discovery;
using Eoss.Backend.Onvif.Discovery.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<DiscoveredDeviceDto>> DiscoveryDeviceAsync(int timeOutSecs = 1)
        {
            try
            {
                var discoveredDeviceDtos = await _discoveryManager.DiscoveryDeviceAsync(timeOutSecs);
                return ObjectMapper.Map<List<DiscoveredDeviceDto>>(discoveredDeviceDtos);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
    }
}
