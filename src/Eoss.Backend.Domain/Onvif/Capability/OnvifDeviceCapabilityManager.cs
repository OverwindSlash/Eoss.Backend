using Abp.Domain.Services;
using Mictlanix.DotNet.Onvif;
using Mictlanix.DotNet.Onvif.Common;

namespace Eoss.Backend.Domain.Onvif.Capability
{
    public class OnvifDeviceCapabilityManager : DomainService, IOnvifDeviceCapabilityManager
    {
        public async Task<DeviceCapabilities> GetCapabilitiesAsync(string host, string username, string password)
        {
            return await DoGetCapabilities(host, username, password);
        }

        private static async Task<DeviceCapabilities> DoGetCapabilities(string host, string username, string password)
        {
            var device = await OnvifClientFactory.CreateDeviceClientAsync(host, username, password);
            var response = await device.GetCapabilitiesAsync(new CapabilityCategory[] { CapabilityCategory.All });

            var capabilities = new DeviceCapabilities();

            var analyticsCapability = response.Capabilities.Analytics;
            if (analyticsCapability != null)
            {
                capabilities.AnalyticsCapability = new AnalyticsCapability()
                {
                    XAddress = analyticsCapability.XAddr
                };
            }

            var deviceCapability = response.Capabilities.Device;
            if (deviceCapability != null)
            {
                capabilities.DeviceCapability = new DeviceCapability()
                {
                    XAddress = deviceCapability.XAddr
                };

                capabilities.DeviceCapability.SupportedOnvifVersions = new List<string>();
                foreach (OnvifVersion onvifVersion in deviceCapability.System.SupportedVersions)
                {
                    capabilities.DeviceCapability.SupportedOnvifVersions.Add($"{onvifVersion.Major}.{onvifVersion.Minor}");
                }
            }

            var eventCapability = response.Capabilities.Events;
            if (eventCapability != null)
            {
                capabilities.EventCapability = new EventCapability()
                {
                    XAddress = eventCapability.XAddr
                };
            }

            var imagingCapability = response.Capabilities.Imaging;
            if (imagingCapability != null)
            {
                capabilities.ImagingCapability = new ImagingCapability()
                {
                    XAddress = imagingCapability.XAddr
                };
            }

            var mediaCapability = response.Capabilities.Media;
            if (mediaCapability != null)
            {
                capabilities.MediaCapability = new MediaCapability()
                {
                    XAddress = mediaCapability.XAddr
                };
            }

            var ptzCapability = response.Capabilities.PTZ;
            if (ptzCapability != null)
            {
                capabilities.PtzCapability = new PtzCapability()
                {
                    XAddress = ptzCapability.XAddr
                };
            }

            return capabilities;
        }
    }
}
