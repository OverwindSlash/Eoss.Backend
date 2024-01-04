﻿using Abp.Application.Services;
using Eoss.Backend.Domain.Onvif.Ptz;
using Eoss.Backend.Onvif.Ptz.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif.Ptz
{
    public class OnvifPtzAppService : ApplicationService, IOnvifPtzAppService
    {
        private readonly IOnvifPtzManager _ptzManager;

        public OnvifPtzAppService(IOnvifPtzManager ptzManager)
        {
            _ptzManager = ptzManager;
        }

        [HttpGet]
        public async Task<List<PtzConfigDto>> GetConfigurationsAsync(string host, string username, string password)
        {
            var ptzConfigs = await _ptzManager.GetConfigurationsAsync(host, username, password);
            return ObjectMapper.Map<List<PtzConfigDto>>(ptzConfigs);
        }

        [HttpGet]
        public async Task<PtzStatusDto> GetStatusAsync(string host, string username, string password, string profileToken)
        {
            var ptzStatus = await _ptzManager.GetStatusAsync(host,username,password,profileToken);
            return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
        }

        public async Task<PtzStatusDto> AbsoluteMoveAsync(string host, string username, string password, string profileToken,
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            var ptzStatus = await _ptzManager.AbsoluteMoveAsync(host, username, password, profileToken, pan, tilt, zoom, 
                panSpeed, tiltSpeed, zoomSpeed);
            return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
        }

        public async Task<PtzStatusDto> RelativeMoveAsync(string host, string username, string password, string profileToken, 
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            var ptzStatus = await _ptzManager.RelativeMoveAsync(host, username, password, profileToken, pan, tilt, zoom,
                panSpeed, tiltSpeed, zoomSpeed);
            return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
        }

        public async Task ContinuousMoveAsync(string host, string username, string password, string profileToken, 
            float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            await _ptzManager.ContinuousMoveAsync(host, username, password, profileToken, panSpeed, tiltSpeed, zoomSpeed);
        }

        public async Task<PtzStatusDto> StopAsync(string host, string username, string password, string profileToken, bool stopPan, bool stopZoom)
        {
            var ptzStatus = await _ptzManager.StopAsync(host, username, password, profileToken, stopPan, stopZoom);
            return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
        }
    }
}