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

        public void CopyFrom(InstallationParams othrer)
        {
            Longitude = othrer.Longitude;
            Latitude = othrer.Latitude;
            Altitude = othrer.Altitude;
            Roll = othrer.Roll;
            Pitch = othrer.Pitch;
            Yaw = othrer.Yaw;
        }
    }
}
