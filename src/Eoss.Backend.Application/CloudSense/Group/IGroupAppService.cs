using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Eoss.Backend.CloudSense.Group.Dto;

namespace Eoss.Backend.CloudSense.Group
{
    public interface IGroupAppService : IAsyncCrudAppService<
        GroupGetDto, int, PagedResultRequestDto, GroupSaveDto, GroupSaveDto>
    {
    }
}
