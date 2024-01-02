using Abp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.Entities
{
    public class Group : Entity<int>
    {
        [DisplayName("Group Name")]
        [Required, StringLength(BackendConsts.MaxNameLength, MinimumLength = BackendConsts.MinNameLength)]
        public string Name { get; set; }

        [DisplayName("Group Description")]
        [StringLength(BackendConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        [DisplayName("Devices In Group")]
        public List<Device> Devices { get; set; }
    }
}
