using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Eoss.Backend.CloudSense.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense
{
    public interface IGroupAppService : 
        IAsyncCrudAppService<GroupGetDto, int, PagedResultRequestDto, GroupSaveDto, GroupSaveDto>
    {
        Task<List<DeviceUnderGroupDto>> GetAllDevicesInGroupAsync(EntityDto<int> group);

        //根据分组Id字段串查询分组及分组设备信息列表
        Task<List<GroupGetDto>> GetAllDevicesByGroupIdsAsync(EntityDto<string> group);

        //查询分组信息列表
        Task<List<GroupGetDto>> GetGroupInfoListAsync();
    }
}
