using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eoss.Backend.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eoss.Backend.CloudSense.Dto
{
    [AutoMapFrom(typeof(Device))]
    public class DeviceGetDto : EntityDto<int>
    {
        [DisplayName("Device Id")]
        public string DeviceId { get; set; }

        [DisplayName("Device Name")]
        public string Name { get; set; }

        [DisplayName("Device IpV4 Address")]
        public string Ipv4Address { get; set; }

        [DisplayName("Device Model")]
        public string Model { get; set; }

        [DisplayName("Device Manufacturer")]
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
