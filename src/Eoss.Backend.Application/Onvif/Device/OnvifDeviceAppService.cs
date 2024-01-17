﻿using Abp.Application.Services;
using Abp.UI;
using Eoss.Backend.Domain.Onvif;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Eoss.Backend.CloudSense.Dto;
using Eoss.Backend.Onvif.Dto;

namespace Eoss.Backend.Onvif
{
    public class OnvifDeviceAppService : ApplicationService, IOnvifDeviceAppService
    {
        private readonly IOnvifDeviceManager _capabilityManager;

        public OnvifDeviceAppService(IOnvifDeviceManager capabilityManager)
        {
            _capabilityManager = capabilityManager;
        }

        [HttpGet]
        public async Task<DeviceGetDto> GetDeviceInfoAsync(string host, string username, string password)
        {
            try
            {
                var deviceInfo = await _capabilityManager.GetDeviceInfoAsync(host, username, password);
                return ObjectMapper.Map<DeviceGetDto>(deviceInfo);
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
                var capabilities = await _capabilityManager.GetCapabilitiesAsync(host, username, password);
                return ObjectMapper.Map<CapabilitiesDto>(capabilities);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
    }
}
