using Abp.AutoMapper;
using Abp.Domain.Entities;
using Eoss.Backend.CloudSense.Group.Dto;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.CloudSense.Device.Dto
{
    [AutoMapFrom(typeof(Entities.Device))]
    public class DeviceGetDto : Entity<int>
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

        [DisplayName("Device Profiles")]
        public List<ProfileGetDto> Profiles { get; set; }

        [DisplayName("Device Group")]
        public GroupUnderDevicesDto Group { get; set; }
    }
}
