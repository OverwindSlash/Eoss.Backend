using Abp.AutoMapper;
using Eoss.Backend.Entities;
using System.ComponentModel;

namespace Eoss.Backend.Onvif.Dto
{
    [AutoMapTo(typeof(PtzParams))]
    public class PtzParamsSaveDto
    {
        // Home position
        [DisplayName("Home Pan To East")]
        public double HomePanToEast { get; set; }

        [DisplayName("Home Tilt To Horizon")]
        public double HomeTiltToHorizon { get; set; }

        // Device limit
        [DisplayName("Minimum Pan Degree")]
        public float MinPanDegree { get; set; }

        [DisplayName("Maximum Pan Degree")]
        public float MaxPanDegree { get; set; }

        [DisplayName("Minimum Tilt Degree")]
        public float MinTiltDegree { get; set; }

        [DisplayName("Maximum Tilt Degree")]
        public float MaxTiltDegree { get; set; }

        [DisplayName("Minimum Zoom Level")]
        public float MinZoomLevel { get; set; }

        [DisplayName("Maximum Zoom Level")]
        public float MaxZoomLevel { get; set; }

        [DisplayName("Focal Length")]
        public float FocalLength { get; set; }     // 1倍焦距

        [DisplayName("Sensor Width")]
        public float SensorWidth { get; set; }

        [DisplayName("Sensor Height")]
        public float SensorHeight { get; set; }
    }
}
