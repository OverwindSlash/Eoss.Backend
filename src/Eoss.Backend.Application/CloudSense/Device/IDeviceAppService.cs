using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Eoss.Backend.CloudSense.Device.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense.Device
{
    public interface IDeviceAppService : 
        IAsyncCrudAppService<DeviceGetDto, int, PagedResultRequestDto, DeviceSaveDto, DeviceSaveDto>
    {
        Task<List<DeviceGetDto>> DiscoveryAndSyncDeviceAsync();

        Task<DeviceGetDto> GetByDeviceIdAsync(string deviceId);

        Task SetDeviceGroup(string deviceId, int groupId);

        Task SetDeviceCredential(DeviceCredentialDto input);

        Task<CapabilitiesDto> GetCapabilities(string deviceId);
    }
}
