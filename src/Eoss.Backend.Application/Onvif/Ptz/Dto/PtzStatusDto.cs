using System;
using Abp.AutoMapper;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Onvif.Dto
{
    [AutoMapFrom(typeof(PtzStatus))]
    public class PtzStatusDto
    {
        public float PanPosition { get; set; }
        public float TiltPosition { get; set; }
        public float ZoomPosition { get; set; }

        public string PanTiltStatus { get; set; }
        public string ZoomStatus { get; set; }

        public DateTime UtcDateTime { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
