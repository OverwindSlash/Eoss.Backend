using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.CloudSense.Group.Dto
{
    [AutoMapFrom(typeof(Entities.Group))]
    public class GroupUnderDevicesDto : EntityDto<int>
    {
        [Required, StringLength(BackendConsts.MaxNameLength, MinimumLength = BackendConsts.MinNameLength)]
        public string Name { get; set; }

        [StringLength(BackendConsts.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
