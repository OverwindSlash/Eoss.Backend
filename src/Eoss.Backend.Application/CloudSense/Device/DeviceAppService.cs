using Abp.Application.Services;
using Eoss.Backend.CloudSense.Device.Dto;
using Eoss.Backend.Domain.Onvif.Discovery;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace Eoss.Backend.CloudSense.Device
{
    public class DeviceAppService : ApplicationService, IDeviceAppService
    {
        private readonly IOnvifDiscoveryManager _discoveryManager;
        private readonly IRepository<Entities.Device> _deviceRepository;

        public DeviceAppService(IOnvifDiscoveryManager discoveryManager,
            IRepository<Entities.Device> deviceRepository)
        {
            _discoveryManager = discoveryManager;
            _deviceRepository = deviceRepository;
        }

        public async Task<List<DeviceGetDto>> DiscoveryAndSyncDeviceAsync()
        {
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

                device = new Entities.Device()
                {
                    Name = discoveredDevice.Name,
                    Ipv4Address = discoveredDevice.Ipv4Address,
                    Model = discoveredDevice.Model,
                    Manufacturer = discoveredDevice.Manufacturer,
                    Types = discoveredDevice.Types
                };

                await _deviceRepository.InsertOrUpdateAsync(device);
            }

            return ObjectMapper.Map<List<DeviceGetDto>>(discoveredDevices);
        }
    }
}
