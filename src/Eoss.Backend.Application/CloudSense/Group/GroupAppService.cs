using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Eoss.Backend.CloudSense.Group.Dto;

namespace Eoss.Backend.CloudSense.Group
{
    public class GroupAppService : AsyncCrudAppService<
        Entities.Group, GroupGetDto, int, PagedResultRequestDto, GroupSaveDto, GroupSaveDto>, IGroupAppService
    {
        private readonly IRepository<Entities.Group> _groupRepository;
        private readonly IRepository<Entities.Device> _deviceRepository;

        public GroupAppService(IRepository<Entities.Group> groupRepository, 
            IRepository<Entities.Device> deviceRepository) : base(groupRepository)
        {
            _groupRepository = groupRepository;
            _deviceRepository = deviceRepository;
        }
    }
}
