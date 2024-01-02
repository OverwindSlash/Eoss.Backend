using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eoss.Backend.Domain.Onvif.Capability
{
    public class AnalyticsCapability
    {
        public string XAddress { get; set; } = string.Empty;
    }

    public class DeviceCapability
    {
        public string XAddress { get; set; } = string.Empty;

        public List<string> SupportedOnvifVersions { get; set; } = new List<string>();
    }

    public class EventCapability
    {
        public string XAddress { get; set; } = string.Empty;
    }

    public class ImagingCapability
    {
        public string XAddress { get; set; } = string.Empty;
    }

    public class MediaCapability
    {
        public string XAddress { get; set; } = string.Empty;
    }

    public class PtzCapability
    {
        public string XAddress { get; set; } = string.Empty;
    }

    public class DeviceCapabilities
    {
        public AnalyticsCapability? AnalyticsCapability { get; set; }
        public DeviceCapability? DeviceCapability { get; set; }
        public EventCapability? EventCapability { get; set; }
        public ImagingCapability? ImagingCapability { get; set; }
        public MediaCapability? MediaCapability { get; set; }
        public PtzCapability? PtzCapability { get; set; }
    }
}
