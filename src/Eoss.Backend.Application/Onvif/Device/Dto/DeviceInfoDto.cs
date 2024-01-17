using Abp.AutoMapper;
using Eoss.Backend.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.Onvif.Dto
{
    [AutoMapFrom(typeof(Device))]
    public class DeviceInfoDto
    {
        [DisplayName("Device Id")]
        public string DeviceId { get; set; }

        [DisplayName("Device Name")]
        public string Name { get; set; }

        [DisplayName("Device IpV4 Address")]
        [Required]
        public string Ipv4Address { get; set; }

        [DisplayName("Device Model")]
        public string Model { get; set; }

        [DisplayName("Device Manufacturer")]
        public string Manufacturer { get; set; }
    }
}
