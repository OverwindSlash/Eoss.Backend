using Abp.Application.Services;
using Abp.UI;
using Eoss.Backend.Domain.Onvif;
using Eoss.Backend.Onvif.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif
{
    public class OnvifDiscoveryAppService : ApplicationService, IOnvifDiscoveryAppService
    {
        private readonly IOnvifDiscoveryManager _discoveryManager;

        public OnvifDiscoveryAppService(IOnvifDiscoveryManager discoveryManager)
        {
            _discoveryManager = discoveryManager;
        }

        [HttpGet]
        public async Task<List<DiscoveredDeviceDto>> DiscoveryDeviceAsync(int timeoutSecs = 1)
        {
            try
            {
                var discoveredDevices = await _discoveryManager.DiscoveryDeviceAsync(timeoutSecs);
                return ObjectMapper.Map<List<DiscoveredDeviceDto>>(discoveredDevices);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
    }
}
