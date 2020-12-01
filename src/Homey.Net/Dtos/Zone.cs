using System;

namespace Homey.Net.Dtos
{
    public class Zone
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public object Parent { get; set; }
        public bool Active { get; set; }
        public DateTime? ActiveLastUpdated { get; set; }
        public string Icon { get; set; }
    }
}
