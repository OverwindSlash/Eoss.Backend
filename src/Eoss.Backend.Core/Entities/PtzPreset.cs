namespace Eoss.Backend.Entities
{
    public class PtzPreset
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public float PanPosition { get; set; }
        public float TiltPosition { get; set; }
        public float ZoomPosition { get; set; }
        public string PanTiltSpace { get; set; }
        public string ZoomSpace { get; set; }
    }
}
