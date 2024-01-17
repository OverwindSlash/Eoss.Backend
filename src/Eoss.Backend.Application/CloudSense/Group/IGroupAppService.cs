using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Eoss.Backend.CloudSense.Dto;

namespace Eoss.Backend.CloudSense
{
    public interface IGroupAppService : 
        IAsyncCrudAppService<GroupGetDto, int, PagedResultRequestDto, GroupSaveDto, GroupSaveDto>
    {
        Task<List<DeviceUnderGroupDto>> GetAllDevicesInGroupAsync(EntityDto<int> group);
    }
}
