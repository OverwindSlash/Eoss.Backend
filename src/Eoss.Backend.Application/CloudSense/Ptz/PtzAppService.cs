﻿using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using Eoss.Backend.CloudSense.Dto;
using Eoss.Backend.Domain.CloudSense;
using Eoss.Backend.Domain.Onvif;
using Eoss.Backend.Entities;
using Eoss.Backend.Onvif.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense
{
    public class PtzAppService : ApplicationService, IPtzAppService
    {
        private readonly IRepository<Device> _deviceRepository;
        private readonly IDeviceManager _deviceManager;
        private readonly IOnvifPtzManager _ptzManager;
        
        private static Dictionary<string, Device> _devicesCache = new();
        private static Dictionary<string, Credential> _credentialsCache = new();
        private static Dictionary<string, Device> _deviceWithProfilesCache = new();
        private static Dictionary<string, PtzStatusInDegreeDto> _deviceLastStatus = new();

        public PtzAppService(IRepository<Device> deviceRepository, 
            IDeviceManager deviceManager, IOnvifPtzManager ptzManager)
        {
            _deviceRepository = deviceRepository;
            _deviceManager = deviceManager;
            _ptzManager = ptzManager;

            LocalizationSourceName = BackendConsts.LocalizationSourceName;
        }

        public async Task SetPtzParamsAsync(string deviceId, string profileToken, PtzParamsSaveDto input)
        {
            try
            {
                var device = await GetDeviceWithProfilesByDeviceId(deviceId);
                var profile = GetDeviceProfile(device, profileToken);
                
                var credential = await GetCredentialByDeviceId(deviceId);

                var ptzConfigs = await _ptzManager.GetConfigurationsAsync(
                    device.Ipv4Address, credential.Username, credential.Password);

                if (ptzConfigs.Count >= 1)
                {
                    var ptzConfig = ptzConfigs[0];
                    profile.PtzParams.MaxPanNormal = ptzConfig.PanMaxLimit;
                    profile.PtzParams.MinPanNormal = ptzConfig.PanMinLimit;
                    profile.PtzParams.MaxTiltNormal = ptzConfig.TiltMaxLimit;
                    profile.PtzParams.MinTiltNormal = ptzConfig.TiltMinLimit;
                    profile.PtzParams.MaxZoomNormal = ptzConfig.ZoomMaxLimit;
                    profile.PtzParams.MinZoomNormal = ptzConfig.ZoomMinLimit;
                }
                else
                {
                    profile.PtzParams.MaxPanNormal = 1;
                    profile.PtzParams.MinPanNormal = -1;
                    profile.PtzParams.MaxTiltNormal = 1;
                    profile.PtzParams.MinTiltNormal = -1;
                    profile.PtzParams.MaxZoomNormal = 1;
                    profile.PtzParams.MinZoomNormal = 0;
                }

                profile.PtzParams.HomePanToEast = input.HomePanToEast;
                profile.PtzParams.HomeTiltToHorizon = input.HomeTiltToHorizon;
                profile.PtzParams.MinPanDegree = input.MinPanDegree;
                profile.PtzParams.MaxPanDegree = input.MaxPanDegree;
                profile.PtzParams.MinTiltDegree = input.MinTiltDegree;
                profile.PtzParams.MaxTiltDegree = input.MaxTiltDegree;
                profile.PtzParams.MinZoomLevel = input.MinZoomLevel;
                profile.PtzParams.MaxZoomLevel = input.MaxZoomLevel;
                profile.PtzParams.FocalLength = input.FocalLength;
                profile.PtzParams.SensorWidth = input.SensorWidth;
                profile.PtzParams.SensorHeight = input.SensorHeight;

                await _deviceRepository.UpdateAsync(device);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        private async Task<Device> GetDeviceWithProfilesByDeviceId(string deviceId)
        {
            if (_deviceWithProfilesCache.ContainsKey(deviceId))
            {
                return _deviceWithProfilesCache[deviceId];
            }
            else
            {
                var device = await _deviceRepository.GetAll()
                    .Include(device => device.InstallationParams)
                    .Include(device => device.Profiles)
                    .ThenInclude(profile => profile.PtzParams)
                    .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

                if (device == null)
                {
                    throw new UserFriendlyException(L("DeviceIdNotExist", deviceId));
                }
                
                _deviceWithProfilesCache.Add(deviceId, device);

                return device;
            }
        }

        private Profile GetDeviceProfile(Device device, string profileToken)
        {
            var profile = device.Profiles.FirstOrDefault(profile => profile.Token == profileToken);

            if (profile == null)
            {
                throw new UserFriendlyException(L("ProfileTokenNotExist", device.DeviceId, profileToken));
            }

            return profile;
        }

        [HttpGet]
        public async Task<PtzParamsGetDto> GetPtzParamsAsync(string deviceId, string profileToken)
        {
            try
            {
                var device = await GetDeviceWithProfilesByDeviceId(deviceId);
                var profile = GetDeviceProfile(device, profileToken);

                var ptzParamsGetDto = ObjectMapper.Map<PtzParamsGetDto>(profile.PtzParams);

                return ptzParamsGetDto;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        [HttpGet]
        public async Task<PtzStatusDto> GetStatusAsync(string deviceId, string profileToken)
        {
            try
            {
                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);

                var ptzStatus = await _ptzManager.GetStatusAsync(device.Ipv4Address, credential.Username, credential.Password, profileToken);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        private async Task<Device> GetDeviceByDeviceIdAsync(string deviceId)
        {
            if (_devicesCache.ContainsKey(deviceId))
            {
                return _devicesCache[deviceId];
            }
            else
            {
                var device = await _deviceRepository.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
                if (device == null)
                {
                    throw new UserFriendlyException(L("DeviceIdNotExist", deviceId));
                }
                
                _devicesCache.Add(deviceId, device);

                return device;
            }
        }

        private async Task<Credential> GetCredentialByDeviceId(string deviceId)
        {
            if (_credentialsCache.ContainsKey(deviceId))
            {
                return _credentialsCache[deviceId];
            }
            else
            {
                var credential = await _deviceManager.GetCredentialAsync(deviceId);
                if (credential == null)
                {
                    throw new UserFriendlyException(L("DeviceCredentialNotSet", deviceId));
                }
                
                _credentialsCache.Add(deviceId, credential);

                return credential;
            }
        }

        public async Task<PtzStatusDto> AbsoluteMoveAsync(string deviceId, string profileToken, float pan, float tilt, float zoom, 
            float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1)
        {
            try
            {
                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);

                var ptzStatus = await _ptzManager.AbsoluteMoveAsync(device.Ipv4Address, credential.Username, credential.Password, profileToken,
                    pan, tilt, zoom, panSpeed, tiltSpeed, zoomSpeed);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<PtzStatusDto> RelativeMoveAsync(string deviceId, string profileToken, float pan, float tilt, float zoom, 
            float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1)
        {
            try
            {
                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);

                var ptzStatus = await _ptzManager.RelativeMoveAsync(device.Ipv4Address, credential.Username, credential.Password, profileToken,
                    pan, tilt, zoom, panSpeed, tiltSpeed, zoomSpeed);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<PtzStatusInDegreeDto> GetStatusInDegreeAsync(string deviceId, string profileToken)
        {
            try
            {
                var device = await GetDeviceWithProfilesByDeviceId(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);
                var profile = GetDeviceProfile(device, profileToken);
                var ptzParams = profile.PtzParams;

                var ptzStatus = await _ptzManager.GetStatusAsync(device.Ipv4Address, credential.Username, credential.Password, profileToken);
                var ptzStatusInDegreeDto = ConvertToDegree(ptzParams, ptzStatus);

                ptzStatusInDegreeDto.Fov = ptzParams.CalculateFov(ptzStatusInDegreeDto.ZoomPosition);
                ptzStatusInDegreeDto.Distance = ptzParams.CalculateMaxDistance(ptzStatusInDegreeDto.ZoomPosition);
                ptzStatusInDegreeDto.Direction = device.InstallationParams.CalculateAzimuthToEast(ptzStatusInDegreeDto.PanPosition);
                
                _deviceLastStatus[deviceId] = ptzStatusInDegreeDto;

                return ptzStatusInDegreeDto;
            }
            catch (Exception e)
            {
                //throw new UserFriendlyException(e.Message);
                return _deviceLastStatus[deviceId];
            }
        }

        private static PtzStatusInDegreeDto ConvertToDegree(PtzParams ptzParams, PtzStatus ptzStatus)
        {
            var ptzStatusInDegreeDto = new PtzStatusInDegreeDto()
            {
                PanPosition = ptzParams.PanNormalizationToDegree(ptzStatus.PanPosition),
                TiltPosition = ptzParams.TiltNormalizationToDegree(ptzStatus.TiltPosition),
                ZoomPosition = ptzParams.ZoomNormalizationToLevel(ptzStatus.ZoomPosition),
                PanTiltStatus = ptzStatus.PanTiltStatus,
                ZoomStatus = ptzStatus.ZoomStatus,
                UtcDateTime = ptzStatus.UtcDateTime,
                Error = ptzStatus.Error
            };
            return ptzStatusInDegreeDto;
        }

        public async Task<PtzStatusInDegreeDto> AbsoluteMoveWithDegreeAsync(string deviceId, string profileToken, 
            float panInDegree, float tiltInDegree, float zoomInLevel, float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1)
        {
            try
            {
                var device = await GetDeviceWithProfilesByDeviceId(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);
                var profile = GetDeviceProfile(device, profileToken);
                var ptzParams = profile.PtzParams;

                var panNormalized = ptzParams.PanDegreeToNormalization(panInDegree);
                var tiltNormalized = ptzParams.TiltDegreeToNormalization(tiltInDegree);
                var zoomNormalized = ptzParams.ZoomLevelToNormalization(zoomInLevel);

                var ptzStatus = await _ptzManager.AbsoluteMoveAsync(device.Ipv4Address, credential.Username, credential.Password, profileToken,
                    panNormalized, tiltNormalized, zoomNormalized, panSpeed, tiltSpeed, zoomSpeed);
                var ptzStatusInDegreeDto = ConvertToDegree(ptzParams, ptzStatus);

                return ptzStatusInDegreeDto;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<PtzStatusInDegreeDto> RelativeMoveWithDegreeAsync(string deviceId, string profileToken, 
            float panInDegree, float tiltInDegree, float zoomInLevel, float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1)
        {
            try
            {
                var device = await GetDeviceWithProfilesByDeviceId(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);
                var profile = GetDeviceProfile(device, profileToken);
                var ptzParams = profile.PtzParams;

                var panNormalized = panInDegree / ptzParams.PanDegreePerNormal;
                var tiltNormalized = -tiltInDegree / ptzParams.TiltDegreePerNormal / 2;
                var zoomNormalized = zoomInLevel / ptzParams.ZoomLevelPerNormal;

                var ptzStatus = await _ptzManager.RelativeMoveAsync(device.Ipv4Address, credential.Username, credential.Password, profileToken,
                    panNormalized, tiltNormalized, zoomNormalized, panSpeed, tiltSpeed, zoomSpeed);
                var ptzStatusInDegreeDto = ConvertToDegree(ptzParams, ptzStatus);

                return ptzStatusInDegreeDto;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task ContinuousMoveAsync(string deviceId, string profileToken, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            try
            {
                //var stopwatch = Stopwatch.StartNew();
                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);
                //Trace.WriteLine("***** App Phase1:" + stopwatch.ElapsedMilliseconds.ToString());
                
                //stopwatch.Restart();
                await _ptzManager.ContinuousMoveAsync(device.Ipv4Address, credential.Username, credential.Password, profileToken,
                    panSpeed, tiltSpeed, zoomSpeed);
                //Trace.WriteLine("***** App Phase2:" + stopwatch.ElapsedMilliseconds.ToString());
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<PtzStatusDto> StopAsync(string deviceId, string profileToken, bool stopPan, bool stopZoom)
        {
            try
            {
                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);

                var ptzStatus = await _ptzManager.StopAsync(device.Ipv4Address, credential.Username, credential.Password, profileToken,
                    stopPan, stopZoom);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<List<PtzPresetDto>> GetPresetsAsync(string deviceId, string profileToken)
        {
            try
            {
                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);

                var ptzPresets = await _ptzManager.GetPresetsAsync(
                    device.Ipv4Address, credential.Username, credential.Password, profileToken);
                return ObjectMapper.Map<List<PtzPresetDto>>(ptzPresets);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<PtzStatusDto> GotoPresetAsync(string deviceId, string profileToken, string presetToken, 
            float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1)
        {
            try
            {
                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);

                var ptzStatus = await _ptzManager.GotoPresetAsync(device.Ipv4Address, credential.Username, credential.Password,
                    profileToken, presetToken, panSpeed, tiltSpeed, zoomSpeed);
                return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<string> SetPresetAsync(string deviceId, string profileToken, string presetToken, string presetName)
        {
            try
            {
                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);

                var token = await _ptzManager.SetPresetAsync(device.Ipv4Address, credential.Username, credential.Password,
                    profileToken, presetToken, presetName);

                return token;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
    }
}
