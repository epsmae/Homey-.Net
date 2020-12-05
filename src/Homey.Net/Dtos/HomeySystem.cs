using System;
using System.Collections.Generic;

namespace Homey.Net.Dtos
{
    public class HomeySystem
    {
        public string BootId { get; set; } 
        public string CloudId { get; set; } 
        public string Hostname { get; set; } 
        public string Platform { get; set; } 
        public string Release { get; set; } 
        public string Arch { get; set; } 
        public int Uptime { get; set; } 
        public List<double> Loadavg { get; set; } 
        public int Totalmem { get; set; } 
        public int FreememMachine { get; set; } 
        public string FreememHuman { get; set; } 
        public List<Cpu> Cpus { get; set; } 
        public DateTime Date { get; set; } 
        public string DateHuman { get; set; } 
        public bool DateDst { get; set; } 
        public bool Devmode { get; set; } 
        public string NodeVersion { get; set; } 
        public string HomeyVersion { get; set; } 
        public string HomeyModelId { get; set; } 
        public string HomeyModelName { get; set; } 
        public string Timezone { get; set; } 
        public string WifiSsid { get; set; } 
        public string WifiAddress { get; set; } 
        public string WifiMac { get; set; } 
    }
}
