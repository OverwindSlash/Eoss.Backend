using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Eoss.Backend.CloudSense.Dto;
using Eoss.Backend.Onvif.Dto;

namespace Eoss.Backend.CloudSense
{
    public interface IDeviceAppService : 
        IAsyncCrudAppService<DeviceGetDto, int, PagedResultRequestDto, DeviceSaveDto, DeviceSaveDto>
    {
        Task<List<DeviceGetDto>> DiscoveryAndSyncDeviceAsync();

        Task<DeviceGetDto> AddDeviceByIpAndCredentialAsync(string host, string username, string password);

        Task<DeviceGetDto> GetByDeviceIdAsync(string deviceId);

        Task SetDeviceGroupAsync(string deviceId, int groupId);

        Task SetDeviceCredentialAsync(DeviceCredentialDto input);
        Task<DeviceCredentialDto> GetDeviceCredentialAsync(string deviceId);

        Task<CapabilitiesGetDto> GetCapabilitiesAsync(string deviceId);

        Task SetInstallationParamsAsync(InstallationParamsDto input);
        Task<InstallationParamsDto> GetInstallationParamsAsync(string deviceId);

        Task<List<ProfileGetDto>> GetProfilesAsync(string deviceId, bool forceRefresh);

        Task<List<VideoSourceDto>> GetVideoSourcesAsync(string deviceId, string profileToken);

        Task<DeviceCoordinateDto> GetDeviceCoordinateAsync(string deviceId);
    }
}
