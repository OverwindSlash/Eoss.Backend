using Abp.AutoMapper;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.CloudSense.Group.Dto
{
    [AutoMapFrom(typeof(Entities.Group))]
    public class GroupUnderDevicesDto : Entity<int>
    {
        [Required, StringLength(BackendConsts.MaxNameLength, MinimumLength = BackendConsts.MinNameLength)]
        public string Name { get; set; }

        [StringLength(BackendConsts.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
