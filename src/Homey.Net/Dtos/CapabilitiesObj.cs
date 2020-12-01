using System;

namespace Homey.Net.Dtos
{
    public class CapabilitiesObj
    {
        public string Value { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Type { get; set; }
        public bool Getable { get; set; }
        public bool Setable { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Units { get; set; }
        public int Decimals { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string ChartType { get; set; }
        public string Id { get; set; }
        //public Options Options { get; set; }
    }
}
