using Abp.AutoMapper;
using Eoss.Backend.Domain.Onvif.Capability;
using System.Collections.Generic;

namespace Eoss.Backend.Onvif.Capability.Dto
{
    [AutoMapFrom(typeof(AnalyticsCapability))]
    public class AnalyticsCapabilityDto
    {
        public string XAddress { get; set; } = string.Empty;
    }

    [AutoMapFrom(typeof(DeviceCapability))]
    public class DeviceCapabilityDto
    {
        public string XAddress { get; set; } = string.Empty;

        public List<string> SupportedOnvifVersions { get; set; } = new List<string>();
    }

    [AutoMapFrom(typeof(EventCapability))]
    public class EventCapabilityDto
    {
        public string XAddress { get; set; } = string.Empty;
    }

    [AutoMapFrom(typeof(ImagingCapability))]
    public class ImagingCapabilityDto
    {
        public string XAddress { get; set; } = string.Empty;
    }

    [AutoMapFrom(typeof(MediaCapability))]
    public class MediaCapabilityDto
    {
        public string XAddress { get; set; } = string.Empty;
    }

    [AutoMapFrom(typeof(PtzCapability))]
    public class PtzCapabilityDto
    {
        public string XAddress { get; set; } = string.Empty;
    }

    [AutoMapFrom(typeof(DeviceCapabilities))]
    public class DeviceCapabilitiesDto
    {
        public AnalyticsCapabilityDto AnalyticsCapability { get; set; }
        public DeviceCapabilityDto DeviceCapability { get; set; }
        public EventCapabilityDto EventCapability { get; set; }
        public ImagingCapabilityDto ImagingCapability { get; set; }
        public MediaCapabilityDto MediaCapability { get; set; }
        public PtzCapabilityDto PtzCapability { get; set; }
    }
}
