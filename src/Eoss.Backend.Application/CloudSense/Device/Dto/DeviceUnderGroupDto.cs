using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eoss.Backend.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.CloudSense.Dto
{
    [AutoMapFrom(typeof(Device))]
    public class DeviceUnderGroupDto : EntityDto<int>
    {
        [DisplayName("Device Id")]
        [Required, StringLength(BackendConsts.MaxStringIdLength, MinimumLength = BackendConsts.MinStringIdLength)]
        public string DeviceId { get; set; }

        [DisplayName("Device Name")]
        [Required, StringLength(BackendConsts.MaxNameLength, MinimumLength = BackendConsts.MinNameLength)]
        public string Name { get; set; }

        [DisplayName("Device IpV4 Address")]
        [Required]
        public string Ipv4Address { get; set; }

        [DisplayName("Device Model")]
        [StringLength(BackendConsts.MaxNameLength)]
        public string Model { get; set; }

        [DisplayName("Device Manufacturer")]
        [StringLength(BackendConsts.MaxNameLength)]
        public string Manufacturer { get; set; }

        [DisplayName("Device Types")]
        public List<string> Types { get; set; }

        [DisplayName("Device Capabilities")]
        public List<string> Capabilities { get; set; }

        [DisplayName("Main Profile")]
        public List<ProfileGetDto> Profiles { get; set; }
    }
}
