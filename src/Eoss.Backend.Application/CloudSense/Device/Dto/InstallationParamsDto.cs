using Abp.AutoMapper;
using Eoss.Backend.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.CloudSense.Dto
{
    [AutoMap(typeof(InstallationParams))]
    public class InstallationParamsDto
    {
        [DisplayName("Longitude")]
        [Required, Range(-180.0, 180.0)]
        public double Longitude { get; set; }

        [DisplayName("Latitude")]
        [Required, Range(-90.0, 90.0)]
        public double Latitude { get; set; }

        [DisplayName("Altitude")]
        [Required, Range(0, Double.MaxValue)]
        public double Altitude { get; set; }

        [DisplayName("Home Pan To East")]
        [Required, Range(-180.0, 180.0)]
        public float HomePanToEast { get; set; }

        [DisplayName("Home Tilt To Horizon")]
        [Required, Range(-180.0, 180.0)]
        public float HomeTiltToHorizon { get; set; }

        [DisplayName("Roll")]
        [Required, Range(-180.0, 180.0)]
        public double Roll { get; set; }

        [DisplayName("Pitch")]
        [Required, Range(-180.0, 180.0)]
        public double Pitch { get; set; }

        [DisplayName("Yaw")]
        [Required, Range(-180.0, 180.0)]
        public double Yaw { get; set; }

        [DisplayName("Device Id")]
        [Required]
        public string DeviceId { get; set; }
    }
}
