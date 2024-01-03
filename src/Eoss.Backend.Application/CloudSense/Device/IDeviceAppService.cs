using Abp.Application.Services;
using Eoss.Backend.CloudSense.Device.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense.Device
{
    public interface IDeviceAppService : IApplicationService
    {
        Task<List<DeviceGetDto>> DiscoveryAndSyncDeviceAsync();

        Task SetDeviceGroup(string deviceId, int groupId);
    }
}
