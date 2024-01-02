using Abp.Domain.Services;
using Mictlanix.DotNet.Onvif;
using Mictlanix.DotNet.Onvif.Common;
using Capabilities = Eoss.Backend.Entities.Capabilities;

namespace Eoss.Backend.Domain.Onvif.Capability
{
    public class OnvifDeviceCapabilityManager : DomainService, IOnvifDeviceCapabilityManager
    {
        public async Task<Capabilities> GetCapabilitiesAsync(string host, string username, string password)
        {
            return await DoGetCapabilities(host, username, password);
        }

        private static async Task<Capabilities> DoGetCapabilities(string host, string username, string password)
        {
            var device = await OnvifClientFactory.CreateDeviceClientAsync(host, username, password);
            var response = await device.GetCapabilitiesAsync(new CapabilityCategory[] { CapabilityCategory.All });

            var capabilities = new Capabilities();

            var analyticsCapability = response.Capabilities.Analytics;
            if (analyticsCapability != null)
            {
                capabilities.AnalyticsXAddress = analyticsCapability.XAddr;
            }

            var deviceCapability = response.Capabilities.Device;
            if (deviceCapability != null)
            {
                capabilities.DeviceXAddress = deviceCapability.XAddr;
   
                capabilities.SupportedOnvifVersions = new List<string>();
                foreach (OnvifVersion onvifVersion in deviceCapability.System.SupportedVersions)
                {
                    capabilities.SupportedOnvifVersions.Add($"{onvifVersion.Major}.{onvifVersion.Minor}");
                }
            }

            var eventCapability = response.Capabilities.Events;
            if (eventCapability != null)
            {
                capabilities.EventXAddress = eventCapability.XAddr;
            }

            var imagingCapability = response.Capabilities.Imaging;
            if (imagingCapability != null)
            {
                capabilities.ImagingXAddress = imagingCapability.XAddr;
            }

            var mediaCapability = response.Capabilities.Media;
            if (mediaCapability != null)
            {
                capabilities.MediaXAddress = mediaCapability.XAddr;
            }

            var ptzCapability = response.Capabilities.PTZ;
            if (ptzCapability != null)
            {
                capabilities.PtzXAddress = ptzCapability.XAddr;
            }

            return capabilities;
        }
    }
}
