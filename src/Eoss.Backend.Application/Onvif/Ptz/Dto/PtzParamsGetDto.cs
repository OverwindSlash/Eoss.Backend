using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Onvif.Dto
{
    [AutoMapFrom(typeof(PtzParams))]
    public class PtzParamsGetDto
    {
        // Home position
        [DisplayName("Home Pan To North")]
        [Required, Range(-180.0, 180.0)]
        public double HomePanToNorth { get; set; }

        [DisplayName("Home Tilt To Horizon")]
        [Required, Range(-90.0, 90.0)]
        public double HomeTiltToHorizon { get; set; }

        // Device limit
        [DisplayName("Minimum Pan Degree")]
        [Range(-180, 0)]
        public float MinPanDegree { get; set; }

        [DisplayName("Maximum Pan Degree")]
        [Range(0, 180)]
        public float MaxPanDegree { get; set; }

        [DisplayName("Minimum Tilt Degree")]
        [Range(-90, 0)]
        public float MinTiltDegree { get; set; }

        [DisplayName("Maximum Tilt Degree")]
        [Range(0, 270)]
        public float MaxTiltDegree { get; set; }

        [DisplayName("Minimum Zoom Level")]
        [Range(1, float.MaxValue), DefaultValue(1)]
        public float MinZoomLevel { get; set; }

        [DisplayName("Maximum Zoom Level")]
        [Range(1, float.MaxValue), DefaultValue(1)]
        public float MaxZoomLevel { get; set; }

        [DisplayName("Focal Length")]
        [Range(0.0, float.MaxValue)]
        public float FocalLength { get; set; }     // 1倍焦距

        [DisplayName("Sensor Width")]
        [Range(0.0, float.MaxValue)]
        public float SensorWidth { get; set; }

        [DisplayName("Sensor Height")]
        [Range(0.0, float.MaxValue)]
        public float SensorHeight { get; set; }

        public float PanDegreeRange => MaxPanDegree - MinPanDegree;
        public float TiltDegreeRange => MaxTiltDegree - MinTiltDegree;
        public float ZoomLevelRange => MaxZoomLevel - MinZoomLevel;

        // Onvif limit
        [DisplayName("Normalized Minimum Pan Limit")]
        [Range(-1, 0), DefaultValue(-1)]
        public float MinPanNormal { get; set; }

        [DisplayName("Normalized Maximum Pan Limit")]
        [Range(0, 1), DefaultValue(1)]
        public float MaxPanNormal { get; set; }

        [DisplayName("Normalized Minimum Tilt Limit")]
        [Range(-1, 0), DefaultValue(-1)]
        public float MinTiltNormal { get; set; }

        [DisplayName("Normalized Maximum Tilt Limit")]
        [Range(0, 1), DefaultValue(1)]
        public float MaxTiltNormal { get; set; }

        [DisplayName("Normalized Minimum Zoom Limit")]
        [Range(0, 1), DefaultValue(0)]
        public float MinZoomNormal { get; set; }

        [DisplayName("Normalized Maximum Zoom Limit")]
        [Range(0, 1), DefaultValue(1)]
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
