using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eoss.Backend.CloudSense.Device.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.CloudSense.Group.Dto
{
    [AutoMapFrom(typeof(Entities.Group))]
    public class GroupGetDto : EntityDto<int>
    {
        [Required, StringLength(BackendConsts.MaxNameLength, MinimumLength = BackendConsts.MinNameLength)]
        public string Name { get; set; }

        [StringLength(BackendConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        public List<DeviceUnderGroupDto> Devices { get; set; }
    }
}
