using Abp.Domain.Entities;

namespace Eoss.Backend.Entities
{
    public class PtzConfig : Entity<int>
    {
        public string NodeToken { get; set; }
        public string DefaultAbsolutePantTiltPositionSpace { get; set; }
        public string DefaultAbsoluteZoomPositionSpace { get; set; }
        public string DefaultRelativePanTiltTranslationSpace { get; set; }
        public string DefaultRelativeZoomTranslationSpace { get; set; }
        public string DefaultContinuousPanTiltVelocitySpace { get; set; }
        public string DefaultContinuousZoomVelocitySpace { get; set; }

        public float DefaultPanSpeed { get; set; }
        public float DefaultTiltSpeed { get; set; }
        public string DefaultPanTiltSpace { get; set; }

        public float DefaultZoomSpeed { get; set; }
        public string DefaultZoomSpace { get; set; }

        public string DefaultPTZTimeout { get; set; }
        public float PanMinLimit { get; set; }
        public float PanMaxLimit { get; set; }
        public float TiltMinLimit { get; set; }
        public float TiltMaxLimit { get; set; }
        public float ZoomMinLimit { get; set; }
        public float ZoomMaxLimit { get; set; }

        public int MoveRamp { get; set; }
        public bool MoveRampSpecified { get; set; }
        public int PresetRamp { get; set; }
        public bool PresetRampSpecified { get; set; }
        public int PresetTourRamp { get; set; }
        public bool PresetTourRampSpecified { get; set; }
    }
}
