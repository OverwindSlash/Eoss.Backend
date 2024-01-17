using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Eoss.Backend.CloudSense.Dto
{
    [AutoMapTo(typeof(Entities.Group))]
    public class GroupSaveDto : EntityDto<int>
    {
        [Required, StringLength(BackendConsts.MaxNameLength, MinimumLength = BackendConsts.MinNameLength)]
        public string Name { get; set; }

        [StringLength(BackendConsts.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
