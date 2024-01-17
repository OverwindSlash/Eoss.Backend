using Abp.Application.Services;
using Abp.UI;
using Eoss.Backend.Domain.Onvif;
using Eoss.Backend.Onvif.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif
{
    public class OnvifDeviceAppService : ApplicationService, IOnvifDeviceAppService
    {
        private readonly IOnvifDeviceManager _deviceManager;

        public OnvifDeviceAppService(IOnvifDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        [HttpGet]
        public async Task<DeviceInfoDto> GetDeviceInfoAsync(string host, string username, string password)
        {
            try
            {
                var device = await _deviceManager.GetDeviceInfoAsync(host, username, password);
                return ObjectMapper.Map<DeviceInfoDto>(device);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        [HttpGet]
        public async Task<CapabilitiesDto> GetCapabilitiesAsync(string host, string username, string password)
        {
            try
            {
                var capabilities = await _deviceManager.GetCapabilitiesAsync(host, username, password);
                return ObjectMapper.Map<CapabilitiesDto>(capabilities);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
    }
}
