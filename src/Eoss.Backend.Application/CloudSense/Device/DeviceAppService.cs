﻿using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using Eoss.Backend.CloudSense.Device.Dto;
using Eoss.Backend.Domain.Onvif.Discovery;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense.Device
{
    public class DeviceAppService : ApplicationService, IDeviceAppService
    {
        private readonly IOnvifDiscoveryManager _discoveryManager;
        private readonly IRepository<Entities.Device> _deviceRepository;
        private readonly IRepository<Entities.Group> _groupRepository;

        public DeviceAppService(IOnvifDiscoveryManager discoveryManager,
            IRepository<Entities.Device> deviceRepository,
            IRepository<Entities.Group> groupRepository)
        {
            _discoveryManager = discoveryManager;
            _deviceRepository = deviceRepository;
            _groupRepository = groupRepository;
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

        public async Task SetDeviceGroup(string deviceId, int groupId)
        {
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
    }
}