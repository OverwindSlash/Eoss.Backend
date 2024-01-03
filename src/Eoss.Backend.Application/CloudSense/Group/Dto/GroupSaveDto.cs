using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.CloudSense.Group.Dto
{
    [AutoMapTo(typeof(Entities.Group))]
    public class GroupSaveDto : Entity<int>
    {
        [AutoMapTo(typeof(Entities.Group))]
        public class DeviceGroupSaveDto : EntityDto<int>
        {
            [Required, StringLength(BackendConsts.MaxNameLength, MinimumLength = BackendConsts.MinNameLength)]
            public string Name { get; set; }

            [StringLength(BackendConsts.MaxDescriptionLength)]
            public string Description { get; set; }
        }
    }
}
