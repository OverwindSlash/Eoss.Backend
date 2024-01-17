using Abp.Dependency;
using Abp.Domain.Services;
using Mictlanix.DotNet.Onvif;
using Mictlanix.DotNet.Onvif.Common;
using Mictlanix.DotNet.Onvif.Device;
using Capabilities = Eoss.Backend.Entities.Capabilities;
using Device = Eoss.Backend.Entities.Device;

namespace Eoss.Backend.Domain.Onvif
{
    public class OnvifDeviceManager : DomainService, IOnvifDeviceManager, ISingletonDependency
    {
        public async Task<Device> GetDeviceInfoAsync(string host, string username, string password)
        {
            var deviceClient = await OnvifClientFactory.CreateDeviceClientAsync(host, username, password);
            var response = await deviceClient.GetDeviceInformationAsync(new GetDeviceInformationRequest());

            var device = new Device()
            {
                DeviceId = response.SerialNumber,
                Ipv4Address = host,
                Model = response.Model,
                Manufacturer = response.Manufacturer
            };

            return device;
        }

        public async Task<Capabilities> GetCapabilitiesAsync(string host, string username, string password)
        {
            return await DoGetCapabilitiesAsync(host, username, password);
        }

        private static async Task<Capabilities> DoGetCapabilitiesAsync(string host, string username, string password)
        {
            var deviceClient = await OnvifClientFactory.CreateDeviceClientAsync(host, username, password);
            var response = await deviceClient.GetCapabilitiesAsync(new CapabilityCategory[] { CapabilityCategory.All });

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
