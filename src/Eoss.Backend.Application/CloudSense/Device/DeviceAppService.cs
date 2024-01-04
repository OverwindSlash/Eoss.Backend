using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Eoss.Backend.CloudSense.Device.Dto;
using Eoss.Backend.Domain.CloudSense.Device;
using Eoss.Backend.Domain.Onvif.Capability;
using Eoss.Backend.Domain.Onvif.Discovery;
using Eoss.Backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense.Device
{
    public class DeviceAppService : 
        AsyncCrudAppService<Entities.Device, DeviceGetDto, int, PagedResultRequestDto, DeviceSaveDto, DeviceSaveDto>, 
        IDeviceAppService
    {
        private readonly IOnvifDiscoveryManager _discoveryManager;
        private readonly IRepository<Entities.Device> _deviceRepository;
        private readonly IRepository<Entities.Group> _groupRepository;
        private readonly IDeviceManager _deviceManager;
        private readonly IOnvifDeviceCapabilityManager _capabilityManager;

        public DeviceAppService(IOnvifDiscoveryManager discoveryManager,
            IRepository<Entities.Device> deviceRepository,
            IRepository<Entities.Group> groupRepository,
            IDeviceManager deviceManager,
            IOnvifDeviceCapabilityManager capabilityManager) : base(deviceRepository)
        {
            _discoveryManager = discoveryManager;
            _deviceRepository = deviceRepository;
            _groupRepository = groupRepository;
            _deviceManager = deviceManager;
            _capabilityManager = capabilityManager;
        }

        public async Task<List<DeviceGetDto>> DiscoveryAndSyncDeviceAsync()
        {
            List<DeviceGetDto> deviceDtos = new List<DeviceGetDto>();

            var discoveredDevices = await _discoveryManager.DiscoveryDeviceAsync();
            foreach (var discoveredDevice in discoveredDevices)
            {
                var device = await _deviceRepository.FirstOrDefaultAsync(
                    d => d.Ipv4Address == discoveredDevice.Ipv4Address);

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

        [HttpGet]
        public async Task<DeviceGetDto> GetByDeviceIdAsync(string deviceId)
        {
            CheckGetPermission();

            var device = await CreateDeviceQueryable().FirstOrDefaultAsync(device => device.DeviceId == deviceId);
            return MapToEntityDto(device);
        }

        public async Task SetDeviceGroup(string deviceId, int groupId)
        {
            CheckUpdatePermission();

            var device = await _deviceRepository.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            if (device == null)
            {
                throw new UserFriendlyException(L("DeviceIdNotExist", deviceId));
            }

            var group = await _groupRepository.FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
            {
                throw new UserFriendlyException(L("GroupIdNotExist", groupId));
            }

            device.Group = group;
            await _deviceRepository.UpdateAsync(device);
        }

        public async Task SetDeviceCredential(DeviceCredentialDto input)
        {
            CheckUpdatePermission();

            var device = await _deviceRepository.FirstOrDefaultAsync(d => d.DeviceId == input.DeviceId);
            if (device == null)
            {
                throw new UserFriendlyException(L("DeviceIdNotExist", input.DeviceId));
            }

            var credential = ObjectMapper.Map<Credential>(input);
            credential.Device = device;

            await _deviceManager.SetCredentialAsync(credential);
        }

        public async Task<CapabilitiesDto> GetCapabilities(string deviceId)
        {
            CheckGetPermission();

            var device = await _deviceRepository.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            if (device == null)
            {
                throw new UserFriendlyException(L("DeviceIdNotExist", deviceId));
            }

            var credential = await _deviceManager.GetCredentialAsync(deviceId);
            if (credential == null)
            {
                throw new UserFriendlyException(L("DeviceCredentialNotSet", deviceId));
            }

            var capabilities = await _capabilityManager.GetCapabilitiesAsync(device.Ipv4Address, credential.Username, credential.Password);
            device.SetCapabilities(capabilities);
            await _deviceRepository.UpdateAsync(device);

            var capabilitiesDto = ObjectMapper.Map<CapabilitiesDto>(capabilities);
            return capabilitiesDto;
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
            return _deviceRepository.GetAll().Include(device => device.Group);
        }
    }
}
