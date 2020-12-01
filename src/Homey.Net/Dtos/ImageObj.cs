using System;

namespace Homey.Net.Dtos
{
    public class ImageObj
    {
        public string Id { get; set; }
        public string OwnerUri { get; set; }
        public string Url { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
