﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Eoss.Backend.CloudSense.Dto;
using Eoss.Backend.Domain.CloudSense;
using Eoss.Backend.Domain.Onvif;
using Eoss.Backend.Entities;
using Eoss.Backend.Onvif.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense
{
    public class DeviceAppService : 
        AsyncCrudAppService<Entities.Device, DeviceGetDto, int, PagedResultRequestDto, DeviceSaveDto, DeviceSaveDto>, 
        IDeviceAppService
    {
        private readonly IOnvifDiscoveryManager _discoveryManager;
        private readonly IRepository<Entities.Device> _deviceRepository;
        private readonly IRepository<Entities.Group> _groupRepository;
        private readonly IDeviceManager _deviceManager;
        private readonly IOnvifDeviceManager _onvifDeviceManager;
        private readonly IOnvifMediaManager _mediaManager;

        public DeviceAppService(IOnvifDiscoveryManager discoveryManager,
            IRepository<Entities.Device> deviceRepository,
            IRepository<Entities.Group> groupRepository,
            IDeviceManager deviceManager,
            IOnvifDeviceManager onvifDeviceManager,
            IOnvifMediaManager mediaManager) : base(deviceRepository)
        {
            _discoveryManager = discoveryManager;
            _deviceRepository = deviceRepository;
            _groupRepository = groupRepository;
            _deviceManager = deviceManager;
            _onvifDeviceManager = onvifDeviceManager;
            _mediaManager = mediaManager;

            LocalizationSourceName = BackendConsts.LocalizationSourceName;
        }

        public async Task<List<DeviceGetDto>> DiscoveryAndSyncDeviceAsync()
        {
            try
            {
                List<DeviceGetDto> deviceDtos = new List<DeviceGetDto>();

                var discoveredDevices = await _discoveryManager.DiscoveryDeviceAsync();
                foreach (var discoveredDevice in discoveredDevices)
                {
                    var device = await _deviceRepository.GetAll()
                        .Include(device => device.Profiles)
                        .Include(device => device.Group)
                        .FirstOrDefaultAsync(d => d.Ipv4Address == discoveredDevice.Ipv4Address);

                    if (device == null)
                    {
                        Entities.Device existanceCheckResult;
                        do
                        {
                            device = new Entities.Device();
                            existanceCheckResult = await _deviceRepository.FirstOrDefaultAsync(d => d.DeviceId == device.DeviceId);
                        } while (existanceCheckResult != null);
                    }

                    device.Name = discoveredDevice.Name;
                    device.Ipv4Address = discoveredDevice.Ipv4Address;
                    device.Model = discoveredDevice.Model;
                    device.Manufacturer = discoveredDevice.Manufacturer;
                    device.Types = discoveredDevice.Types;

                    await _deviceRepository.InsertOrUpdateAsync(device);
                    await CurrentUnitOfWork.SaveChangesAsync();

                    deviceDtos.Add(ObjectMapper.Map<DeviceGetDto>(device));
                }

                return deviceDtos;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<DeviceGetDto> AddDeviceByIpAndCredentialAsync(string host, string username, string password)
        {
            try
            {
                CheckCreatePermission();

                var candidateDevice = await _onvifDeviceManager.GetDeviceInfoAsync(host, username, password);
                if (candidateDevice == null)
                {
                    throw new ArgumentException(L("CredentialNotCorrect", host));
                }

                var device = await _deviceRepository.FirstOrDefaultAsync(device => device.Ipv4Address == host);
                if (device == null)
                {
                    device = candidateDevice;
                }
                else
                {
                    device.Name = candidateDevice.Name;
                    device.Model = candidateDevice.Model;
                    device.Manufacturer = candidateDevice.Manufacturer;
                }

                var id = await _deviceRepository.InsertOrUpdateAndGetIdAsync(device);
                device.Id = id;

                var credential = await GetOrCreateCredentialAsync(device.DeviceId);
                credential.Username = username;
                credential.Password = password;
                credential.Device = device;

                await _deviceManager.SetCredentialAsync(credential);

                return ObjectMapper.Map<DeviceGetDto>(device);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        [HttpGet]
        public async Task<DeviceGetDto> GetByDeviceIdAsync(string deviceId)
        {
            try
            {
                CheckGetPermission();

                var device = await CreateDeviceQueryable().FirstOrDefaultAsync(device => device.DeviceId == deviceId);
                return MapToEntityDto(device);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public override async Task<DeviceGetDto> CreateAsync(DeviceSaveDto input)
        {
            try
            {
                CheckCreatePermission();

                var entity = MapToEntity(input);
                entity.Group = _groupRepository.FirstOrDefault(group => group.Id == input.GroupId);

                await Repository.InsertAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();

                return MapToEntityDto(entity);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task SetDeviceGroupAsync(string deviceId, int groupId)
        {
            try
            {
                CheckUpdatePermission();

                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var group = await GetGroupByGroupId(groupId);

                device.Group = group;
                await _deviceRepository.UpdateAsync(device);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task SetDeviceCredentialAsync(DeviceCredentialDto input)
        {
            try
            {
                CheckUpdatePermission();

                var device = await GetDeviceByDeviceIdAsync(input.DeviceId);

                var credential = await GetOrCreateCredentialAsync(input.DeviceId);
                credential.Username = input.Username;
                credential.Password = input.Password;
                credential.Device = device;

                await _deviceManager.SetCredentialAsync(credential);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        private async Task<Credential> GetOrCreateCredentialAsync(string deviceId)
        {
            var credential = await _deviceManager.GetCredentialAsync(deviceId);
            if (credential == null)
            {
                credential = new Credential();
                credential.Device = _deviceRepository.FirstOrDefault(device => device.DeviceId == deviceId);
            }
            
            return credential;
        }

        public async Task<DeviceCredentialDto> GetDeviceCredentialAsync(string deviceId)
        {
            try
            {
                CheckGetPermission();

                var credential = await _deviceManager.GetCredentialAsync(deviceId);
                if (credential == null)
                {
                    throw new UserFriendlyException(L("CredentialNotSet", deviceId));
                }

                var credentialDto = ObjectMapper.Map<DeviceCredentialDto>(credential);
                credentialDto.DeviceId = credential.Device.DeviceId;
                return credentialDto;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        [HttpGet]
        public async Task<CapabilitiesGetDto> GetCapabilitiesAsync(string deviceId)
        {
            try
            {
                CheckGetPermission();

                var device = await GetDeviceByDeviceIdAsync(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);
                var capabilities = await _onvifDeviceManager.GetCapabilitiesAsync(device.Ipv4Address, credential.Username, credential.Password);

                device.SetCapabilities(capabilities);
                await _deviceRepository.UpdateAsync(device);

                return ObjectMapper.Map<CapabilitiesGetDto>(capabilities);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task SetInstallationParamsAsync(InstallationParamsDto input)
        {
            try
            {
                CheckUpdatePermission();

                var installationParams = ObjectMapper.Map<InstallationParams>(input);

                var device = await GetDeviceWithInstallationParamsByDeviceIdAsync(input.DeviceId);
                device.InstallationParams.CopyFrom(installationParams);

                await _deviceRepository.UpdateAsync(device);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        [HttpGet]
        public async Task<InstallationParamsDto> GetInstallationParamsAsync(string deviceId)
        {
            try
            {
                CheckGetPermission();

                var device = await _deviceRepository.GetAll()
                    .Include(device => device.InstallationParams)
                    .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

                if (device == null)
                {
                    throw new UserFriendlyException(L("DeviceIdNotExist", deviceId));
                }

                var installationParamsDto = ObjectMapper.Map<InstallationParamsDto>(device.InstallationParams);
                installationParamsDto.DeviceId = deviceId;

                return installationParamsDto;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        [HttpGet]
        public async Task<List<ProfileGetDto>> GetProfilesAsync(string deviceId, bool forceRefresh = false)
        {
            try
            {
                CheckGetPermission();

                var device = await GetDeviceWithProfilesByDeviceId(deviceId);

                if (forceRefresh || device.Profiles.Count == 0)
                {
                    var credential = await GetCredentialByDeviceId(deviceId);

                    var profiles = await _mediaManager.GetProfilesAsync(device.Ipv4Address, credential.Username, credential.Password);
                    device.Profiles = profiles;

                    await _deviceRepository.UpdateAsync(device);
                }

                var profileGetDtos = ObjectMapper.Map<List<ProfileGetDto>>(device.Profiles);
                return profileGetDtos;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<List<VideoSourceDto>> GetVideoSourcesAsync(string deviceId, string profileToken)
        {
            try
            {
                CheckGetPermission();

                var device = await GetDeviceWithProfilesByDeviceId(deviceId);
                var credential = await GetCredentialByDeviceId(deviceId);

                List<VideoSourceDto> videoSourceDtos = new();

                var profiles = await _mediaManager.GetProfilesAsync(device.Ipv4Address, credential.Username, credential.Password);
                var profile = profiles.SingleOrDefault(p => p.Token == profileToken);
                if (profile != null)
                {
                    var videoSourceDto = new VideoSourceDto()
                    {
                        Token = profile.VideoSourceToken,
                        StreamUri = profile.StreamUri,
                        Username = credential.Username,
                        Password = credential.Password,
                        VideoWidth = profile.VideoWidth,
                        VideoHeight = profile.VideoHeight,
                        Framerate = profile.FrameRate
                    };

                    videoSourceDtos.Add(videoSourceDto);
                }

                return videoSourceDtos;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public async Task<DeviceCoordinateDto> GetDeviceCoordinateAsync(string deviceId)
        {
            try
            {
                CheckGetPermission();

                var device = await _deviceRepository.GetAll()
                    .Include(device => device.InstallationParams)
                    .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

                if (device == null)
                {
                    throw new UserFriendlyException(L("DeviceIdNotExist", deviceId));
                }

                var deviceCoordinateDto = ObjectMapper.Map<DeviceCoordinateDto>(device.InstallationParams);

                return deviceCoordinateDto;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        protected override async Task<Entities.Device> GetEntityByIdAsync(int id)
        {
            return await CreateDeviceQueryable().FirstOrDefaultAsync(device => device.Id == id);
        }

        protected override IQueryable<Entities.Device> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return CreateDeviceQueryable();
        }

        private IIncludableQueryable<Entities.Device, Entities.Group> CreateDeviceQueryable()
        {
            return _deviceRepository.GetAll()
                .Include(device => device.Profiles)
                .ThenInclude(profile => profile.PtzParams)
                .Include(device => device.Group);
        }

        private async Task<Entities.Device> GetDeviceByDeviceIdAsync(string deviceId)
        {
            var device = await _deviceRepository.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            if (device == null)
            {
                throw new UserFriendlyException(L("DeviceIdNotExist", deviceId));
            }

            return device;
        }

        private async Task<Entities.Group> GetGroupByGroupId(int groupId)
        {
            var group = await _groupRepository.FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
            {
                throw new UserFriendlyException(L("GroupIdNotExist", groupId));
            }

            return group;
        }

        private async Task<Credential> GetCredentialByDeviceId(string deviceId)
        {
            var credential = await _deviceManager.GetCredentialAsync(deviceId);
            if (credential == null)
            {
                throw new UserFriendlyException(L("DeviceCredentialNotSet", deviceId));
            }

            return credential;
        }

        private async Task<Entities.Device> GetDeviceWithProfilesByDeviceId(string deviceId)
        {
            var device = await _deviceRepository.GetAll()
                .Include(device => device.Profiles)
                .ThenInclude(profile => profile.PtzParams)
                .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

            if (device == null)
            {
                throw new UserFriendlyException(L("DeviceIdNotExist", deviceId));
            }

            return device;
        }

        private async Task<Entities.Device> GetDeviceWithInstallationParamsByDeviceIdAsync(string deviceId)
        {
            var device = await _deviceRepository.GetAll()
                    .Include(device => device.InstallationParams)
                    .FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            if (device == null)
            {
                throw new UserFriendlyException(L("DeviceIdNotExist", deviceId));
            }

            return device;
        }
    }
}
