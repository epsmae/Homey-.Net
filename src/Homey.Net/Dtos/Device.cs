using System.Collections.Generic;

namespace Homey.Net.Dtos
{
    public class Device
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DriverUri { get; set; }
        public string DriverId { get; set; }
        public string Zone { get; set; }
        public string ZoneName { get; set; }
        //public object icon { get; set; }
        //public object iconObj { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        //public object SettingsObj { get; set; }
        //public object Class { get; set; }
        //public object Energy { get; set; }
        //public object EnergyObj { get; set; }
        //public object VirtualClass { get; set; }
        public IList<string> Capabilities { get; set; }
        public Dictionary<string, CapabilitiesObj> CapabilitiesObj { get; set; }
        public IList<string> Flags { get; set; }
        //public object ui { get; set; }
        public bool Ready { get; set; }
        public bool Available { get; set; }
        public bool Repair { get; set; }
        public bool Unpair { get; set; }
        public IList<string> SpeechExamples { get; set; }
        public IList<Image> Images { get; set; }
        public IList<Insight> Insights { get; set; }
        public Dictionary<string, string> Data { get; set; }

    }
}
