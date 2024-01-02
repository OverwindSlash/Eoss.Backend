using Abp.Domain.Entities;
using System.Collections.Generic;

namespace Eoss.Backend.Entities
{
    public class Capabilities : Entity<int>
    {
        public string AnalyticsXAddress { get; set; } = string.Empty;
        public string DeviceXAddress { get; set; } = string.Empty;
        public string EventXAddress { get; set; } = string.Empty;
        public string ImagingXAddress { get; set; } = string.Empty;
        public string MediaXAddress { get; set; } = string.Empty;
        public string PtzXAddress { get; set; } = string.Empty;

        public List<string> SupportedOnvifVersions { get; set; } = new List<string>();
    }
}
