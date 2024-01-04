﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Eoss.Backend.CloudSense.Device.Dto;
using Eoss.Backend.CloudSense.Group.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<DeviceUnderGroupDto>> GetAllDevicesInGroupAsync(EntityDto<int> group)
        {
            List<DeviceUnderGroupDto> result = new List<DeviceUnderGroupDto>();

            var deviceGroup = await GetAsync(group);
            if (deviceGroup != null)
            {
                var devices = await _deviceRepository.GetAll()
                    .Include(device => device.Group)
                    .Where(device => device.Group.Id == group.Id).ToListAsync();

                result.AddRange(devices.Select(device => ObjectMapper.Map<DeviceUnderGroupDto>(device)));
            }

            return result;
        }

        protected override Task<Entities.Group> GetEntityByIdAsync(int id)
        {
            return CreateGroupQueryable().FirstOrDefaultAsync(group => group.Id == id);
        }

        protected override IQueryable<Entities.Group> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return CreateGroupQueryable();
        }

        private IIncludableQueryable<Entities.Group, List<Entities.Device>> CreateGroupQueryable()
        {
            return _groupRepository.GetAll().Include(group => group.Devices);
        }
    }
}