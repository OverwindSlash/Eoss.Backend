using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.Entities
{
    public class Device : Entity<int>
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
        public List<Profile> Profiles { get; set; }

        [DisplayName("Device Installation Parameters")]
        public InstallationParams InstallationParams { get; set; }

        [DisplayName("Device Group")]
        public Group Group { get; set; }

        public Device()
        {
            var suffix = Guid.NewGuid().ToString().Substring(0, 8);

            DeviceId = $"Cam-{suffix}";
            Name = "Unknown";
            Ipv4Address = "192.168.1.100";
            Model = "Unknown";
            Manufacturer = "Unknown";
            Types = new List<string>();
            Capabilities = new List<string>();
            Profiles = new List<Profile>();
            InstallationParams = new InstallationParams();
        }
    }
}
