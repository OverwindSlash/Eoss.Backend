using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Eoss.Backend.CloudSense.Device.Dto;
using Eoss.Backend.CloudSense.Group.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense.Group
{
    public interface IGroupAppService : IAsyncCrudAppService<
        GroupGetDto, int, PagedResultRequestDto, GroupSaveDto, GroupSaveDto>
    {
        Task<List<DeviceUnderGroupDto>> GetAllDevicesInGroupAsync(EntityDto<int> group);
    }
}
