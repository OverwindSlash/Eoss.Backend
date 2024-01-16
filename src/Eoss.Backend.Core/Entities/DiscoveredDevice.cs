using System.Collections.Generic;

namespace Eoss.Backend.Entities
{
    public class DiscoveredDevice
    {
        public string Name { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public string Ipv4Address { get; set; }

        public List<string> Types { get; set; }

        public List<string> XAddress { get; set; }

        public List<string> Scopes { get; set; }
    }
}
