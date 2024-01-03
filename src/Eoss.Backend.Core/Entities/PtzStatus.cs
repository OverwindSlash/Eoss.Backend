using System;

namespace Eoss.Backend.Entities
{
    public class PtzStatus
    {
        public float PanPosition { get; set; }
        public float TiltPosition { get; set; }
        public float ZoomPosition { get; set; }

        public string PanTiltStatus { get; set; }
        public string ZoomStatus { get; set; }

        public string PanTiltSpace { get; set; }
        public string ZoomSpace { get; set; }

        public DateTime UtcDateTime { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
