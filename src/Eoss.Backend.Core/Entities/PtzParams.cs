using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.Entities
{
    public class PtzParams : Entity<int>
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


        // Methods
        public float PanDegreeToNormalization(float panDegree)
        {
            float panOffset = panDegree - MinPanDegree;

            var panNormalized = panOffset / PanDegreePerNormal;

            return panNormalized + MinPanNormal;
        }

        public float PanNormalizationToDegree(float panNormalized)
        {
            float panOffset = (panNormalized - MinPanNormal) * PanDegreePerNormal;

            return panOffset + MinPanDegree;
        }

        public float TiltDegreeToNormalization(float tiltDegree)
        {
            float tiltOffset = tiltDegree - MinTiltDegree;

            var tiltNormalized = tiltOffset / TiltDegreePerNormal;

            return -(tiltNormalized + MinTiltNormal);
        }

        public float TiltNormalizationToDegree(float tiltNormalized)
        {
            float tiltOffset = -(tiltNormalized - MinTiltNormal) * TiltDegreePerNormal;

            return TiltDegreeRange + (tiltOffset + MinTiltDegree);
        }

        public float ZoomLevelToNormalization(int zoomLevel)
        {
            float zoomOffset = zoomLevel - MinZoomLevel;

            var zoomNormalized = zoomOffset / ZoomLevelRange;

            return zoomNormalized - MinZoomNormal;
        }

        public float ZoomNormalizationToLevel(float zoomNormalized)
        {
            float zoomOffset = (zoomNormalized + MinZoomNormal) * ZoomLevelPerNormal;

            return zoomOffset + MinZoomLevel;
        }
    }
}
