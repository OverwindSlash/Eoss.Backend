using Abp.Dependency;
using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif.Discovery
{
    public class OnvifDiscoveryManager : DomainService, IOnvifDiscoveryManager, ISingletonDependency
    {
        private static readonly OnvifDiscovery.Discovery OnvifDiscovery;

        static OnvifDiscoveryManager()
        {
            OnvifDiscovery = new OnvifDiscovery.Discovery();
        }

        public async Task<List<DiscoveredDevice>> DiscoveryDeviceAsync(int timeOutSecs = 1)
        {
            return await DoDiscoveryDeviceAsync(timeOutSecs);
        }

        private async Task<List<DiscoveredDevice>> DoDiscoveryDeviceAsync(int timeOutSecs)
        {
            List<DiscoveredDevice> discoveredDevices = new ();
            try
            {
                await foreach (var device in OnvifDiscovery.DiscoverAsync(timeOutSecs))
                {
                    var discoveredDevice = new DiscoveredDevice()
                    {
                        Name = $"{device.Mfr}:{device.Model}",
                        Manufacturer = device.Mfr,
                        Model = device.Model,
                        Ipv4Address = device.Address,
                        XAddress = device.XAddresses.ToList(),
                        Types = device.Types.ToList(),
                        Scopes = device.Scopes.ToList()
                    };

                    discoveredDevices.Add(discoveredDevice);
                }

                return discoveredDevices;
            }
            catch (Exception _)
            {
                return new List<DiscoveredDevice>();
            }
        }
    }
}
