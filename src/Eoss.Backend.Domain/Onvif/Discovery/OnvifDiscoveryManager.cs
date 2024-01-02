using Abp.Dependency;
using Abp.Domain.Services;

namespace Eoss.Backend.Domain.Onvif.Discovery
{
    public class OnvifDiscoveryManager : DomainService, IOnvifDiscoveryManager, ISingletonDependency
    {
        private const int DiscoveryTimeOutSeconds = 10;
        private static readonly OnvifDiscovery.Discovery OnvifDiscovery;

        static OnvifDiscoveryManager()
        {
            OnvifDiscovery = new OnvifDiscovery.Discovery();
        }

        public async Task<List<DiscoveredDevice>> DiscoveryDeviceAsync()
        {
            return await DoDiscoveryDeviceAsync();
        }

        private static async Task<List<DiscoveredDevice>> DoDiscoveryDeviceAsync()
        {
            List<DiscoveredDevice> discoveredDevices = new ();
            try
            {
                await foreach (var device in OnvifDiscovery.DiscoverAsync(DiscoveryTimeOutSeconds))
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
