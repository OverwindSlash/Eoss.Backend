using Abp.AutoMapper;
using Eoss.Backend.Entities;
using System.ComponentModel;

namespace Eoss.Backend.Onvif.Dto
{
    [AutoMapFrom(typeof(PtzParams))]
    public class PtzParamsGetDto
    {
        // Home position
        [DisplayName("Home Pan To North")]
        public double HomePanToNorth { get; set; }

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

        public float PanDegreeRange => MaxPanDegree - MinPanDegree;
        public float TiltDegreeRange => MaxTiltDegree - MinTiltDegree;
        public float ZoomLevelRange => MaxZoomLevel - MinZoomLevel;

        // Onvif limit
        [DisplayName("Normalized Minimum Pan Limit")]
        public float MinPanNormal { get; set; }

        [DisplayName("Normalized Maximum Pan Limit")]
        public float MaxPanNormal { get; set; }

        [DisplayName("Normalized Minimum Tilt Limit")]
        public float MinTiltNormal { get; set; }

        [DisplayName("Normalized Maximum Tilt Limit")]
        public float MaxTiltNormal { get; set; }

        [DisplayName("Normalized Minimum Zoom Limit")]
        public float MinZoomNormal { get; set; }

        [DisplayName("Normalized Maximum Zoom Limit")]
        public float MaxZoomNormal { get; set; }

        public float PanNormalRange => MaxPanNormal - MinPanNormal;
        public float TiltNormalRange => MaxTiltNormal - MinTiltNormal;
        public float ZoomNormalRange => MaxZoomNormal - MinZoomNormal;

        // For calculation
        public float PanDegreePerNormal => PanDegreeRange / PanNormalRange;
        public float TiltDegreePerNormal => TiltDegreeRange / TiltNormalRange;
        public float ZoomLevelPerNormal => ZoomLevelRange / ZoomNormalRange;
    }
}
