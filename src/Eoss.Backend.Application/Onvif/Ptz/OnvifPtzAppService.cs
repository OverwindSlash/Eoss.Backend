using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.UI;
using Eoss.Backend.Domain.Onvif;
using Eoss.Backend.Onvif.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Eoss.Backend.Onvif
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
            try
            {
                var ptzConfigs = await _ptzManager.GetConfigurationsAsync(host, username, password);
                return ObjectMapper.Map<List<PtzConfigDto>>(ptzConfigs);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        [HttpGet]
        public async Task<PtzStatusDto> GetStatusAsync(string host, string username, string password, string profileToken)
        {
            try
            {
                var ptzStatus = await _ptzManager.GetStatusAsync(host, username, password, profileToken);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<PtzStatusDto> AbsoluteMoveAsync(string host, string username, string password, string profileToken,
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            try
            {
                var ptzStatus = await _ptzManager.AbsoluteMoveAsync(host, username, password, profileToken, pan, tilt, zoom,
                    panSpeed, tiltSpeed, zoomSpeed);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<PtzStatusDto> RelativeMoveAsync(string host, string username, string password, string profileToken, 
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            try
            {
                var ptzStatus = await _ptzManager.RelativeMoveAsync(host, username, password, profileToken, pan, tilt, zoom,
                    panSpeed, tiltSpeed, zoomSpeed);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task ContinuousMoveAsync(string host, string username, string password, string profileToken, 
            float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            try
            {
                await _ptzManager.ContinuousMoveAsync(host, username, password, profileToken, panSpeed, tiltSpeed, zoomSpeed);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<PtzStatusDto> StopAsync(string host, string username, string password, string profileToken, bool stopPan, bool stopZoom)
        {
            try
            {
                var ptzStatus = await _ptzManager.StopAsync(host, username, password, profileToken, stopPan, stopZoom);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<List<PtzPresetDto>> GetPresetsAsync(string host, string username, string password, string profileToken)
        {
            try
            {
                var ptzPresets = await _ptzManager.GetPresetsAsync(host, username, password, profileToken);
                return ObjectMapper.Map<List<PtzPresetDto>>(ptzPresets);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<PtzStatusDto> GotoPresetAsync(string host, string username, string password, string profileToken, string presetToken,
            float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            try
            {
                var ptzStatus = await _ptzManager.GotoPresetAsync(host, username, password, profileToken, presetToken,
                    panSpeed, tiltSpeed, zoomSpeed);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<string> SetPresetAsync(string host, string username, string password, string profileToken, string presetToken,
            string presetName)
        {
            try
            {
                var token = await _ptzManager.SetPresetAsync(host, username, password, profileToken, presetToken, presetName);
                return token;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
    }
}
