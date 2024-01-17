using System;

namespace Eoss.Backend.CloudSense.Dto
{
    public class PtzStatusInDegreeDto
    {
        public float PanPosition { get; set; }
        public float TiltPosition { get; set; }
        public float ZoomPosition { get; set; }
        public string PanTiltStatus { get; set; }
        public string ZoomStatus { get; set; }
        public DateTime UtcDateTime { get; set; }
        public string Error { get; set; }
    }
}
