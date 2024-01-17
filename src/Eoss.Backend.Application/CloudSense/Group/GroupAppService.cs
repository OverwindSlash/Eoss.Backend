using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Eoss.Backend.CloudSense.Dto;
using Eoss.Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense
{
    public class GroupAppService : 
        AsyncCrudAppService<Group, GroupGetDto, int, PagedResultRequestDto, GroupSaveDto, GroupSaveDto>,
        IGroupAppService
    {
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<Device> _deviceRepository;

        public GroupAppService(IRepository<Group> groupRepository, 
            IRepository<Device> deviceRepository) : base(groupRepository)
        {
            _groupRepository = groupRepository;
            _deviceRepository = deviceRepository;

            LocalizationSourceName = BackendConsts.LocalizationSourceName;
        }

        public async Task<List<DeviceUnderGroupDto>> GetAllDevicesInGroupAsync(EntityDto<int> group)
        {
            try
            {
                List<DeviceUnderGroupDto> result = new List<DeviceUnderGroupDto>();

                var deviceGroup = await GetAsync(group);
                if (deviceGroup != null)
                {
                    var devices = await CreateDeviceDetailQueryable()
                        .Where(device => device.Group.Id == group.Id).ToListAsync();

                    result.AddRange(devices.Select(device => ObjectMapper.Map<DeviceUnderGroupDto>(device)));
                }

                return result;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        private IIncludableQueryable<Device, List<Profile>> CreateDeviceDetailQueryable()
        {
            return _deviceRepository.GetAll()
                .Include(device => device.Group)
                .Include(device => device.Profiles);
        }

        protected override async Task<Group> GetEntityByIdAsync(int id)
        {
            return await CreateGroupWithDeviceDetailQueryable()
                .FirstOrDefaultAsync(group => group.Id == id);
        }

        private IIncludableQueryable<Group, PtzParams> CreateGroupWithDeviceDetailQueryable()
        {
            return _groupRepository.GetAll()
                .Include(group => group.Devices)
                .ThenInclude(device => device.Profiles)
                .ThenInclude(profile => profile.PtzParams);
        }

        protected override IQueryable<Group> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return CreateGroupWithDeviceDetailQueryable();
        }
    }
}
