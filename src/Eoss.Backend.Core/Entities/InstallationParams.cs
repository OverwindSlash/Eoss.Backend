using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.Entities
{
    public class InstallationParams : Entity<int>
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

        [DisplayName("Roll")]
        [Required, Range(-180.0, 180.0)]
        public double Roll { get; set; }

        [DisplayName("Pitch")]
        [Required, Range(-180.0, 180.0)]
        public double Pitch { get; set; }

        [DisplayName("Yaw")]
        [Required, Range(-180.0, 180.0)]
        public double Yaw { get; set; }

        // 相机0位的状态: 逆时针为正
        [DisplayName("Angle To X Axis")]
        [Required, Range(-180.0, 180.0), DefaultValue(90.0)]
        public double AngleToXAxis { get; set; }

        [DisplayName("Angle To Y Axis")]
        [Required, Range(-180.0, 180.0), DefaultValue(90.0)]
        public double AngleToYAxis { get; set; }

        [DisplayName("Angle To Z Axis")]
        [Required, Range(-180.0, 180.0), DefaultValue(0.0)]
        public double AngleToZAxis { get; set; }
    }
}
