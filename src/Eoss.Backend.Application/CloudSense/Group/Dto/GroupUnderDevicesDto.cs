using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Eoss.Backend.CloudSense.Dto
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
